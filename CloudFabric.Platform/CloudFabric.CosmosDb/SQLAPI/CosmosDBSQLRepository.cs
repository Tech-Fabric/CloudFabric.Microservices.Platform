using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Client;

namespace CloudFabric.CosmosDb.SQLAPI
{
    public static class CosmosDBSQLRepository<T> where T : class
    {
        private static string _database;
        private static DocumentClient _client;

        public static void Initialize(string uri, string database, string sharedAccessKey)
        {
            _database = database;
            _client = new DocumentClient(new Uri(uri), sharedAccessKey);
            CreateDatabaseIfNotExistsAsync().Wait();
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, string collection)
        {
            IDocumentQuery<T> query = _client.CreateDocumentQuery<T>
            (
                UriFactory.CreateDocumentCollectionUri
                (
                    _database,
                    collection
                ),
                new FeedOptions { MaxItemCount = -1 }
            )
            .Where(predicate)
            .AsDocumentQuery();

            List<T> results = new List<T>();

            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public static async Task<T> GetItemAsync(string id, string collection)
        {
            try
            {
                Document document = await _client.ReadDocumentAsync
                (
                    UriFactory.CreateDocumentUri
                    (
                        _database,
                        collection,
                        id
                    )
                );

                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public static async Task<Document> CreateItemAsync(T item, string collection)
        {
            return await _client.CreateDocumentAsync
            (
                UriFactory.CreateDocumentCollectionUri
                (
                    _database,
                    collection
                ),
                item
            );
        }

        public static async Task<Document> UpdateItemAsync(string id, T item, string collection)
        {
            return await _client.ReplaceDocumentAsync
            (
                UriFactory.CreateDocumentUri
                (
                    _database,
                    collection,
                    id
                ),
                item
            );
        }

        public static async Task DeleteItemAsync(string id, string collection)
        {
            await _client.DeleteDocumentAsync
            (
                UriFactory.CreateDocumentUri
                (
                    _database,
                    collection,
                    id
                )
            );
        }

        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_database));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDatabaseAsync(new Database { Id = _database });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
