using Microsoft.AspNetCore.Mvc;
using api.Services.GeneralAPIServices;
namespace api.Controllers
{
    [Route("api/canbo")]
    public class CanBoController : APIController<tbCanBo>
    {
        public CanBoController(IAPIService<tbCanBo> repository) : base(repository)
        {
        }
    }
}