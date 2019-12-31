using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workflow.API.Model
{
    public class Input
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string DefaultValue { get; set; }
        public int MyProperty { get; set; }
        public bool IsRequired { get; set; }
        public string Type { get; set; }
        public string HelpMarkdown { get; set; }
    }
}
