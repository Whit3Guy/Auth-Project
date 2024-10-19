using System.Globalization;

namespace AuthApplication.DTOs
{
    public class LoginResponse
    {
        public LoginResponse(bool sucesso, string token, UserStorageDto userStorage)
        {
            Sucesso = sucesso;
            Token = token;
            UserStorage = userStorage;
        }

        public bool Sucesso { get; set; }
        public string Token { get; set; }
        public UserStorageDto UserStorage { get; set; }
    }
}
