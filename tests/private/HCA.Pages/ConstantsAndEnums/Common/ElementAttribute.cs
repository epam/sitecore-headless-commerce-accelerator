using System;

namespace HCA.Pages.ConstantsAndEnums.Common
{
    public class ElementAttribute : Attribute
    {
        public string Name { get; }

        public ElementAttribute(string name) => Name = name;
    }
}
