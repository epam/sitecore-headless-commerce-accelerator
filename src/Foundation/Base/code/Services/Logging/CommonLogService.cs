namespace Wooli.Foundation.Base.Services.Logging
{
    using System.Diagnostics.CodeAnalysis;
    using DependencyInjection;
    using Models.Logging;

    [ExcludeFromCodeCoverage]
    [Service(typeof(ILogService<CommonLog>), Lifetime = Lifetime.Singleton)]
    public class CommonLogService : LogService<CommonLog>
    { }
}