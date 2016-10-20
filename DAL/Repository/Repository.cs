using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Repository
{
    public class Repository<T> where T : class 
    {
        private IMongoDatabase _database;
        private string _tableName;
        private readonly IMongoCollection<T> _collection;

        public Repository(IMongoDatabase db, string tblName)
        {
            _database = db;
            _tableName = tblName;
            _collection = _database.GetCollection<T>(tblName);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var filter = Builders<T>.Filter.Where(r => true);
            var options = new FindOptions<T> { Limit = 10 };

            return await _collection.FindAsync<T>(filter, options).Result.ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, ObjectId>> queryExpression, ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq(queryExpression, id);
            return await _collection.FindSync<T>(filter).ToListAsync();
        }

        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(Expression<Func<T, ObjectId>> queryExpression, ObjectId id, T entity)
        {
            var filter = Builders<T>.Filter.Eq(queryExpression, id);
            _collection.ReplaceOne(filter, entity);
        }

        public void Delete(Expression<Func<T, ObjectId>> queryExpression, ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq(queryExpression, id);
            DeleteResult deleteResult = _collection.DeleteOne(filter);
        }
    }
}