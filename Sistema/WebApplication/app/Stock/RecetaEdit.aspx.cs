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
    public partial class ProductoEdit : System.Web.UI.Page
    {
        Producto seProducto = null;

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
                CargaCategorias();
                if (s != null && s != string.Empty)
                {
                    int uid = Convert.ToInt32(s);
                    seProducto = ProductoOperator.GetOneByIdentity(uid);
                    //obtengo todas las categorias y utilizo descripcion y id
                    Categoria categoriaEdicion = CategoriaOperator.GetOneByIdentity(seProducto.CategoriaID);
                    ddlCategoriaID.Items.Insert(0, new ListItem(categoriaEdicion.Descripcion, categoriaEdicion.ID.ToString()));
                    txtDescripcion.Text = seProducto.Descripcion;
                    //busco el stock para el StockID
                    Stock stockCant = new Stock();
                    stockCant = StockOperator.GetOneByIdentity(seProducto.StockID);
                    txtCantidad.Text = stockCant.Cantidad.ToString();
                    txtMargen.Text = seProducto.Margen.ToString();
                    txtCosto.Text = seProducto.Costo.ToString();
                    txtPrecio.Text = seProducto.Precio.ToString();
                    btnSubmit.Text = "Modifica Producto";
                }
                else
                {
                    ddlCategoriaID.Items.Insert(0, new ListItem("<Selecciona Categoria>", "0"));
                    h4Titulo.InnerText = "Creación de nuevo Producto";
                    seProducto = new Producto();
                }
                SetMaximosTextBoxes();
                SessionSaveAll();
            }
            SessionLoadAll();
        }

        public void CargaCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            categorias = CategoriaOperator.GetAll();
            ddlCategoriaID.DataSource = categorias;
            ddlCategoriaID.DataTextField = "Descripcion";
            ddlCategoriaID.DataValueField = "ID";
            ddlCategoriaID.DataBind();
        }

        protected void SetMaximosTextBoxes()
        {
            txtDescripcion.MaxLength = ProductoOperator.MaxLength.Descripcion;
        }

        #region Session
        protected void SessionClearAll()
        {
            Session["seProducto"] = null;
        }
        protected void SessionLoadAll()
        {
            seProducto = (Producto)Session["seProducto"];
        }
        protected void SessionSaveAll()
        {
            Session["seProducto"] = seProducto;
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
                seProducto.CategoriaID = Int32.Parse(ddlCategoriaID.Text);
                seProducto.Descripcion = txtDescripcion.Text;
                seProducto.Margen = Decimal.Parse(txtMargen.Text, CultureInfo.InvariantCulture);
                seProducto.Costo = Decimal.Parse(txtCosto.Text, CultureInfo.InvariantCulture);
                seProducto.Precio = Decimal.Parse(txtPrecio.Text, CultureInfo.InvariantCulture);
                Stock ProductoStock = new Stock();
                if (seProducto.ID > 0) //producto existente
                {
                    ProductoStock.ID = seProducto.StockID;
                    ActualizaStock(ProductoStock);
                }
                else ///////////Producto NUEVO\\\\\\\\\\\\\\
                {
                    seProducto.StockID = ActualizaStock(ProductoStock);
                    seProducto.EstadoID = EstadoOperator.GetHablitadoID();
                    ProductoOperator.Save(seProducto);
                    string url = GetRouteUrl("ListaProductos", null);
                    Response.Redirect(url);
                }
                ProductoOperator.Save(seProducto);
            }
            catch (Exception ex)
            {
                AlertaRoja(ex.Message);
            }
        }
        public int ActualizaStock(Stock ProductoStock)
        {
            if (txtCantidad.Text != "")
            {
                ProductoStock.Cantidad = Int32.Parse(txtCantidad.Text);
                ProductoStock.Peso = null;
            }
            else if (txtPeso.Text != "")
            {
                ProductoStock.Peso = Decimal.Parse(txtPeso.Text, CultureInfo.InvariantCulture);
                ProductoStock.Cantidad = null;
            }
            ProductoStock = StockOperator.Save(ProductoStock);
            return ProductoStock.ID;
        }
    }

}