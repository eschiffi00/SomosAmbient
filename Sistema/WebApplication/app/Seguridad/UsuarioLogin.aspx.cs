using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades.Entities;
using DbEntidades.Operators;

namespace WebApplication.app.Seguridad
{
    public partial class UsuarioLogin : System.Web.UI.Page
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected UsuarioLogin()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Web.config")));
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                LibDB2.DB.deshabilita_encripcion = false;
                //Session["Edificio"] = ddlServidores.SelectedValue.ToUpperFirstLetter();
                Session["WebApplication"] = "WebApplication";
                //string conn = System.Configuration.ConfigurationManager.ConnectionStrings[ddlServidores.SelectedValue].ConnectionString;
                string conn = System.Configuration.ConfigurationManager.ConnectionStrings[Session["WebApplication"].ToString()].ConnectionString;
                Session["WebApplicationConStr"] = conn;
                LibDB2.DB db = new LibDB2.DB(conn);

                Session["UsuarioId"] = null;
                ParametroOperator.ClearCache();
                ddlServidoresBind();
#if DEBUG
                txtLogin.Text = "x";
                txtPassword.Text = "x";
#endif
            }
        }

        protected void ddlServidoresBind()
        {
            foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                //if (c.Name != "LocalSqlServer" && c.Name != "LocalMySqlServer")
                //{
                //    ListItem li = new ListItem("Edificio " + c.Name.ToUpperFirstLetter(), c.Name);
                //    ddlServidores.Items.Add(li);
                //}
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (ddlServidores.SelectedValue == "0")
            //{
            //    AlertaRoja("Edificio inválido");
            //    return;
            //}
          

            try
            {
#if DEBUG
                Session["UsuarioId"] = 1;
                string ReturnUrl2 = Convert.ToString(Request.QueryString["url"]);
                if (!string.IsNullOrEmpty(ReturnUrl2)) Response.Redirect(ReturnUrl2);
                string url2 = GetRouteUrl("Main", null); ;
                Response.Redirect(url2, false);
                return;
#endif
                Usuario u = UsuarioOperator.GetOneByLoginAndEstado(txtLogin.Text, 1);
                string encPass = (new CryptorEngine()).Encrypt(txtPassword.Text, true);
                int maxNroFallos = Convert.ToInt32(ParametroOperator.GetAll().Where(x => x.Name == "MaxLoginsIncorrectos").First().Value);
                if (encPass != u.Password || u.NroDeFallos > maxNroFallos) 
                {
                    if (u.NroDeFallos > maxNroFallos) throw new Exception("1");
                    u.NroDeFallos++;
                    UsuarioOperator.Save(u);
                    throw new Exception("2"); 
                }
                else
                {
                    u.NroDeFallos = 0;
                    UsuarioOperator.Save(u);
                    Session["UsuarioId"] = u.UsuarioId;
                    //log.Debug($"Ingreso de {u.LoginName}(uid={u.UsuarioId})");
                    string ReturnUrl = Convert.ToString(Request.QueryString["url"]);
                    if (!string.IsNullOrEmpty(ReturnUrl)) Response.Redirect(ReturnUrl);
                    string url = GetRouteUrl("Main", null); ;
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "1") AlertaRoja("El usuario está deshabilitado");
                else AlertaRoja("Usuario o clave incorrectos");
                //log.Debug($"Login fallido {txtLogin.Text}");
            }
        }

        #region AlertaRoja
        protected void AlertaRoja(string s)
        {
            lblAlerta.Text = s;
            divAlerta.Visible = true;
            divAlerta.Attributes.Add("class", "row alert alert-danger w-75");
            //divPpal.Attributes["style"] = "display:none";
        }
        protected void btnCloseAlerta_Click(object sender, EventArgs e)
        {
            divAlerta.Visible = false;
            //divPpal.Attributes["style"] = "display:block";
        }

        #endregion AlertaRoja

        
    }
}