using Microsoft.AspNetCore.Identity;

namespace AuthApplication.Model
{
    public class UsuarioModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UsuarioModel(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public void GerarSenhaHash()
        {
            var _passHasher = new PasswordHasher<string>();
            var senhaHash = _passHasher.HashPassword(null, this.Password);
            this.Password = senhaHash;
        }

        //public bool CompareHash(string password)
        //{
        //    var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        //    return result == PasswordVerificationResult.Success;
        // preciso de um repository de user para buscar o hash da senha pelo email antes de implementar essa função
        //}
    }
}
