using AuthApplication.DTOs;
using AuthApplication.Model;

namespace AuthApplication.Repository.Interfaces
{
    public interface IUsuarioRepository
    {

        public Task<List<UsuarioModel>> GetAll();
        public Task<UsuarioModel> GetById(int id);
        public Task<UsuarioModel> GetByEmail(string email);
        public Task<UsuarioModel> Post(UsuarioPutPostDto user);
        public Task<UsuarioModel> Put(int id ,UsuarioPutPostDto user);
        public Task<bool> Delete(int id);    
    }
}
