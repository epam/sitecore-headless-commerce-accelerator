#reference "System.Net"
#reference "System.Net.Http"
#reference "Newtonsoft.Json"

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public static CakeTaskBuilder UpdateSchema {get; set; }

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

public class HttpManager
{
    public string Post(string url, string command, string jsonRequest){
        using(var client = new HttpClient())
        {
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes($"{{\"{command}\":{jsonRequest}}}"));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var res = client.PostAsync(url, content).Result;
            return Encoding.UTF8.GetString(res.Content.ReadAsByteArrayAsync().Result);
        }
    }

    public string Get(string url){
        using (var client = new HttpClient()) {
            return Encoding.UTF8.GetString(client.GetByteArrayAsync(url).Result);
        }
    }
}

Solr.AddSuggesterComponents = Task("Solr :: Add Suggester Components")
    .Does(()=>
    {
        var HttpManager = new HttpManager();

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

        string searchComponentResp = HttpManager.Get($"{solrConfigUrl}/searchComponent?componentName=suggest");
        string handlerComponentResp = HttpManager.Get($"{solrConfigUrl}/requestHandler?componentName=/suggest");

        JObject suggesterComponent = JsonConvert.DeserializeObject<JObject>(searchComponentResp);
        JObject handlerComponent = JsonConvert.DeserializeObject<JObject>(handlerComponentResp);

        var changeSearchComponentResponse = suggesterComponent["config"]["searchComponent"]["suggest"].HasValues
            ? HttpManager.Post(solrOverlayUrl, "update-searchcomponent", jsonSuggester)
            : HttpManager.Post(solrOverlayUrl, "add-searchcomponent", jsonSuggester);
        
        var changeHandlerComponentResponse = handlerComponent["config"]["requestHandler"]["/suggest"].HasValues
            ? HttpManager.Post(solrOverlayUrl, "update-requesthandler", jsonRequestHandler)
            : HttpManager.Post(solrOverlayUrl, "add-requesthandler", jsonRequestHandler);
    });

Solr.CreateCores = Task("Solr :: Create Cores")
    .Does(() => {
        
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        var HttpManager = new HttpManager();

        var baseUrl = $"https://{Solr.VagrantIP}:{Solr.SolrPort}/solr/admin/cores";
        var basePath = $"\\\\{Solr.VagrantIP}\\c$\\Solr\\{Solr.SolrInstance}\\server\\solr";
            
        var defaultConfPath = $"{basePath}\\sc10__web_index\\conf";
        var coreSolrConfigPath = "conf/solrconfig.xml";

        foreach (var core in Solr.CoresToCreate)
        {
            Information($"Creating core: {core}");
                    
            var coreStatusUrl = $"{baseUrl}?action=STATUS&core={core}";

            Information($"\tExecuting request to: {coreStatusUrl}");
            var coreStatusRes = HttpManager.Get(coreStatusUrl);
                
            Information("\tParsing core status...");
            JObject coreStatus = JsonConvert.DeserializeObject<JObject>(coreStatusRes);
            var coreExists = coreStatus["status"][core].HasValues;

            if (coreExists) {
                if (Solr.RecreateCoresIfExist) {
                    var coreDeleteUrl = $"{baseUrl}?action=UNLOAD&core={core}&deleteInstanceDir=true";

                    Information($"\tExecuting request to: {coreDeleteUrl}");
                    HttpManager.Get(coreDeleteUrl);
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
                HttpManager.Get(coreCreateUrl);               
            }
        }

        string coresToPopulateSchema = string.Join("|", Solr.CoresToCreate);
        var populateManagedSchemaUrl = $"http://hca.local/sitecore/admin/PopulateManagedSchema.aspx?indexes={coresToPopulateSchema}";
        Information($"\tExecuting request to: {populateManagedSchemaUrl}");
        HttpManager.Get(populateManagedSchemaUrl);
    });

Solr.UpdateSchema = Task("Solr :: Update Schema")
    .Does(()=>
    {
        var HttpManager = new HttpManager();

        var solrSchemaUrl = $"https://{Solr.VagrantIP}:{Solr.SolrPort}/solr/{Solr.SolrCore}/schema";
        var solrReloadCoreUrl = $"https://{Solr.VagrantIP}:{Solr.SolrPort}/solr/admin/cores?action=RELOAD&core={Solr.SolrCore}";
        string fieldTypeName = "search_string";
        string dynamicFieldName = "*_s";

        string jsonFildType = JsonConvert.SerializeObject(new
            {
                name = fieldTypeName,
                @class = "solr.TextField",
                analyzer = new
                {
                    tokenizer = new
                    {
                        @class = "solr.KeywordTokenizerFactory"
                    },
                    filters = new[]
                    {
                        new { @class = "solr.TrimFilterFactory" }
                    }
                }
            });           

        string jsonDynamicField = JsonConvert.SerializeObject(new
            {
                name = dynamicFieldName,
                type = fieldTypeName,
                indexed = true,
                stored = true
            });

        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        string dynamicFieldResponse = HttpManager.Get($"{solrSchemaUrl}/dynamicfields/{dynamicFieldName}");
        string searchStringResponse = HttpManager.Get($"{solrSchemaUrl}/fieldtypes/{fieldTypeName}");

        JObject dynamicFieldComponent = JsonConvert.DeserializeObject<JObject>(dynamicFieldResponse);
        JObject searchStringComponent = JsonConvert.DeserializeObject<JObject>(searchStringResponse);
        
        var add_update_field_type = searchStringComponent["fieldType"].HasValues
            ? HttpManager.Post(solrSchemaUrl, "replace-field-type", jsonFildType)
            : HttpManager.Post(solrSchemaUrl, "add-field-type", jsonFildType);

        var add_update_dynamic_field = dynamicFieldComponent["dynamicField"].HasValues
            ? HttpManager.Post(solrSchemaUrl, "replace-dynamic-field", jsonDynamicField)
            : HttpManager.Post(solrSchemaUrl, "add-dynamic-field", jsonDynamicField);     

        Information($"Executing request to: {solrReloadCoreUrl}");
        HttpManager.Get(solrReloadCoreUrl);
    });