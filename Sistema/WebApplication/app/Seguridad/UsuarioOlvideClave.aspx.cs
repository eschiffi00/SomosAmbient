using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades.Operators;
using DbEntidades.Entities;
using System.Configuration;
using System.IO;

namespace WebApplication.app.Seguridad
{
    public partial class UsuarioOlvideClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlServidoresBind();
                LibDB2.DB.deshabilita_encripcion = false;
                Session["Edificio"] = ddlServidores.SelectedValue.ToUpperFirstLetter();
                string conn = System.Configuration.ConfigurationManager.ConnectionStrings[ddlServidores.SelectedValue].ConnectionString;
                Session["EdificioConStr"] = conn;
                LibDB2.DB db = new LibDB2.DB(conn);
            }
        }
        #region AlertaRoja
        protected void AlertaRoja(string s)
        {
            lblAlerta.Text = s;
            divAlerta.Visible = true;
            divAlerta.Attributes.Add("class", "row alert alert-danger");
            divPpal.Attributes["style"] = "display:none";
        }
        protected void btnCloseAlerta_Click(object sender, EventArgs e)
        {
            divAlerta.Visible = false;
            divPpal.Attributes["style"] = "display:block";
        }
        #endregion AlertaRoja
        protected void ddlServidoresBind()
        {
            foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                if (c.Name != "LocalSqlServer" && c.Name != "LocalMySqlServer")
                {
                    ListItem li = new ListItem("Edificio " + c.Name.ToUpperFirstLetter(), c.Name);
                    ddlServidores.Items.Add(li);
                }
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Usuario u = UsuarioOperator.GetOneByEmailAndEstado(txtEmail.Text, 1);
            if (u == null)
            {
                AlertaRoja("El email no pertenece a ningún usuario habilitado");
            }
            else
            {
                CCLib.EnviaMailOlvideClaveBondi(Session["Edificio"].ToString(), txtEmail.Text, File.ReadAllText(Server.MapPath("~/app/MailRecuperacionClave.html")));
                AlertaRoja("Se enviaron las instrucciones de recuperación de contraseña a su casilla de correo.<br/>Puede cerrar esta ventana.");
                btnCloseAlerta.Visible = false;
            }
        }
    }
}