global using api.Models;
global using Microsoft.EntityFrameworkCore;
global using api.Data;
global using AutoMapper;
using api.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using api.Services.GeneralAPIServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<dbAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbAcountConnection"), o => o.UseCompatibilityLevel(120)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
// builder.Services.AddSwaggerGen(c =>
//         {
//             c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//             {
//                 Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
//                 In = ParameterLocation.Header,
//                 Name = "Authorization",
//                 Type = SecuritySchemeType.Http,
//                 BearerFormat = "JWT",
//                 Scheme = "bearer"

//             });

//             c.AddSecurityRequirement(new OpenApiSecurityRequirement
//             {
//                 {
//                     new OpenApiSecurityScheme
//                     {
//                         Reference = new OpenApiReference
//                         {
//                             Type=ReferenceType.SecurityScheme,
//                             Id="Bearer"
//                         }
//                     },
//                     new string[]{}
//                 }
//             });
// .
//         }
// );
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(120)));
});
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped(sp =>
    new HttpClient {
        BaseAddress = new Uri("http://localhost:5105")
    });

builder.Services.AddScoped(typeof(IAPIService<>), typeof(APIService<>));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Keycloak:Authority"];
    options.Audience = builder.Configuration["Keycloak:ClientId"];
    options.RequireHttpsMetadata = false; 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
// app.UseSwagger();
// app.UseSwaggerUI(c =>);


app.UseHttpsRedirection();
app.UseOutputCache();
app.UseAuthorization();
app.MapControllers();

app.Run();

