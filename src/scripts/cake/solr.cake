#addin nuget:https://api.nuget.org/v3/index.json?package=Cake.Http&version=1.2.2

#reference "System.Net"
#reference "Newtonsoft.Json"

using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Path = System.IO.Path;

public static class Solr
{
    public static string VagrantIP { get; private set; }

    public static string SolrInstance { get; private set; } 
    public static string SolrPort { get; private set; }
    public static string SolrCore { get; private set; }

    public static bool RecreateCoresIfExist { get; private set; }
    public static string[] CoresToCreate { get; private set; }

    private static ICakeContext _context;

    public static CakeTaskBuilder AddSuggesterComponents { get; set; }
    public static CakeTaskBuilder CreateCores { get; set; }

    public static void InitParams(
        ICakeContext context,
        string vagrantIP = "192.168.50.4",
        string solrInstance = "s10_solr-8.4.0",
        string solrPort = "8983",
        string solrCore = "sc10__master_index",
        bool recreateCoresIfExist = false,
        string[] coresToCreate = null
        )
    {
        _context = context;

        VagrantIP = vagrantIP;

        SolrInstance = solrInstance;
        SolrPort = solrPort;
        SolrCore = solrCore;
        
        RecreateCoresIfExist = recreateCoresIfExist;
        CoresToCreate = coresToCreate;
    }

    public static ICakeContext Context => _context;
}

public class FsUtils
{
    public static void Copy(string srcDir, string destDir) {
        var src = new DirectoryInfo(srcDir);
        var dest = new DirectoryInfo(destDir);

        CopyAll(src, dest);
    }

    public static void CopyAll(DirectoryInfo src, DirectoryInfo dest) {
        System.IO.Directory.CreateDirectory(dest.FullName);

        foreach (FileInfo file in src.GetFiles()) {
            file.CopyTo(Path.Combine(dest.FullName, file.Name), true);
        }

        foreach (DirectoryInfo srcSubDir in src.GetDirectories()) {
            DirectoryInfo nextDestSubDir = dest.CreateSubdirectory(srcSubDir.Name);
            CopyAll(srcSubDir, nextDestSubDir);
        }
    }
}

public class SolrConfigurationManager
{
    public string ChangeSolrConfig(string url, string command, string jsonRequest){

        return HttpClientAliases.HttpPost(Solr.Context, url, settings =>
            {
                settings.SetContentType("application/json")
                        .SetRequestBody($"{{\"{command}\":{jsonRequest}}}");
            });
    }

    public string GetSolrConfig(string url){
        return HttpClientAliases.HttpGet(Solr.Context, url);
    }
}

