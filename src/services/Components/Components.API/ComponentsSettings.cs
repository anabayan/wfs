using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.API
{
    public class ComponentsSettings
    {
        public string AzureCosmosDBUri { get; set; }
        public string AzureCosmosDBPrimaryKey { get; set; }
        public string AzureCosmosDBSecondaryKey { get; set; }
        public string AzureCosmosDBDatabaseId { get; set; }
        public string AzureCosmosDBContainerId { get; set; }
    }
}
