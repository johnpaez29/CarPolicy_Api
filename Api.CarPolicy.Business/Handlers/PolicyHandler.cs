
using Api.CarPolicy.DataAccess.Repositories;
using Api.CarPolicy.Model.DataBase;
using Api.CarPolicy.Model.DTO;
using Api.CarPolicy.Model.Service;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.CarPolicy.Business.Handlers
{
    public class PolicyHandler : IPolicyHandler<Policy>
    {

        private readonly IPolicyRepository<Policy> _policyRepository;
        public PolicyHandler(IPolicyRepository<Policy> policyRepository) 
        {
            _policyRepository = policyRepository;
        }
        public async Task<ResponseGeneric> Get(Policy filter)
        {
            var builder = Builders<Policy>.Filter;
            var filterPolicy = FilterDefinition<Policy>.Empty;
            if (!string.IsNullOrEmpty(filter.PolicyNumber))
                filterPolicy = builder.Eq(policy => policy.PolicyNumber, filter.PolicyNumber);
            if (!string.IsNullOrEmpty(filter?.Car?.Plate))
                filterPolicy &= builder.Eq(policy => policy.Car.Plate, filter.Car.Plate);

            var response = await _policyRepository.Get(filterPolicy);

            if (response != null)
                return new ResponseGeneric { Data = response, Status = (int)TaskStatus.Created, Message = "Ok" };
            
            throw new Exception("Error to the get policy, Validate filters.");
        }

        public async Task<ResponseGeneric> Insert(Policy item)
        {
            if (item?.Validity?.End < DateTime.UtcNow.AddHours(-5))
            {
                return new ResponseGeneric { Status = (int)TaskStatus.Faulted, Message = "The policy has expired"};
            }

            bool result = await _policyRepository.Insert(item);

            if (result) 
            {
                return new ResponseGeneric { Status = (int)TaskStatus.Created, Message = "Ok", Data = result };
            }

            throw new Exception("Error to the insert policy.");

        }
    }
}
