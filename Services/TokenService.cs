using AuthApplication.DTOs;
using AuthApplication.Model;
using AuthApplication.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApplication.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(string secretKey, string issuer, string audience)
        {
            _issuer = issuer;
            _audience = audience;
            _secretKey = secretKey;
        }

        public string GerarTokenJWT(UsuarioModel user)
        {
            string chaveSecreta = _secretKey;
            //cheave secreta
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            //indicando o tipo de algoritmo de encriptação
            // nesse algoritmo, sua chave secreta deve conter pelo menos 32 caracteres, para que ele seja realmente eficaz
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //pegar o usuário para inserir suas coisas nas claims
            //Usuario usuario = await _rep.GetById(user);

            var claims = new[]
            {
                new Claim("Name",user.Name),
                new Claim("Email",user.Email),
                new Claim("User", "user")
            };


            // 2/3 criação do token 
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
