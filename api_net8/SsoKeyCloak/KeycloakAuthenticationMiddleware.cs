using System.IdentityModel.Tokens.Jwt;

namespace api.SsoKeyCloak
{
    public class KeycloakAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public KeycloakAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jsonToken != null)
                    {
                        context.Items["User"] = jsonToken.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;
                    }
                }
                catch
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            await _next(context);
        }
    }
}
