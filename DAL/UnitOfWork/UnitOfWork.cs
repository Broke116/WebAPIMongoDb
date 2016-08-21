using DAL.Models;
using DAL.Repository;
using MongoDB.Driver;

namespace DAL.UnitOfWork
{
    public class UnitOfWork
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private readonly IMongoCollection<Restaurant> _collection;
        protected Repository<Restaurant> _restaurant;

        public UnitOfWork()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("test");
            _collection = _database.GetCollection<Restaurant>("restaurants");
        }

        public Repository<Restaurant> Restaurant
        {
            get
            {
                if (_restaurant == null)
                    _restaurant = new Repository<Restaurant>(_database, "restaurants");

                return _restaurant;
            }
        }
    }
}