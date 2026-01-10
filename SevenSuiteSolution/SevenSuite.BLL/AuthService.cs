using System;
using System.Web;

namespace SevenSuite.BLL
{
    public class AuthService
    {
        private const string AUTH_COOKIE = "AUTH_SESSION";

        public bool Login(string username, string password)
        {
            // Credenciales simples para la prueba
            if (username == "admin" && password == "1234")
            {
                var cookie = new HttpCookie(AUTH_COOKIE, "1")
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddHours(1)
                };

                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }

            return false;
        }

        public bool HasSession()
        {
            return HttpContext.Current.Request.Cookies[AUTH_COOKIE] != null;
        }

        public void Logout()
        {
            if (HttpContext.Current.Request.Cookies[AUTH_COOKIE] != null)
            {
                var cookie = new HttpCookie(AUTH_COOKIE)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
