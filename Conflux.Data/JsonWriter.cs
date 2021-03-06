﻿namespace MondoDbProvider
{
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoJsonWriter
    {
        private static string collectionName;
        private static string connectionString;
        private static MongoClient client;
        private static MongoServer server;
        private static MongoDatabase database;
        private static MongoCollection entries;

        public static string DbName { get; private set; }

        public static string CollectionNaMe
        {
            get
            {
                return collectionName;
            }

            set
            {
                collectionName = value;
                entries = database.GetCollection(collectionName);
            }
        }

        public static void SetInitialData(string connStr, string databaseName, string initialCollection)
        {
            connectionString = connStr;
            DbName = databaseName;
            collectionName = initialCollection;
        }

        public static void EstablishConnection()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(DbName);
            entries = database.GetCollection(collectionName);
        }

        public static void SaveJson(string json)
        {
            BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);
            entries.Insert(document);
        }
    }
}
