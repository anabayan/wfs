using Components.API.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Components.API.Infrastructure.Repositories
{
    public class WorkflowComponentDataRepository<T> : IWorkflowComponentDataRepository<T> where T : class
    {
        private DocumentClient documentClient;
        private ComponentsSettings _settings;

        // The database we will create
        private Database database;

        public WorkflowComponentDataRepository(IOptions<ComponentsSettings> settings)
        {
            _settings = settings.Value;
            documentClient = new DocumentClient(new Uri(_settings.AzureCosmosDBUri), _settings.AzureCosmosDBPrimaryKey);

            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();

        }
        
        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await documentClient.ReadDocumentAsync(
                    UriFactory.CreateDocumentUri(_settings.AzureCosmosDBDatabaseId, _settings.AzureCosmosDBContainerId, id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_settings.AzureCosmosDBDatabaseId, _settings.AzureCosmosDBContainerId),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var partialResult = await query.ExecuteNextAsync<T>();
                results.AddRange(partialResult);
            }

            return results;
        }


        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            IDocumentQuery<T> query = documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_settings.AzureCosmosDBDatabaseId, _settings.AzureCosmosDBContainerId),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var partialResult = await query.ExecuteNextAsync<T>();
                results.AddRange(partialResult);
            }

            return results;
        }

        public async Task<Document> CreateItemAsync(T item)
        {
            return await documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_settings.AzureCosmosDBDatabaseId, _settings.AzureCosmosDBContainerId), item);
        }

        public async Task<Document> UpdateItemAsync(string id, T item)
        {
            return await documentClient.ReplaceDocumentAsync(
                UriFactory.CreateDocumentUri(_settings.AzureCosmosDBDatabaseId, _settings.AzureCosmosDBContainerId, id), item);
        }

        public async Task DeleteItemAsync(string id)
        {
            await documentClient.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(_settings.AzureCosmosDBDatabaseId, _settings.AzureCosmosDBContainerId, id));
        }



        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_settings.AzureCosmosDBDatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await documentClient.CreateDatabaseAsync(new Database { Id = _settings.AzureCosmosDBDatabaseId });
                }
                else
                {
                    throw e;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await documentClient.ReadDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(_settings.AzureCosmosDBDatabaseId,
                                                            _settings.AzureCosmosDBContainerId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_settings.AzureCosmosDBDatabaseId),
                        new DocumentCollection { Id = _settings.AzureCosmosDBContainerId });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
