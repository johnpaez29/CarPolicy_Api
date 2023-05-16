using Api.CarPolicy.Model.Service;

namespace Api.CarPolicy.Business.Handlers
{
    public interface IPolicyHandler<in T> where T : class
    {
        Task<ResponseGeneric> Insert(T item);

        Task<ResponseGeneric> Get(T filter);
    }
}
