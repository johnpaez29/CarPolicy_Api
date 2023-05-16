using MongoDB.Driver;

namespace Api.CarPolicy.DataAccess.Repositories
{
    public interface IPolicyRepository<T>
    {
        public Task<bool> Insert(T item);

        public Task<T> Get(FilterDefinition<T> filter);
    }
}
