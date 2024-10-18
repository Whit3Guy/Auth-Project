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
                return BadRequest("senha incorreta!");
            }
            var token = _serviceToken.GerarTokenJWT(user);
            
            return Ok(token);
            
        }
    }
}
