using AuthApplication.Model;
using AuthApplication.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public readonly IUsuarioRepository _rep;
        public UsuariosController(IUsuarioRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _rep.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public async Task<UsuarioModel> PostUser(UsuarioModel user)
        {
            return await _rep.Post(user);
        }
    }
}
