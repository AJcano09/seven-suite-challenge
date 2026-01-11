
namespace SevenSuite.BLL
{
    public class AuthService
    {
        public bool ValidateCredentials(string username, string password)
        {
            // Credenciales simples para la prueba
            return username == "admin" && password == "1234";
        }
    }
}
