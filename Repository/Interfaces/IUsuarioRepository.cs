using AuthApplication.Model;

namespace AuthApplication.Repository.Interfaces
{
    public interface IUsuarioRepository
    {

        public Task<List<UsuarioModel>> GetAll();
        public Task<UsuarioModel> GetById(int id);
        public Task<UsuarioModel> GetByEmail(string email);
        public Task<UsuarioModel> Post(UsuarioModel user);
        public Task<UsuarioModel> Put(int id ,UsuarioModel user);
        public Task<bool> Delete(int id);    
    }
}
