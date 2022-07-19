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
    public partial class UsuarioBrowse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
#if DEBUG
                Session["UsuarioId"] = 1;
#endif
                if (string.IsNullOrEmpty(Convert.ToString(Session["UsuarioId"]))) Response.Redirect("~/app/Seguridad/UsuarioLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                if (!PermisoOperator.TienePermiso(Convert.ToInt32(Session["UsuarioId"]), GetType().BaseType.FullName)) throw new PermisoException();

                grdUsuariosBind();
                
            }
            
        }
        protected void grdUsuariosBind()
        {
            List<Usuario> lista = UsuarioOperator.GetAllEstado1().OrderBy(x => x.LoginName).ToList();
            grdUsuarios.DataSource = lista;
            grdUsuarios.DataBind();
        }

        protected void grdUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandNameDelete")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int colindex = CCLib.GetColumnIndexByHeaderText((GridView)sender, "UsuarioId");
                int id = Convert.ToInt32(row.Cells[colindex].Text);
                UsuarioOperator.Delete(id);
                grdUsuariosBind();
            }
            if (e.CommandName == "CommandNameEdit")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int colindex = CCLib.GetColumnIndexByHeaderText((GridView)sender, "UsuarioId");
                int id = Convert.ToInt32(row.Cells[colindex].Text);
                //string url = "~/app/Seguridad/UsuarioEdit.aspx?id=" + id.ToString();
                //string url = "/edita-usuario/" + id.ToString();
                string url = GetRouteUrl("EditaUsuario", new { id = id.ToString() });
                //Server.Transfer(url); esto no se puede usar porque cuando la pag url da excepcion de permiso vuelve aca
                Response.Redirect(url);
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/edita-usuario");
            Response.Redirect(GetRouteUrl("NuevoUsuario", null));
        }

        protected void grdUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}