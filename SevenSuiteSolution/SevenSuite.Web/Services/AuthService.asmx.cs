using SevenSuite.Web.Models;
using System;
using System.Web;
using System.Web.Services;

namespace SevenSuite.Web.Services
{
    /// <summary>
    /// Summary description for AuthService
    /// </summary>
    [WebService(Namespace = "http://seventsuite.local/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AuthService : System.Web.Services.WebService
    {

        private const string AUTH_COOKIE = "AUTH_SESSION";
        private readonly SevenSuite.BLL.AuthService _auth =
            new SevenSuite.BLL.AuthService();

        [WebMethod]
        public ApiResponse<bool> HasSession()
        {
            var hasCookie = HttpContext.Current.Request.Cookies[AUTH_COOKIE] != null;
            
            return ApiResponse<bool>.Ok(hasCookie);
        }


        [WebMethod]
        public ApiResponse<bool> Login(string username, string password)
        {
            if (!_auth.ValidateCredentials(username, password))
            {
                return ApiResponse<bool>.Fail(
                    "InvalidCredentials",
                    "Usuario o contraseña incorrectos.");
            }

            var expiresAt = DateTime.UtcNow.AddMinutes(5);

            var cookie = new HttpCookie(AUTH_COOKIE, expiresAt.Ticks.ToString())
            {
                HttpOnly = true,
                Path = "/",
                Expires = expiresAt
            };


            HttpContext.Current.Response.Cookies.Add(cookie);
            return ApiResponse<bool>.Ok(true);
        }

        [WebMethod]
        public ApiResponse<bool> Logout()
        {
            var cookie = new HttpCookie(AUTH_COOKIE)
            {
                Path = "/",
                Expires = DateTime.Now.AddDays(-1)
            };

            HttpContext.Current.Response.Cookies.Add(cookie);

            return ApiResponse<bool>.Ok(true);
        }

        [WebMethod]
        public ApiResponse<int> GetSessionRemainingMinutes()
        {
            var cookie = HttpContext.Current.Request.Cookies[AUTH_COOKIE];

            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return ApiResponse<int>.Fail("NoSession", "Sin sesión activa");

            long ticks;
            if (!long.TryParse(cookie.Value, out ticks))
                return ApiResponse<int>.Fail("InvalidSession", "Sesión inválida");

            var expiresAt = new DateTime(ticks, DateTimeKind.Utc);
            var remaining = (int)Math.Max(
                0,
                (expiresAt - DateTime.UtcNow).TotalMinutes
            );

            return ApiResponse<int>.Ok(remaining);
        }



    }
}
