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

namespace WebApplication.app.StockNS
{
    public partial class ProductoBrowse : System.Web.UI.Page
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
            List<ItemsListado> ent = ItemsOperator.GetAllWithDetails().ToList();
            grdItems.DataSource = ent;
            grdItems.DataBind();
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevoItems_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("NuevoItems", null));
        }
        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("EditaItemss", null));
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        protected void grdItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}