using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades.Entities;
using DbEntidades.Operators;

namespace WebApplication
{
    public partial class GeneralError : System.Web.UI.Page
    {
        //readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected GeneralError()
        {
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Web.config")));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAlerta.Text = "Se ha producido un error en la última operación realizada.";
            Exception exc = Server.GetLastError();
            if (exc != null)
            {
                txtDetalle.Text = exc.Message;
                if (exc.InnerException != null)
                {
                    txtDetalle.Text += "\n" + exc.InnerException.Message;
                    if (exc.InnerException.InnerException != null) txtDetalle.Text += "\n" + exc.InnerException.InnerException.Message;
                    if (exc.InnerException is PermisoException) lblAlerta.Text = exc.InnerException.Message;
                }
                else txtDetalle.Text = exc.Message;
                //log.Debug(txtDetalle.Text);
            }
        }
    }
}