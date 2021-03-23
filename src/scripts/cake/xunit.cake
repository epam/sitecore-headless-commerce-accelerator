public static class XUnitRunner
{
    public static CakeTaskBuilder RunTests { get; set; }
}

// TODO: Was copied from Cake.Sitecore to disable test shadow copy
XUnitRunner.RunTests = Task("xUnit Tests :: Run Server Tests")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.SrcDir, "SrcDir", "SRC_DIR");
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.SolutionName, "SolutionName", "SOLUTION_NAME");
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.XUnitTestsCoverageOutputDir, "XUnitTestsCoverageOutputDir", "XUNIT_TESTS_COVERAGE_OUTPUT_DIR");
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.XUnitTestsCoverageRegister, "XUnitTestsCoverageRegister", "XUNIT_TESTS_COVERAGE_REGISTER");
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.TestsOutputDir, "TestsOutputDir", "TESTS_OUTPUT_DIR");

        var coverSettings = new OpenCoverSettings()
            .WithFilter($"+[{Sitecore.Parameters.SolutionName}.*]*")
            .WithFilter($"-[{Sitecore.Parameters.SolutionName}.*.Tests*]*");
        coverSettings.SkipAutoProps = true;
        coverSettings.Register = Sitecore.Parameters.XUnitTestsCoverageRegister;
        coverSettings.MergeByHash = true;
        coverSettings.NoDefaultFilters = true;
        coverSettings.ReturnTargetCodeOffset = 0;

        void applyExclude<T>(ISet<T> filtersSet, string paramValue, Func<string, T> mapper)
        {
            if (!string.IsNullOrEmpty(paramValue))
            {
                var excludes = paramValue.Split(',').Select(mapper);
                filtersSet.UnionWith(excludes);
            }
        }

        applyExclude(coverSettings.ExcludedAttributeFilters, Sitecore.Parameters.XUnitTestsCoverageExcludeAttributeFilters, x => x);
        applyExclude(coverSettings.ExcludedFileFilters,      Sitecore.Parameters.XUnitTestsCoverageExcludeFileFilters, x => x);
        applyExclude(coverSettings.ExcludeDirectories,       Sitecore.Parameters.XUnitTestsCoverageExcludeDirectories, x => Directory($"{Sitecore.Parameters.SrcDir}/{x}"));

        var directories = GetDirectories(
                $"{Sitecore.Parameters.SrcDir}/**/bin", 
                new GlobberSettings { Predicate = fileSystemInfo => !fileSystemInfo.Path.FullPath.EndsWith("node_modules", StringComparison.OrdinalIgnoreCase) }
            );
        foreach (var directory in directories)
        {
            coverSettings.SearchDirectories.Add(directory);
        }

        EnsureDirectoryExists(Sitecore.Parameters.XUnitTestsCoverageOutputDir);

        var openCoverResultsFilePath = new FilePath($"{Sitecore.Parameters.XUnitTestsCoverageOutputDir}/coverage.xml");

        var xUnit2Settings = new XUnit2Settings {
                XmlReport = true,
                Parallelism = ParallelismOption.None,
                NoAppDomain = false,
                OutputDirectory = Sitecore.Parameters.TestsOutputDir,
                ReportName = "xUnitTestResults",
                ShadowCopy = false
            };

        OpenCover(
            tool => { tool.XUnit2($"{Sitecore.Parameters.SrcDir}/**/tests/bin/*.Tests.dll", xUnit2Settings); }, 
            openCoverResultsFilePath, 
            coverSettings
        );

        ReportGenerator(openCoverResultsFilePath, Sitecore.Parameters.XUnitTestsCoverageOutputDir);

        var converterExecutablePath = Context.Tools.Resolve("OpenCoverToCoberturaConverter.exe");
        StartProcess(converterExecutablePath, new ProcessSettings {
            Arguments = new ProcessArgumentBuilder()
                .Append($"-input:\"{openCoverResultsFilePath}\"")
                .Append($"-output:\"{Sitecore.Parameters.XUnitTestsCoverageOutputDir}/cobertura-coverage.xml\"")
        });
    })
    .OnError(exception =>
    {
        Sitecore.Variables.UnitTestsFailed = true;
    });