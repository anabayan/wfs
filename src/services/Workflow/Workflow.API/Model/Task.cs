using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workflow.API.Model
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }
        public string HelpUrl { get; set; }

        public List<Input> Inputs { get; set; }
    }
}
