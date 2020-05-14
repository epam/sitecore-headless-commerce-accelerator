#reference "System.Xml.Linq"

using System.Xml.Linq;

public static class Coverage
{
    public static CakeTaskBuilder OutputCoverage { get; set; }
}

Coverage.OutputCoverage = Task("Coverage :: Coverage Output")
    .Does(() => 
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.TestsCoverageOutputDir, "TestsCoverageOutputDir", "TESTS_COVERAGE_OUTPUT_DIR");

        const string lineRateAttribute = "line-rate";
        const string branchRateAttribute = "branch-rate";

        var fullCoveragePath = $"{Sitecore.Parameters.TestsCoverageOutputDir}/cobertura-coverage.xml";

        var fullCoverage = XDocument.Load(fullCoveragePath);

        var coverageNode = fullCoverage.XPathSelectElement("/coverage");

        var lineRate = getAttributeDoubleValue(coverageNode, lineRateAttribute);
        var branchRate = getAttributeDoubleValue(coverageNode, branchRateAttribute);

        Information($"Line coverage: {lineRate:P2}");
        Information($"Branch coverage: {branchRate:P2}");
    });