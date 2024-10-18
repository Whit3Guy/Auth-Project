using AuthApplication.DTOs;
using AuthApplication.Model;
using AuthApplication.Repository;
using AuthApplication.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _rep;
        public AuthController(IUsuarioRepository rep)
        {
            _rep = rep;
        }


        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto userLogin)
        {
            UsuarioModel user = await _rep.GetByEmail(userLogin.email);
            bool result = user.CompareHash(userLogin.password);
            if (!result)
            {
                return BadRequest("senha incorreta!");
            }
            return Ok("senha correta!");
            
        }
    }
}
