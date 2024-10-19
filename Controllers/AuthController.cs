using AuthApplication.DTOs;
using AuthApplication.Model;
using AuthApplication.Repository;
using AuthApplication.Repository.Interfaces;
using AuthApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _rep;
        private readonly TokenService _serviceToken;
        public AuthController(IUsuarioRepository rep, TokenService service)
        {
            _rep = rep;
            _serviceToken = service;
        }


        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto userLogin)
        {
            UsuarioModel user = await _rep.GetByEmail(userLogin.email);

            bool result = user.CompareHash(userLogin.password);

            if (result is false)
            {
                LoginResponse bad_result = new LoginResponse(false, null, null);
                return Unauthorized(bad_result);
            }

            var token = _serviceToken.GerarTokenJWT(user);

            UserStorageDto user_response = new (user.Id, user.Name, user.Email);
            LoginResponse response = new (true, token, user_response);

            return Ok(response);
            
        }
    }
}
