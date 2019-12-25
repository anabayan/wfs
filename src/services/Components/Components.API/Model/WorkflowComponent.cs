using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.API.Model
{
    public class WorkflowComponent
    {
        [JsonProperty(PropertyName = "id")]

        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "displayname")]
        public string DisplayName { get; set; }
    }
}
