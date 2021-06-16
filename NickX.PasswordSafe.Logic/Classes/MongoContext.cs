using MongoDB.Driver;
using NickX.InventoryManagement.Logic.Interfaces;
using NickX.InventoryManagement.Models.Classes;
using System;
using System.Collections.Generic;

namespace NickX.InventoryManagement.Logic.Classes
{
    public class MongoContext : IDbContext
    {
        public string MongoConnectionString { get; set; }
        public string DisplayName { get; set; }
        public ICollection<Password> Passwords { get; private set; }

        private MongoClient client;
        private IMongoDatabase database;

        private const string PASSWORD_COL_NAME = "col_password";
        private const string CATEGORY_COL_NAME = "col_cateogry";

        public MongoContext(string mongoConnectionString)
        {
            // TODO: <MongoContext> - Implement creation of database & collections if they don't exist yet

            if (string.IsNullOrEmpty(new MongoUrl(mongoConnectionString).DatabaseName))
                throw new Exception("ConnectionString requires database name.");

            this.MongoConnectionString = mongoConnectionString;
            this.Initialize();
        }

        public void CreatePassword(Password password)
        {
            var col = database.GetCollection<Password>(PASSWORD_COL_NAME);
            col.InsertOne(password);
        }

        public Password GetPassword(Guid guid)
        {
            var col = database.GetCollection<Password>(PASSWORD_COL_NAME);
            return col.Find(x => x.Guid == guid).First();
        }

        public Password UpdatePassword(Password password)
        {
            // TODO: <MongoContext> - simplify this update process through deleteing current & creating new one

            var updateBuilder = Builders<Password>.Update;

            var update = Builders<Password>.Update
                .Set(x => x.DisplayName, password.DisplayName)
                .Set(x => x.DateCreate, password.DateCreate)
                .Set(x => x.Description, password.Description)
                .Set(x => x.Key, password.Key)
                .Set(x => x.Username, password.Username)
                .Set(x => x.Domain, password.Domain)
                .Set(x => x.Url, password.Url)
                .Set(x => x.DisplayColor, password.DisplayColor)
                .Set(x => x.AdditionalInformation, password.AdditionalInformation);

            var col = database.GetCollection<Password>(PASSWORD_COL_NAME);
            var filter = Builders<Password>.Filter.Eq(x => x.Guid, password.Guid);

            return col.FindOneAndUpdate<Password>(filter, update);
        }

        public void DeletePassword(Password password)
        {
            var col = database.GetCollection<Password>(PASSWORD_COL_NAME);
            var filter = Builders<Password>.Filter.Eq(x => x.Guid, password.Guid);
            col.FindOneAndDelete(filter);
        }

        public void ReloadPasswords()
        {
            var col = database.GetCollection<Password>(PASSWORD_COL_NAME);
            Passwords = col.Find<Password>(Builders<Password>.Filter.Empty).ToList();
        }

        void Initialize()
        {
            client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoUrl.Create(MongoConnectionString).DatabaseName);
            ReloadPasswords();
        }

    }
}
