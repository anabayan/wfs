using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workflow.API.Model
{
    public class Phase
    {
        public string Name { get; set; }
        public List<Step> Steps { get; set; }

    }
}
