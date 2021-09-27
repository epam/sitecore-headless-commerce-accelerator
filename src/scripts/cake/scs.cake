public static class SCS
{
    public static CakeTaskBuilder Push { get; set; }
}

SCS.Push = Task("SCS :: Push")
    .Does(() =>
    {
        StartPowershellScript("dotnet sitecore ser push");
    });
