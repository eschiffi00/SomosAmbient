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
    public partial class ProveedorBrowse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UsuarioId"]))) Response.Redirect("~/app/Seguridad/UsuarioLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                if (!PermisoOperator.TienePermiso(Convert.ToInt32(Session["UsuarioId"]), GetType().BaseType.FullName)) throw new PermisoException();
                grdProveedoresBind();

            }
        }

        protected void grdProveedoresBind()
        {
            List<Entidades> ent = EntidadesOperator.GetAll().Where(x => x.IsProveedor == "1").ToList();
            grdProveedores.DataSource = ent;
            grdProveedores.DataBind();
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("NuevoProveedor", null));
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}