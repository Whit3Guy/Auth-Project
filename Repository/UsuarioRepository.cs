using AuthApplication.Database;
using AuthApplication.DTOs;
using AuthApplication.Model;
using AuthApplication.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDbContext _context;
        public UsuarioRepository(UsuarioDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            UsuarioModel? user = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == id);
           if (user is null)
           {
                throw new Exception($"Usuario de id {id} não encontrado");
           }

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UsuarioModel>> GetAll()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<UsuarioModel> GetByEmail(string email)
        {
            UsuarioModel? user = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email);
            if (user is null)
            {
                throw new Exception($"Usuario de email {email} não encontrado");
            }
            return user;
        }

        public async Task<UsuarioModel> GetById(int id)
        {
            UsuarioModel? user = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if(user is null)
            {
                throw new Exception($"Usuario de id {id} não encontrado");
            }

            return user;
        }

        public async Task<UsuarioModel> Post(UsuarioPutPostDto usuario)
        {
            UsuarioModel user = new UsuarioModel(usuario.Name, usuario.Email, usuario.Password);
            user.GerarSenhaHash();
            
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UsuarioModel> Put(int id, UsuarioPutPostDto user)
        {
            UsuarioModel? oldUser = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == id);

            if (oldUser is null)
            {
                throw new Exception($"Usuario de id {id} não encontrado");
            }

            oldUser.Name = user.Name;
            oldUser.Email = user.Email;
            oldUser.Password = user.Password;
            oldUser.GerarSenhaHash();

            await _context.SaveChangesAsync();

            return oldUser;
        }
    }
}
