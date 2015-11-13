using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
namespace Hk.Infrastructures.Mongo
{
    public class DbHelper
    {
        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static MongoCursor<T> Query<T>(string connectionString, string databaseName,
            string collectionName, IMongoQuery query)
        {
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<T> collection = mongoDatabase.GetCollection<T>(collectionName);
            try
            {
                if (query == null)
                    return collection.FindAll();
                else
                    return collection.Find(query);
            }
            finally
            {
                server.Disconnect();
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
 
        public static Boolean Insert<T>(string connectionString, string databaseName, string collectionName, T document)
        {
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();

            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<T> collection = mongoDatabase.GetCollection<T>(collectionName);
            try
            {
                collection.Insert(document);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }


        /// <summary>
        /// InsertBatch
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <returns></returns>
 
        public static Boolean InsertBatch<T>(string connectionString, string databaseName, string collectionName, List<T> documents)
        {
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();

            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<T> collection = mongoDatabase.GetCollection<T>(collectionName);
            try
            {
                collection.InsertBatch(documents);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <param name="newDocument"></param>
        /// <returns></returns>
   
        public static Boolean Update(string connectionString, string databaseName, String collectionName,
            IMongoQuery query, IMongoUpdate newDocument)
        {
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            try
            {
                collection.Update(query, newDocument);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Boolean Remove(string connectionString, string databaseName, String collectionName, IMongoQuery query)
        {
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            try
            {
                collection.Remove(query);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }
    }
}
