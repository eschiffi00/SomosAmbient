using DbEntidades.Entities;
using DbEntidades.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.app.Seguridad
{
    public partial class UsuarioEdit : System.Web.UI.Page
    {
        Usuario seUsuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UsuarioId"]))) Response.Redirect("~/app/Seguridad/UsuarioLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                if (!PermisoOperator.TienePermiso(Convert.ToInt32(Session["UsuarioId"]), GetType().BaseType.FullName)) throw new PermisoException();

                SessionClearAll();

                string s;
                object o = Page.RouteData.Values["id"];
                if (o != null) s = Page.RouteData.Values["id"].ToString();
                else s = Request.QueryString["id"];

                if (s != null && s != string.Empty)
                {
                    divPasswords.Visible = false;
                    int uid = Convert.ToInt32(s);
                    seUsuario = UsuarioOperator.GetOneByIdentity(uid);
                    txtLogin.Text = seUsuario.LoginName;
                    txtNombre.Text = seUsuario.Nombre;
                    txtEmail.Text = seUsuario.Email;
                    txtPassword1.Text = seUsuario.Password;
                    txtPassword1.Text = seUsuario.Password;
                    lblNroFallos.Text = seUsuario.NroDeFallos.ToString();
                    ddlEsAdmin.SelectedValue = seUsuario.EsAdmin.ToString();
                    txtFeCreacion.Text = seUsuario.FeCreacion.ToShortDateString();
                    txtCreadoPor.Text = UsuarioOperator.GetOneByIdentity(seUsuario.UsuarioIdCreador).Nombre;
                    btnSubmit.Text = "Modifica usuario";
                }
                else
                {
                    h4Titulo.InnerText = "Creación de nuevo usuario";
                    divNroFallos.Visible = false;
                    divFeCreacion.Visible = false;
                    divCreadoPor.Visible = false;
                    seUsuario = new Usuario();
                }
                SetMaximosTextBoxes();
                SessionSaveAll();
            }
            SessionLoadAll();
        }

        protected void SetMaximosTextBoxes()
        {
            txtLogin.MaxLength = UsuarioOperator.MaxLength.LoginName;
            txtNombre.MaxLength = UsuarioOperator.MaxLength.Nombre;
            txtEmail.MaxLength = UsuarioOperator.MaxLength.Email;
            txtPassword1.MaxLength = UsuarioOperator.MaxLength.Password;
            txtPassword2.MaxLength = UsuarioOperator.MaxLength.Password;
        }

        #region Session
        protected void SessionClearAll()
        {
            Session["seUsuario"] = null;
        }
        protected void SessionLoadAll()
        {
            seUsuario = (Usuario)Session["seUsuario"];
        }
        protected void SessionSaveAll()
        {
            Session["seUsuario"] = seUsuario;
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            SessionSaveAll();
            base.OnPreRenderComplete(e);
        }
        #endregion Session

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region Validacion code behind
            /* Esto es un validador de code behind
            if (txtPassword1.Text != txtPassword2.Text)
            {
                txtPassword2.CssClass = "form-control is-invalid";
                //txtNombre.CssClass = "form-control is-invalid";
                return;
            }
            else txtPassword2.CssClass = "form-control";
            */
            #endregion

            try
            {
                if (seUsuario.UsuarioId > 0) //usuario existente
                {
                    seUsuario.LoginName = txtLogin.Text;
                    seUsuario.Nombre = txtNombre.Text;
                    seUsuario.Email = txtEmail.Text;
                    //seUsuario.Password = txtPassword1.Text;
                    seUsuario.NroDeFallos = Convert.ToInt32(lblNroFallos.Text);
                    seUsuario.EsAdmin = Convert.ToInt32(ddlEsAdmin.SelectedValue);
                    UsuarioOperator.Save(seUsuario);
                    //string url = "~/app/Seguridad/UsuarioBrowse.aspx";
                    string url = GetRouteUrl("ListaUsuarios", null);
                    Response.Redirect(url);
                }
                else //usuario nuevo
                {
                    int uid = Convert.ToInt32(Session["UsuarioId"].ToString());
                    seUsuario.LoginName = txtLogin.Text;
                    seUsuario.Nombre = txtNombre.Text;
                    seUsuario.Email = txtEmail.Text;
                    CryptorEngine c = new CryptorEngine();
                    seUsuario.Password = (new CryptorEngine()).Encrypt(txtPassword1.Text, true);
                    seUsuario.NroDeFallos = 0;
                    seUsuario.EsAdmin = Convert.ToInt32(ddlEsAdmin.SelectedValue);
                    seUsuario.EstadoId = 1;
                    seUsuario.FeCreacion = DateTime.Now;
                    seUsuario.UsuarioIdCreador = uid;
                    UsuarioOperator.Save(seUsuario);
                    //string url = "~/app/Seguridad/UsuarioBrowse.aspx";
                    string url = GetRouteUrl("ListaUsuarios", null);
                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                AlertaRoja(ex.Message);
            }
        }

        protected void btnVuelveACero_Click(object sender, EventArgs e)
        {
            lblNroFallos.Text = "0";
            seUsuario.NroDeFallos = 0;
            UsuarioOperator.Save(seUsuario);
        }

        protected void btnCambiaClave_Click(object sender, EventArgs e)
        {
            //string url = "~/app/Seguridad/UsuarioClave.aspx?id=" + (new CryptorEngine()).Encrypt(seUsuario.LoginName, true);
            //string url = "/clave-usuario/" + (new CryptorEngine()).Encrypt(seUsuario.LoginName, true);
            string url = GetRouteUrl("ClaveUsuario", new { id = CCLib.Base64Encode((new CryptorEngine()).Encrypt(Session["Edificio"] + ";" +  seUsuario.LoginName, true)) });
            Response.Redirect(url);
        }
    }
}