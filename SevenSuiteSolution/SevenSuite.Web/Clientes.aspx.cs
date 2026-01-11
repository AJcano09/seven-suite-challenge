using System;

namespace SevenSuite.Web
{
    public partial class Clientes : System.Web.UI.Page
    {
        private const string AUTH_COOKIE = "AUTH_SESSION";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies[AUTH_COOKIE] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
        }

    }
}