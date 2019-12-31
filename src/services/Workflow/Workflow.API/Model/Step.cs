using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workflow.API.Model
{
    public class Step
    {
        public bool IsEnabled { get; set; }
        public bool IsContinueOnError { get; set; }
        public bool IsAlwaysRun { get; set; }
        public string DisplayName { get; set; }
        public string Condition { get; set; }
        public Task Task { get; set; }
    }
}
