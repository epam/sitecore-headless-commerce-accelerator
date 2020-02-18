namespace Wooli.Foundation.Base.Models.Logging
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class Log
    {
        public abstract string Name { get; }
    }
}