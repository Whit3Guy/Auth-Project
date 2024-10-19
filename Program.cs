using AuthApplication.Database;
using AuthApplication.Repository;
using AuthApplication.Repository.Interfaces;
using AuthApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();

// Get Secret key in app.settings
if (builder.Configuration["JwtSettings:SecretKey"] is null || builder.Configuration["JwtSettings:ValidIssuer"] is null || builder.Configuration["JwtSettings:ValidAudience"] is null)
{
    throw new Exception("As variaveis do Jwt estão vazias");
}
    Console.WriteLine(builder.Configuration["JwtSettings:ValidAudience"]);
    Console.WriteLine(builder.Configuration["JwtSettings:SecretKey"]);
    Console.WriteLine(builder.Configuration["JwtSettings:ValidIssuer"]);

var myIssuer = builder.Configuration["JwtSettings:ValidIssuer"];
var secretKey = builder.Configuration["JwtSettings:SecretKey"];
var myAudience = builder.Configuration["JwtSettings:ValidAudience"];

builder.Services.AddSingleton(new TokenService(builder.Configuration["JwtSettings:SecretKey"],
        builder.Configuration["JwtSettings:ValidIssuer"],
        builder.Configuration["JwtSettings:ValidAudience"]
    ));

//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var secretKey = jwtSettings.GetValue<string>("SecretKey");

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<UsuarioDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("database")
     ));

// escopo do repositorio UsuarioRepository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// construir autenticação jwt Bearer

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = myIssuer,
        ValidAudience = myAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

    };
});

// Uma autorização para user comum e outra para admin, apenas admins podem ler listas de Usuarios e deletar usuarios.
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    option.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
