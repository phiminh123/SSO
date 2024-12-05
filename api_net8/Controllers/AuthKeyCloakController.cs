using api.SsoKeyCloak;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthKeyCloakController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AuthKeyCloakController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var tokenEndpoint = "http://10.0.27.242:8080/realms/test_client/protocol/openid-connect/token";

            var parameters = new Dictionary<string, string>
        {
            { "client_id", "TestSSO" },  
            { "client_secret", "4W3jtKXsBJTUHszABbSQltdhVcrD9dGI"}, 
            { "grant_type", "password" }, 
            { "username", model.Username }, 
            { "password", model.Password }  
        };

            var response = await _httpClient.PostAsync(tokenEndpoint,
                new FormUrlEncodedContent(parameters));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return Ok(result);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok(new { message = "This is a protected endpoint" });
        }
    }
}
