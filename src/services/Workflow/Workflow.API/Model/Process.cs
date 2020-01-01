using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workflow.API.Model
{
    public class Process
    {
        public string Name { get; set; }
        public List<Phase> Phases { get; set; }

    }
}
