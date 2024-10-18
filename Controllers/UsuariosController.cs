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

        //find all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _rep.GetAll();
            return Ok(users);
        }
        //find user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByEmail(int id)
        {
            UsuarioModel user = await _rep.GetById(id);
            return Ok(user);
        }

        //create user
        [HttpPost]
        public async Task<IActionResult> PostUser(UsuarioModel user)
        {
            UsuarioModel userToPost =  await _rep.Post(user);
            return Ok(userToPost);
        }

        // update user
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UsuarioModel user)
        {
            UsuarioModel userToChange = await _rep.Put(id, user);
            return Ok(userToChange);
        }

        // delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool deleted = await _rep.Delete(id);
            return Ok("usuario deletado com sucesso");
        }
    }
}