Solr.AddSuggesterComponents = Task("Solr :: Add Suggester Components")
    .Does(()=>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.VagrantIP, "VagrantIP", "VAGRANT_IP");
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.SolrPort, "SolrPort", "SOLR_PORT");
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.SolrCore, "SolrCore", "SOLR_CORE");

        var solrConfigurationManager = new SolrConfigurationManager();

        var solrConfigUrl = $"https://{Solr.VagrantIP}:{Solr.SolrPort}/solr/{Solr.SolrCore}/config";
        var solrOverlayUrl = $"{solrConfigUrl}/overlay";

        string jsonSuggester = JsonConvert.SerializeObject(new
            {
                name = "suggest",
                @class = "solr.SuggestComponent",
                suggester = new
                {
                    name = "productSuggester",
                    lookupImpl = "BlendedInfixLookupFactory",
                    dictionaryImpl = "DocumentDictionaryFactory",
                    field = "_displayname",
                    payloadField = "productid_t",
                    contextField = "_template",
                    suggestAnalyzerFieldType = "text_general",
                    buildOnStartup = "false"
                }
            });          

        string jsonRequestHandler = JsonConvert.SerializeObject(new
            {
                startup = "lazy",
                name = "/suggest",
                @class = "solr.SearchHandler",
                defaults = new
                {
                    suggest = "true",
                },
                components = new[] { "suggest" }
            });

        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        string searchComponentResp = solrConfigurationManager.GetSolrConfig($"{solrConfigUrl}/searchComponent?componentName=suggest");
        string handlerComponentResp = solrConfigurationManager.GetSolrConfig($"{solrConfigUrl}/requestHandler?componentName=/suggest");

        JObject suggesterComponent = JsonConvert.DeserializeObject<JObject>(searchComponentResp);
        JObject handlerComponent = JsonConvert.DeserializeObject<JObject>(handlerComponentResp);

        var changeSearchComponentResponse = suggesterComponent["config"]["searchComponent"]["suggest"].HasValues
            ? solrConfigurationManager.ChangeSolrConfig(solrOverlayUrl, "update-searchcomponent", jsonSuggester)
            : solrConfigurationManager.ChangeSolrConfig(solrOverlayUrl, "add-searchcomponent", jsonSuggester);
        
        var changeHandlerComponentResponse = handlerComponent["config"]["requestHandler"]["/suggest"].HasValues
            ? solrConfigurationManager.ChangeSolrConfig(solrOverlayUrl, "update-requesthandler", jsonRequestHandler)
            : solrConfigurationManager.ChangeSolrConfig(solrOverlayUrl, "add-requesthandler", jsonRequestHandler);
    });

Solr.CreateCores = Task("Solr :: Create Cores")
    .Does(() => {
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.VagrantIP, "VagrantIP", "VAGRANT_IP");
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.SolrInstance, "SolrInstance", "SOLR_INSTANCE");
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.SolrPort, "SolrPort", "SOLR_PORT");
        
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        var solrConfigurationManager = new SolrConfigurationManager();

        var baseUrl = $"https://{Solr.VagrantIP}:{Solr.SolrPort}/solr/admin/cores";
        var basePath = $"\\\\{Solr.VagrantIP}\\c$\\Solr\\{Solr.SolrInstance}\\server\\solr";
            
        var defaultConfPath = $"{basePath}\\configsets\\_default\\conf";
        var coreSolrConfigPath = "conf/solrconfig.xml";

        foreach (var core in Solr.CoresToCreate)
        {
            Information($"Creating core: {core}");
                    
            var coreStatusUrl = $"{baseUrl}?action=STATUS&core={core}";

            Information($"\tExecuting request to: {coreStatusUrl}");
            var coreStatusRes = solrConfigurationManager.GetSolrConfig(coreStatusUrl);
                
            Information("\tParsing core status...");
            JObject coreStatus = JsonConvert.DeserializeObject<JObject>(coreStatusRes);
            var coreExists = coreStatus["status"][core].HasValues;

            if (coreExists) {
                if (Solr.RecreateCoresIfExist) {
                    var coreDeleteUrl = $"{baseUrl}?action=UNLOAD&core={core}&deleteInstanceDir=true";

                    Information($"\tExecuting request to: {coreDeleteUrl}");
                    solrConfigurationManager.GetSolrConfig(coreDeleteUrl);
                }
                else {
                    Warning($"\t{core} already exists. No need to create one!");
                }
            }
            if (!coreExists || Solr.RecreateCoresIfExist) {
                var corePath = $"{basePath}\\{core}";
                var coreConfPath = $"{corePath}\\conf";
        
                var coreCreateUrl = $"{baseUrl}?action=CREATE&name={core}&config={coreSolrConfigPath}";
                    
                Information($"\tCreating core directory: {corePath}");
                System.IO.Directory.CreateDirectory(corePath);

                Information($"\tCopying conf folder: {defaultConfPath} -> {coreConfPath}");
                FsUtils.Copy(defaultConfPath, coreConfPath);
                
                Information($"\tExecuting request to: {coreCreateUrl}");
                solrConfigurationManager.GetSolrConfig(coreCreateUrl);
            }
        }
    });
                