using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly IDataContext _context;

        public DataRepository(IDataContext context)
        {
            _context = context;
        }

        #region City
        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.Find(Builders<City>.Filter.Empty).ToListAsync();
        }

        public async Task<City> GetCity(string name)
        {
            FilterDefinition<City> filter = Builders<City>.Filter.Eq(x => x.Name, name);

            return await _context.Cities.Find(filter).FirstOrDefaultAsync();

        }

        public async Task CreateCity(City city)
        {
            await _context.Cities.InsertOneAsync(city);
        }

        public async Task<bool> UpdateCity(City city)
        {
            ReplaceOneResult updateResult = await _context.Cities.ReplaceOneAsync(filter: x => x.Id == city.Id, replacement: city);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeteleCity(string name)
        {
            FilterDefinition<City> filter = Builders<City>.Filter.Eq(x => x.Name, name);

            DeleteResult deleteResult = await _context.Cities.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }




        #endregion

        #region factory

        public async Task<IEnumerable<Factory>> GetFactories(string city)
        {
            FilterDefinition<Factory> filter = Builders<Factory>.Filter.Eq(x => x.City, city);

            return await _context.Factories.Find(filter).ToListAsync();
        }

        public async Task<Factory> GetFactory(string name)
        {
            FilterDefinition<Factory> filter = Builders<Factory>.Filter.Eq(x => x.Name, name);

            return await _context.Factories.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateFactory(Factory factory)
        {
            await _context.Factories.InsertOneAsync(factory);
        }

        public async Task<bool> UpdateFactory(Factory factory)
        {
            ReplaceOneResult updateResult = await _context.Factories.ReplaceOneAsync(filter: x => x.Id == factory.Id, replacement: factory);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeteleFactory(string name)
        {
            FilterDefinition<Factory> filter = Builders<Factory>.Filter.Eq(x => x.Name, name);

            DeleteResult deleteResult = await _context.Factories.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }




        #endregion

        #region Worker

        public async Task<IEnumerable<Worker>> GetWorkers(string factory)
        {
            FilterDefinition<Worker> filter = Builders<Worker>.Filter.Eq(x => x.Factory, factory);

            return await _context.Workers.Find(filter).ToListAsync();
        }

        public async Task<Worker> GetWorker(string id)
        {
            FilterDefinition<Worker> filter = Builders<Worker>.Filter.Eq(x => x.WorkerId, id);

            return await _context.Workers.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateWorker(Worker worker)
        {
            await _context.Workers.InsertOneAsync(worker);
        }

        public async Task<bool> UpdateWorker(Worker worker)
        {
            ReplaceOneResult updateResult = await _context.Workers.ReplaceOneAsync(filter: x => x.Id == worker.Id, replacement: worker);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeteleWorker(string id)
        {
            FilterDefinition<Worker> filter = Builders<Worker>.Filter.Eq(x => x.WorkerId, id);

            DeleteResult deleteResult = await _context.Workers.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

       


        #endregion

    }

    public interface IDataRepository
    {
                
        #region City 

        Task<IEnumerable<City>> GetCities();

        Task<City> GetCity(string name);

        Task CreateCity(City city);

        Task<bool> UpdateCity(City city);

        Task<bool> DeteleCity(string name);

        #endregion

        #region Factory
        Task<IEnumerable<Factory>> GetFactories(string city);

        Task<Factory> GetFactory(string name);

        Task CreateFactory(Factory factory);

        Task<bool> UpdateFactory(Factory factory);

        Task<bool> DeteleFactory(string name);

        #endregion


        #region Worker

        Task<IEnumerable<Worker>> GetWorkers(string factory);

        Task<Worker> GetWorker(string id);

        Task CreateWorker(Worker worker);

        Task<bool> UpdateWorker(Worker worker);

        Task<bool> DeteleWorker(string id);

        #endregion



    }
}
