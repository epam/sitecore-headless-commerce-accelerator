using System.Diagnostics.CodeAnalysis;

namespace HCA.Foundation.ConnectBase.Entities
{
    [ExcludeFromCodeCoverage]
    public class Qualification
    {
        public string ConditionOperator { get; set; }

        public string Condition { get; set; }

        public string Operator { get; set; }

        public string Subtotal { get; set; }
    }
}