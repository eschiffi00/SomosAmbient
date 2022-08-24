using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades.Entities;
using DbEntidades.Operators;
using System.Globalization;
using System.Data;
using System.Text.RegularExpressions;

namespace WebApplication.app.StockNS
{
    public partial class ItemsEdit : System.Web.UI.Page
    {
        Items seItems = null;
        DataTable tablagrid = new DataTable();
        List<int> listaitem = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tablagrid.Columns.Add(new DataColumn("Item", typeof(string)));
                //tablagrid.Columns.Add(new DataColumn("Cantidad", typeof(string)));
                //tablagrid.Columns.Add(new DataColumn("Peso", typeof(string)));
                Session["tablagrid"] = tablagrid;
                Session["listaitem"] = listaitem;
                AddNewRowToGrid(sender, e);

                if (string.IsNullOrEmpty(Convert.ToString(Session["UsuarioId"]))) Response.Redirect("~/app/Seguridad/UsuarioLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                if (!PermisoOperator.TienePermiso(Convert.ToInt32(Session["UsuarioId"]), GetType().BaseType.FullName)) throw new PermisoException();

                SessionClearAll();
                chkDetalle.InputAttributes.Add("class", "customCheck");
                string s;
                object o = Page.RouteData.Values["id"];
                if (o != null) s = Page.RouteData.Values["id"].ToString();
                else s = Request.QueryString["id"];
                CargaCategorias();
                CargaCuentas();
                CargaUnidades();
                //CargaItems();
                if (s != null && s != string.Empty)
                {
                    int uid = Convert.ToInt32(s);
                    seItems = ItemsOperator.GetOneByIdentity(uid);
                    //obtengo todas las categorias y utilizo descripcion y id
                    if (seItems.CategoriaItemId > 0)
                    {
                        ddlCategoriaId.SelectedValue = CategoriasOperator.GetOneByIdentity((int)seItems.CategoriaItemId).Id.ToString();
                    }
                    if(seItems.CuentaId > 0)
                    {
                        ddlCuenta.SelectedValue = CuentasOperator.GetOneByIdentity((int)seItems.CuentaId).Id.ToString();
                    }
                    if (seItems.DepositoId > 0)
                    {
                        ddlUnidad.SelectedValue = INVENTARIO_UnidadesOperator.GetOneByIdentity((INVENTARIO_ProductoOperator.GetOneByIdentity((int)seItems.DepositoId).Id)).Descripcion;
                    }
                        txtDescripcion.Text = seItems.Detalle;
                    //busco el stock para el StockID
                    //INVENTARIO_Producto stockCant = new INVENTARIO_Producto();
                    //stockCant = INVENTARIO_ProductoOperator.GetOneByIdentity(seItems.DepositoId);
                    //txtCantidad.Text = stockCant.Cantidad.ToString();
                    txtMargen.Text = seItems.Margen.ToString();
                    txtCosto.Text = seItems.Costo.ToString();
                    txtPrecio.Text = seItems.Precio.ToString();
                    btnSubmit.Text = "Grabar";
                    ddlEstado.SelectedValue = EstadosOperator.GetOneByIdentity(seItems.EstadoId).Id.ToString();
                }
                else
                {
                    ddlCategoriaId.Items.Insert(0, new ListItem("<Selecciona Categoria>", "0"));
                    ddlCuenta.Items.Insert(0, new ListItem("<Selecciona Cuenta Contable>", "0"));
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
            List<Categorias> categoriasList = CategoriasOperator.GetAll();
            ddlCategoriaId.DataSource = categoriasList;
            ddlCategoriaId.DataTextField = "Descripcion";
            ddlCategoriaId.DataValueField = "ID";
            ddlCategoriaId.DataBind();
        }
        //public void CargaItems()
        //{
        //    List<ItemsCombo> ItemsList = ItemsOperator.GetAllForCombo();
        //    ddlItems.DataSource = ItemsList;
        //    ddlItems.DataTextField = "Detalle";
        //    ddlItems.DataValueField = "Id";
        //    ddlItems.DataBind();
        //}
        public void CargaCuentas()
        {
            List<Cuentas> cuentasList = CuentasOperator.GetAll();
            ddlCuenta.DataSource = cuentasList;
            ddlCuenta.DataTextField = "Nombre";
            ddlCuenta.DataValueField = "ID";
            ddlCuenta.DataBind();
        }
        public void CargaUnidades()
        {
            List<INVENTARIO_Unidades> unidadesList = INVENTARIO_UnidadesOperator.GetAll();
            ddlUnidad.DataSource = unidadesList;
            ddlUnidad.DataTextField = "Descripcion";
            ddlUnidad.DataValueField = "ID";
            ddlUnidad.DataBind();
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
                seItems.CuentaId = Int32.Parse(ddlCuenta.Text);
                if (!chkDetalle.Checked)
                {
                    seItems.Margen = Decimal.Parse(txtMargen.Text, CultureInfo.InvariantCulture);
                    seItems.Costo = Decimal.Parse(txtCosto.Text, CultureInfo.InvariantCulture);
                    seItems.Precio = Decimal.Parse(txtPrecio.Text, CultureInfo.InvariantCulture);
                    
                }
                else
                {
                    ///se debe calcular el margen,costo y precio de la siguiente manera
                    ///cada item que compone la coleccion debera tener asociada una cantidad
                    ///se debe multiplicar la cantidad por el margen,precio,costo de ese item de coleccion
                    ///los mismos se deben aplicar al item
                    seItems.Margen = 0;
                    seItems.Costo = 0;
                    seItems.Precio = 0;
                }
                    
                seItems.EstadoId = Int32.Parse(ddlEstado.Text);
                INVENTARIO_Producto ItemsStock = new INVENTARIO_Producto();
                if (seItems.Id > 0) //Items existente
                {
                    //ItemsStock.Id = (int)seItems.DepositoId;
                    //ActualizaStock(ItemsStock);
                }
                else ///////////Items NUEVO\\\\\\\\\\\\\\
                {
                    //seItems.DepositoId = ActualizaStock(ItemsStock);
                    seItems.DepositoId = 0;
                    seItems.EstadoId = EstadosOperator.GetHablitadoID();
                    seItems.ItemDetalleId = -1;
                    var newItemId = ItemsOperator.Save(seItems).Id;
                    if (chkDetalle.Checked)
                    {
                        foreach (GridViewRow fila in GridView1.Rows)
                        {
                            ItemDetalle detalle = new ItemDetalle();
                            var codigo = ((DropDownList)fila.FindControl("ddlItems")).Text;
                            detalle.ItemDetalleId = newItemId;
                            detalle.ItemId = Int32.Parse(codigo);
                            ItemDetalleOperator.Save(detalle);
                        }
                    }
                    string url = GetRouteUrl("ListaItems", null);
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
            Item.UnidadId = 1;//Int32.Parse(ddlUnidad.SelectedValue);
            Item.UnidadPresentacionId = 1;
            Item.UnidadMedidaConversionId = 1;
            Item.TipoMovimientoId = 1;
            Item.CentroCostoId = 1;
            Item.UpdateDate = null;
            if (Item.Id > -1)
            {
                Item.UpdateDate = DateTime.Now;
            }
            else
            {
                Item.CreateDate = DateTime.Now;
            }
            Item.Delete = 1;
            Item.DeleteDate = null;
            Item = INVENTARIO_ProductoOperator.Save(Item);
            return Item.Id;
        }
        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            tablagrid = (DataTable)Session["tablagrid"];
            int id = Int32.Parse(((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlItems")).Text);

            var idborrar = ItemDetalleOperator.GetOneRelative(id, seItems.Id);
            if (idborrar > 0)
            {
                ItemDetalleOperator.Delete(id);

            }
            tablagrid.Rows.RemoveAt(e.RowIndex);
            //foreach (DataRow fila in tablagrid.Rows)
            //{
            //    if (fila["Id"].ToString() == id.ToString())
            //    {
            //        fila.Delete();
            //    }
            //}
            tablagrid.AcceptChanges();
            GridView1.DataSource = tablagrid;
            GridView1.DataBind();
            
        }
            
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList mydrop = (DropDownList)e.Row.FindControl("ddlItems");
                mydrop.DataSource = ItemsOperator.GetAllForCombo().OrderBy(x => x.Detalle).ToList(); ;
                mydrop.DataTextField = "Detalle";
                mydrop.DataValueField = "Id";
                mydrop.DataBind();
            }
        }

        //////////////////////////////////
        //COMIENZA CODIGO FILAS DINAMICAS/
        //////////////////////////////////
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid(sender, e);
        }
        public void AddNewRowToGrid(object sender, EventArgs e)
        {
            tablagrid = (DataTable)Session["tablagrid"];
            DataRow gridrow;
            gridrow = tablagrid.NewRow();
            listaitem = (List<int>)Session["listaitem"];

            if (GridView1.Rows.Count > 0)
            {
                var indice = 0;
                foreach (GridViewRow fila in GridView1.Rows)
                {
                    tablagrid.Rows[indice]["Item"] = ((DropDownList)fila.FindControl("ddlItems")).Text;
                    //tablagrid.Rows[indice]["Cantidad"] = ((TextBox)fila.FindControl("Cantidad")).Text;
                    //tablagrid.Rows[indice]["Peso"] = ((TextBox)fila.FindControl("Peso")).Text;

                    listaitem[indice] = Int32.Parse(((DropDownList)fila.FindControl("ddlItems")).Text);
                    indice++;
                }
            }
            gridrow = tablagrid.NewRow();
            gridrow["Item"] = "1";
            //gridrow["Peso"] = "1";
            //gridrow["Cantidad"] = "1";
            tablagrid.Rows.Add(gridrow);
            listaitem.Add(0);
            GridView1.DataSource = tablagrid;
            GridView1.DataBind();
            Session["tablagrid"] = tablagrid;
            Session["listaitem"] = listaitem;

            //Set Previous Data on Postbacks
            if (GridView1.Rows.Count > 0)
            {
                SetPreviousData();
            }

        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (Session["tablagrid"] != null)
            {
                DataTable dt = (DataTable)Session["tablagrid"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList box1 = (DropDownList)GridView1.Rows[rowIndex].Cells[1].FindControl("ddlItems");
                        //TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("Peso");
                        //TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("Cantidad");

                        box1.SelectedValue = dt.Rows[i]["Item"].ToString();
                        //box2.Text = dt.Rows[i]["Cantidad"].ToString();
                        //box3.Text = dt.Rows[i]["Peso"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
    }

}