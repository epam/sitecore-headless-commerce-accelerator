using System;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums.Common
{
    public class ElementAttribute : Attribute
    {
        public ElementAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}