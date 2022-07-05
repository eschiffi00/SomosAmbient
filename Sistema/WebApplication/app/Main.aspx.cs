using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.app
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioId"] == null) Response.Redirect("~/app/Seguridad/UsuarioLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
        }

        
    }
}