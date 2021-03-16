#addin nuget:https://api.nuget.org/v3/index.json?package=Cake.Http&version=1.2.2

#reference "System.Net"
#reference "Newtonsoft.Json"

using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Solr
{
    public static string SolrPort { get; private set; }
    public static string SolrCore { get; private set; }

    private static ICakeContext _context;

    public static CakeTaskBuilder AddSuggesterComponents { get; set; }

    public static void InitParams(
        ICakeContext context,
        string solrPort = "8983",
        string solrCore = "sc9_master_index"
        )
    {
        _context = context;

        SolrPort = solrPort;
        SolrCore = solrCore;
    }

    public static ICakeContext Context => _context;
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
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.SolrPort, "SolrPort", "SOLR_PORT");
        Sitecore.Utils.AssertIfNullOrEmpty(Solr.SolrCore, "SolrCore", "SOLR_CORE");

        var solrConfigurationManager = new SolrConfigurationManager();

        var solrConfigUrl = $"https://localhost:{Solr.SolrPort}/solr/{Solr.SolrCore}/config";
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


                