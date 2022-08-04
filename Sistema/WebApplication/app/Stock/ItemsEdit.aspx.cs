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
    public partial class ItemsEdit : System.Web.UI.Page
    {
        Items seItems = null;

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
                    seItems = ItemsOperator.GetOneByIdentity(uid);
                    //obtengo todas las categorias y utilizo descripcion y id
                    Categoria categoriaEdicion = CategoriaOperator.GetOneByIdentity(seItems.CategoriaItemId);
                    ddlCategoriaId.Items.Insert(0, new ListItem(categoriaEdicion.Descripcion, categoriaEdicion.ID.ToString()));
                    txtDescripcion.Text = seItems.Detalle;
                    //busco el stock para el StockID
                    INVENTARIO_Producto stockCant = new INVENTARIO_Producto();
                    stockCant = INVENTARIO_ProductoOperator.GetOneByIdentity(seItems.DepositoId);
                    txtCantidad.Text = stockCant.Cantidad.ToString();
                    txtMargen.Text = seItems.Margen.ToString();
                    txtCosto.Text = seItems.Costo.ToString();
                    txtPrecio.Text = seItems.Precio.ToString();
                    btnSubmit.Text = "Modifica Items";
                }
                else
                {
                    ddlCategoriaId.Items.Insert(0, new ListItem("<Selecciona Categoria>", "0"));
                    h4Titulo.InnerText = "Creación de nuevo Item";
                    seItems = new Items();
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
            ddlCategoriaId.DataSource = categorias;
            ddlCategoriaId.DataTextField = "Descripcion";
            ddlCategoriaId.DataValueField = "ID";
            ddlCategoriaId.DataBind();
        }

        protected void SetMaximosTextBoxes()
        {
            txtDescripcion.MaxLength = ItemsOperator.MaxLength.Detalle;
        }

        #region Session
        protected void SessionClearAll()
        {
            Session["seItems"] = null;
        }
        protected void SessionLoadAll()
        {
            seItems = (Items)Session["seItems"];
        }
        protected void SessionSaveAll()
        {
            Session["seItems"] = seItems;
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
                seItems.CategoriaItemId = Int32.Parse(ddlCategoriaId.Text);
                seItems.Detalle = txtDescripcion.Text;
                seItems.Margen = Decimal.Parse(txtMargen.Text, CultureInfo.InvariantCulture);
                seItems.Costo = Decimal.Parse(txtCosto.Text, CultureInfo.InvariantCulture);
                seItems.Precio = Decimal.Parse(txtPrecio.Text, CultureInfo.InvariantCulture);
                INVENTARIO_Producto ItemsStock = new INVENTARIO_Producto();
                if (seItems.Id > 0) //Items existente
                {
                    ItemsStock.Id = seItems.DepositoId;
                    ActualizaStock(ItemsStock);
                }
                else ///////////Items NUEVO\\\\\\\\\\\\\\
                {
                    seItems.DepositoId = ActualizaStock(ItemsStock);
                    seItems.EstadoId = EstadoOperator.GetHablitadoID();
                    ItemsOperator.Save(seItems);
                    string url = GetRouteUrl("ListaItemss", null);
                    Response.Redirect(url);
                }
                ItemsOperator.Save(seItems);
            }
            catch (Exception ex)
            {
                AlertaRoja(ex.Message);
            }
        }
        public int ActualizaStock(INVENTARIO_Producto Item)
        {
            Item.RubroId = 1;
            Item.Codigo = "";
            Item.CodigoBarra = "";
            Item.Descripcion = "prueba";
            Item.CantidadNominal = 1.0m;
            Item.Cantidad = Decimal.Parse(txtCantidad.Text);
            Item.Costo = 1.0m;
            Item.UnidadId = Int32.Parse(ddlUnidad.SelectedValue);
            Item.UnidadPresentacionId = 1;
            Item.UnidadMedidaConversionId = 1;
            Item.TipoMovimientoId = 1;
            Item.CentroCostoId = 1;
            if(Item.Id > -1)
                Item.UpdateDate = DateTime.Now;
            else
                Item.CreateDate = DateTime.Now;

            Item.Delete = 0;
            Item.DeleteDate = DateTime.Now;
            Item = INVENTARIO_ProductoOperator.Save(Item);
            return Item.Id;
        }
    }

}