namespace Wooli.Foundation.Base.Models.Logging
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class CommonLog : Log
    {
        public override string Name => "log";
    }
}