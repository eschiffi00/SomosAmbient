using DbEntidades.Entities;
using DbEntidades.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace WebApplication.app.Administracion
{
    public partial class ItemBrowse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UsuarioId"]))) Response.Redirect("~/app/Seguridad/UsuarioLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                if (!PermisoOperator.TienePermiso(Convert.ToInt32(Session["UsuarioId"]), GetType().BaseType.FullName)) throw new PermisoException();
                grdItemsBind();

            }
        }

        protected void grdItemsBind()
        {
            List<ItemDetalle> ent = ItemOperator.GetAllWithDetails().ToList();
            grdItems.DataSource = ent;
            grdItems.DataBind();
        }
        protected void grdItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            int colindex = CCLib.GetColumnIndexByHeaderText((GridView)sender, "ID");
            int id = Convert.ToInt32(row.Cells[colindex].Text);

            if (e.CommandName == "CommandNameDelete")
            {
                ItemOperator.Delete(id);
                grdItemsBind();
            }
            if (e.CommandName == "CommandNameEdit")
            {
                string url = GetRouteUrl("EditaItems", new { id = id.ToString() });
                Response.Redirect(url);
            }
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevoItem_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("NuevoItem", null));
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        protected void grdItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}