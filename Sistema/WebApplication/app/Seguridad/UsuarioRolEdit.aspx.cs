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
    public partial class UsuarioRolEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PermisoOperator.TienePermiso(Convert.ToInt32(Session["UsuarioId"]), GetType().BaseType.FullName)) throw new PermisoException();

                ddlUsuariosBind();
                grdRolesBind();

                ViewState["UsuarioRolEdit"] = Request.UrlReferrer;
            }
        }

        protected void ddlUsuariosBind()
        {
            List<Usuario> usuarios = UsuarioOperator.GetAll();
            ddlUsuarios.DataSource = usuarios;
            ddlUsuarios.DataTextField = "LoginName";
            ddlUsuarios.DataValueField = "UsuarioId";
            ddlUsuarios.DataBind();
        }

        protected void grdRolesBind()
        {
            List<Rol> roles = RolOperator.GetAll();
            grdRoles.DataSource = roles;
            grdRoles.DataBind();
        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdRolesBind();
        }

        protected void grdRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int usuarioId = Convert.ToInt32(ddlUsuarios.SelectedValue);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int colindex = CCLib.GetColumnIndexByHeaderText((GridView)sender, "RolId");
                int rolId = Convert.ToInt32(e.Row.Cells[colindex].Text);
                CheckBox cb = e.Row.FindControl("cbPermiso") as CheckBox;
                cb.Checked = UsuarioRolOperator.ExisteRolEnUsuario(usuarioId, rolId);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int usuarioId = Convert.ToInt32(ddlUsuarios.SelectedValue);
            UsuarioRolOperator.DeleteForUser(usuarioId);
            foreach(GridViewRow row in grdRoles.Rows)
            {
                int colindex = CCLib.GetColumnIndexByHeaderText(grdRoles, "RolId");
                int rolId = Convert.ToInt32(row.Cells[colindex].Text);
                CheckBox cb = row.FindControl("cbPermiso") as CheckBox;
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        UsuarioRol ur = new UsuarioRol();
                        ur.UsuarioId = usuarioId;
                        ur.RolId = rolId;
                        ur.EstadoId = 1;
                        UsuarioRolOperator.Save(ur);
                    }
                }
            }
            grdRolesBind();
        }
        /*
        protected void btnCance_Click(object sender, EventArgs e)
        {
            if (ViewState["UsuarioClave"] != null) Response.Redirect(ViewState["UsuarioClave"].ToString());
            else Response.Redirect("/main");
        }
        */
    }
}