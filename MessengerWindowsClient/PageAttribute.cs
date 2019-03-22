using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerWindowsClient
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class PageAttribute:Attribute
    {
        public string Name { get; set; }

        public PageAttribute(string name)
        {
            Name = name;
        }
    }
}
