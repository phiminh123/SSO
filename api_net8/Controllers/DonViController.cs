using Microsoft.AspNetCore.Mvc;
using api.Services.GeneralAPIServices;
namespace api.Controllers
{
    [Route("api/donvi")]
    public class DonViController : APIController<tbDonVi>
    {
        public DonViController(IAPIService<tbDonVi> repository) : base(repository)
        {
        }
    }
}