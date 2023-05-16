using Api.CarPolicy.Model.DataBase;
using Api.CarPolicy.Model.DTO;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.CarPolicy.DataAccess.Repositories
{
    public class PolicyRepository : IPolicyRepository<Policy>
    {
        private readonly IMongoCollection<Policy> _policies;
        public PolicyRepository(
            IOptions<DataBaseSettings> settings 
            )
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            var database = client.GetDatabase(settings.Value.DatabaseName);

            _policies = database.GetCollection<Policy>(
                "Policies");
        }
        public async Task<Policy> Get(FilterDefinition<Policy> filter)
        {
            try
            {
                return await _policies.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error inserted policy.");
            }
        }

        public async Task<bool> Insert(Policy item)
        {
            try
            {
                item.PolicyNumber = null;
                await _policies.InsertOneAsync(item);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
