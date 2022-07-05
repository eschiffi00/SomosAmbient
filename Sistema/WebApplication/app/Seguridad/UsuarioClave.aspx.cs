using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades.Entities;
using DbEntidades.Operators;

namespace WebApplication.app.Seguridad
{
    public partial class UsuarioClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s;
                object o = Page.RouteData.Values["id"];
                if (o != null) s = Page.RouteData.Values["id"].ToString();
                else s = Request.QueryString["id"];
                //en s NO viene el login name o viene 0 si es cambio de su propia clave
                //si viene trae el edificio;mail o login
                if (s != null && s != string.Empty && s != "0") //s tiene el login name encriptado o sino el email encriptado
                {
                    CryptorEngine c = new CryptorEngine();
                    s = CCLib.Base64Decode(s);
                    string desencriptado = c.Decrypt(s, true);
                    string[] a = desencriptado.Split(';');
                    LibDB2.DB.deshabilita_encripcion = false;
                    Session["Edificio"] = a[0];
                    string conn = System.Configuration.ConfigurationManager.ConnectionStrings[a[0]].ConnectionString;
                    Session["EdificioConStr"] = conn;
                    LibDB2.DB db = new LibDB2.DB(conn);
                    txtLogin.Text = a[1];
                }
                else //s viene nulo entonces es cambio de su propia clave
                {
                    object oo = Session["UsuarioId"];
                    if (oo != null) txtLogin.Text = UsuarioOperator.GetOneByIdentity(Convert.ToInt32(oo.ToString())).LoginName ;
                    else Response.Redirect(GetRouteUrl("Login", null));
                }

                ViewState["UsuarioClaveReferer"] = Request.UrlReferrer;
                lblMensaje.Text = string.Empty;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Usuario u = UsuarioOperator.GetOneByLoginAndEstado(txtLogin.Text, 1);
            if (u == null) //puede que no vine el login sino el email
            {
                u = UsuarioOperator.GetOneByEmailAndEstado(txtLogin.Text, 1);
            }
            u.Password = (new CryptorEngine()).Encrypt(txtPassword1.Text, true);
            UsuarioOperator.Save(u);
            
            if (ViewState["UsuarioClaveReferer"] != null) Response.Redirect(ViewState["UsuarioClaveReferer"].ToString());
            else lblMensaje.Text = "Clave guardada";
        }

    }
}