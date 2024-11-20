using NewProject.Models;
namespace NewProject.Services.AuthenServices
{
    public interface IAuthenServices
    {
      Task<string> GetCurrentUserTokenAsync();
      string GetCurrentToken();
      Task<tbUser> GetMe();
    }
}

