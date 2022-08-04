using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades.Entities;
using DbEntidades.Operators;
using System.Globalization;

namespace WebApplication.app.StockNS
{
    public partial class ItemEdit : System.Web.UI.Page
    {
        Item seItem = null;

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
                    int uid = Convert.ToInt32(s);
                    seItem = ItemOperator.GetOneByIdentity(uid);
                    //EMA00
                    //AGREGAR BUSQUEDA DE CUENTAS CONTABLES BBDD
                    ListItem i;
                    i = new ListItem("Nuevo leon", "1");
                    ddlCuentaID.Items.Add(i);
                    txtDescripcion.Text = seItem.Descripcion;
                    //busco el stock para el StockID
                    Stock stockCant = new Stock();
                    stockCant = StockOperator.GetOneByIdentity(seItem.StockID);
                    if(stockCant.Cantidad == null)
                    {
                        txtPeso.Text = stockCant.Peso.ToString() ;

                    }
                    else
                    {
                        txtCantidad.Text = stockCant.Cantidad.ToString();
                    }

                    btnSubmit.Text = "Modifica Item";
                }
                else
                {
                    ListItem i;
                    i = new ListItem("Nuevo leon", "1");
                    ddlCuentaID.Items.Add(i);
                    h4Titulo.InnerText = "Creación de nuevo Item";
                    seItem = new Item();
                }
                SetMaximosTextBoxes();
                SessionSaveAll();
            }
            SessionLoadAll();
        }

        protected void SetMaximosTextBoxes()
        {
            txtDescripcion.MaxLength = ItemOperator.MaxLength.Descripcion;
        }

        #region Session
        protected void SessionClearAll()
        {
            Session["seItem"] = null;
        }
        protected void SessionLoadAll()
        {
            seItem = (Item)Session["seItem"];
        }
        protected void SessionSaveAll()
        {
            Session["seItem"] = seItem;
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
            try
            {
                seItem.CuentaID = Int32.Parse(ddlCuentaID.Text);
                seItem.Descripcion = txtDescripcion.Text;
                Stock ItemStock = new Stock();
                if (seItem.ID > 0) //Item existente
                {
                    ItemStock.ID = seItem.StockID;
                    ActualizaStock(ItemStock);
                    ItemOperator.Save(seItem);
                    string url = GetRouteUrl("ListaItems", null);
                    Response.Redirect(url);
                    if(seItem.ProItemID < 0)
                    {
                        seItem.ProItemID = null;
                    }
                }
                else ///////////ITEM NUEVO\\\\\\\\\\\\\\
                {
                    seItem.StockID = ActualizaStock(ItemStock);
                    seItem.CuentaID = Int32.Parse(ddlCuentaID.Text);
                    seItem.ProItemID = null;
                    seItem.EstadoID = EstadoOperator.GetHablitadoID();
                    ItemOperator.Save(seItem);
                    string url = GetRouteUrl("ListaItems", null);
                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                AlertaRoja(ex.Message);
            }
        }
        public int ActualizaStock(Stock ItemStock)
        {
            if(txtCantidad.Text != "")
            {
                ItemStock.Cantidad = Int32.Parse(txtCantidad.Text);
                ItemStock.Peso = null;
            }else if(txtPeso.Text != "")
            {
                ItemStock.Peso = Decimal.Parse(txtPeso.Text, CultureInfo.InvariantCulture);
                ItemStock.Cantidad = null;
            }
            ItemStock = StockOperator.Save(ItemStock);
            return ItemStock.ID;
        }
    }

}