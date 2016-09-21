using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Repositories.Repositories
{
    public class RepositoryBase<T> where T : ModelBase
    {
        public readonly string Collection;
        public RepositoryBase(string collectionName)
        {
            Collection = collectionName;
        }

        /// <summary>
        /// To initialize the db
        /// </summary>
        /// <typeparam name="T">Entity Type that is used in the DB</typeparam>
        /// <param name="collectionName">mongodb Collection name</param>
        /// <returns></returns>
        public MongoCollection<T> GetCollection(string collectionName)
        {
            //TODO: find a better way for the db initialization
            //TODO: find a better way for the connection strings 

            var _client = new MongoClient();
            var _database = _client.GetServer().GetDatabase("pubSub");

            //TODO: Find a better way to handle collection names
            return _database.GetCollection<T>(collectionName);
        }

        #region Basic CRUD implementation

        public virtual string Save(T entity)
        {
            GetCollection(Collection).Insert(entity);
            return entity.Id.ToString();
        }

        public virtual void Update(T entity)
        {
            GetCollection(Collection).Save(entity);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Task.Run(() =>
            {
                var publishers = GetCollection(Collection).AsQueryable().ToList();
                publishers.ForEach(f => f.StringId = f.Id.ToString());
                return publishers;
            });
        }

        public virtual async Task<T> Get(string id)
        {
            var objectId = ObjectId.Parse(id);

            return await Task.Run(
                        () => GetCollection(Collection).AsQueryable()
                        .FirstOrDefault(f => f.Id == objectId));
        }

        #endregion 
    }
}