namespace api.Services.GeneralAPIServices
{
    public interface IAPIService<TItem> where TItem : class
    {
        
        Task<ServiceResponse<TItem>> GetById(int id);
        Task<ServiceResponse<List<TItem>>> GetAll(string lamda);
        Task<ServiceResponse<PageDataReturn<TItem>>> GetAll(int pageNumber, int pageSize, string lamda);

        Task<ServiceResponse<bool>> Update(TItem item);
        Task<ServiceResponse<bool>> Create(TItem item);
        Task<ServiceResponse<bool>> Delete(string lamda);
        Task<ServiceResponse<int>> GetId(string seqName);
    }
}