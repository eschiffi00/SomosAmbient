#undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades;
using DbEntidades.Entities;
using DbEntidades.Operators;

namespace WebApplication.app
{
    public partial class app : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            if (Session["WebApplication"] == null)
            {
                string url = GetRouteUrl("Login", null); ;
                Response.Redirect(url, false);
                return;
            }
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings[Session["WebApplication"].ToString()].ConnectionString;
            Session["WebApplicationConStr"] = conn;
            LibDB2.DB db = new LibDB2.DB(conn);
#if DEBUG
            if (Session["UsuarioId"] == null) Session["UsuarioId"] = "1";
            LibDB2.DB.deshabilita_encripcion = false;
            Session["WebApplication"] = "SomosAmbient";
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings[Session["WebApplication"].ToString()].ConnectionString;
            LibDB2.DB db = new LibDB2.DB(conn);
#endif
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //SeteaEdificioUsuario();
            if (!IsPostBack)
            {
                HabilitaMenues(Convert.ToInt32(Session["UsuarioId"]));
            }
        }
        public void SeteaEdificioUsuario() //se llama desde ReservasBrowse.aspx.cs
        {
            
            string s = "SetUserAndEdificio('" + UsuarioOperator.GetOneByIdentity(Convert.ToInt32(Session["UsuarioId"])).Nombre + "','Edificio: " + Session["WebApplication"].ToString() + "')";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "nombre", s, true);
        }
        public void QuitaFooter() //se llama desde ReservasBrowse.aspx.cs
        {
            footerDiv.Visible = false;
        }

        protected void HabilitaMenues(int usuarioId)
        {
            //cc: habilita temporalmente todos los menues
            //List<string> listaDeForms = PermisoOperator.ListaPermisos(usuarioId);
            List<string> listaDeForms = FormOperator.GetAll().Select(x => x.Nombre).ToList();
            foreach (string fName in listaDeForms)
            {
                string id = fName.Replace(".", "_");
                HyperLink h = (HyperLink)FindControl(id);
                if (h != null) h.Visible = true;
            }
        }

        protected void searchcommand_TextChanged(object sender, EventArgs e)
        {
            string s = searchcommand.Text;
            if (s == "version") s = "ver";
            Response.Redirect("/" + s);
        }

        protected string GetCaractName(string caract)
        {
            string s = DbEntidades.Operators.ParametroOperator.GetAll().Where(x => x.Name == caract).FirstOrDefault().Value;
            return Utility.GetPlural(s);
            
        }

        
    }
}