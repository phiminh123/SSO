using NewProject.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace NewProject.Services.IApiServices
{
    public interface IApiServices
    {
        Task<List<T>> GetAll<T>(string apiPath,string lamda="");     // GET request
        Task<T> GetId<T>(string apiPath, string seqName); // GET request
        Task Update<T>(string apiPath, object data); // POST request
        Task Create<T>(string apiPath, object data); // PUT request
        Task Delete<T>(string apiPath, string lamda = "",bool showMessage=true); // DELETE request
    }
}
