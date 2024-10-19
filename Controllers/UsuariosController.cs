using AuthApplication.DTOs;
using AuthApplication.Model;
using AuthApplication.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByEmail(int id)
        {
            UsuarioModel user = await _rep.GetById(id);
            return Ok(user);
        }

        //create user
        [HttpPost]
        public async Task<IActionResult> PostUser(UsuarioPutPostDto user)
        {
            UsuarioModel userToPost =  await _rep.Post(new(user.Name, user.Password, user.Email));
            return Ok(userToPost);
        }

        // update user
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UsuarioPutPostDto user)
        {
            UsuarioModel userToChange = await _rep.Put(id, user);
            return Ok(userToChange);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool deleted = await _rep.Delete(id);
            return Ok("usuario deletado com sucesso");
        }
    }
}
