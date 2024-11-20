using Microsoft.AspNetCore.Mvc;
using api.Services.GeneralAPIServices;

namespace api.Controllers
{

    [ApiController]
    [Route("api")]
    public class APIController<TItem> : ControllerBase where TItem : class
    {
        private readonly IAPIService<TItem> _repository;

        public APIController(IAPIService<TItem> repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TItem>> GetById(int id)
        {
            var item = await _repository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpGet("getid")]
        public async Task<ActionResult<ServiceResponse<int>>> GetId([FromQuery] string seqName)
        {
            var response = await _repository.GetId(seqName);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("getallpaged")]
        public async Task<ActionResult<ServiceResponse<PageDataReturn<TItem>>>> GetAll([FromQuery] PaginationFilter filter, [FromQuery] string lamda)
        {
            var response = await _repository.GetAll(filter.PageNumber, filter.PageSize, lamda);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("getallnopaged")]
        public async Task<ActionResult<ServiceResponse<List<TItem>>>> GetAll([FromQuery] string lamda)
        {
            var response = await _repository.GetAll(lamda);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
 

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> Create(TItem item)
        {
            var response = await _repository.Create(item);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<bool>>> Update(TItem item)
        {
            var response = await _repository.Update(item);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(string lamda)
        {
            var response = await _repository.Delete(lamda);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }

}