using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbEntidades.Entities;
using DbEntidades.Operators;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;


namespace WebApplication.app.StockNS
{
    public partial class RecetaEdit : System.Web.UI.Page
    {
        Receta seReceta = new Receta();
        DataTable tablagrid = new DataTable();
        List<int> listaitem = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var inicio = 0;
                tablagrid.Columns.Add(new DataColumn("Item", typeof(string)));
                tablagrid.Columns.Add(new DataColumn("Cantidad", typeof(string)));
                tablagrid.Columns.Add(new DataColumn("Peso", typeof(string)));
                Session["tablagrid"] = tablagrid;
                Session["listaitem"] = listaitem;
                //LoadGrid(inicio);
                AddNewRowToGrid(sender,e);
                
                //ViewState["tablagrid"] = tablagrid;
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
                    seReceta = RecetaOperator.GetOneByIdentity(uid);
                    //obtengo una lista del detalle de la receta
                    List<REC_detalle> detalleReceta = RecetaOperator.GetDetalleById(uid);
                    //foreach(var ingrediente in detalleReceta)
                    //{
                    //    CargaRecetas(ingrediente);
                    //}
                    //busco el stock para el StockID
                    INVENTARIO_Producto stockCant = new INVENTARIO_Producto();
                    stockCant = INVENTARIO_ProductoOperator.GetOneByIdentity(seReceta.StockID);
                    //txtCantidad.Text = stockCant.Cantidad.ToString();
                    
                    btnSubmit.Text = "Modifica Receta";
                }
                else
                {
                    //SetInitialRow();
                    //esNuevo = true;
                    h4Titulo.InnerText = "Creación de nuevo Receta";
                    
                }
                GridView1Bind();
                //SetMaximosTextBoxes();
                SessionSaveAll();
            }
            else
            {

            }
            SessionLoadAll();
        }
        protected void GridView1Bind()
        {
            //if (esNuevo)
            //{
            //    List<ItemDetalle> nuevoItem = new List<ItemDetalle>();
            //    nuevoItem.Add(new ItemDetalle()
            //    {
            //        ID = 1,
            //        Descripcion = "",
            //        CuentaID = 1,
            //        CuentaDescripcion = "",
            //        StockID = 1,
            //        ProItemID = 1,
            //        EstadoID = 1,
            //        Cantidad = 0,
            //        Peso = 0
            //    });
            //    GridView1.DataSource = nuevoItem;
            //}
            //else
            //{
            //    List<RecetaDetalle> ent = RecetaOperator.GetAllWithDetails().Where(x => x.ID == seReceta.ID && x.EstadoID != 1).ToList();
            //    List<ItemDetalle> items = new List<ItemDetalle>();
            //    foreach (var x in ent)
            //    {
            //        foreach (var item in x.Item)
            //        {
            //            items.Add(new ItemDetalle()
            //            {
            //                ID = item.ID,
            //                Descripcion = item.Descripcion,
            //                CuentaID = item.CuentaID,
            //                CuentaDescripcion = "",
            //                StockID = item.StockID,
            //                ProItemID = item.ProItemID,
            //                EstadoID = item.EstadoID,
            //                Cantidad = StockOperator.GetOneByIdentity(item.StockID).Cantidad,
            //                Peso = StockOperator.GetOneByIdentity(item.StockID).Peso
            //            });
            //        }
            //    }
            //    GridView1.DataSource = items;
                
            //}
            //GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            int colindex = CCLib.GetColumnIndexByHeaderText((GridView)sender, "ID");
            int id = Convert.ToInt32(row.Cells[colindex].Text);

            if (e.CommandName == "CommandNameDelete")
            {
                //ItemOperator.Delete(id);
                GridView1Bind();
            }
            if (e.CommandName == "CommandNameEdit")
            {
                string url = GetRouteUrl("EditaItems", new { id = id.ToString() });
                Response.Redirect(url);
            }
        }
        
        //public void CargaRecetas(REC_detalle ingrediente)
        //{
        //        if (ingrediente.TipoRelacion == "item")
        //        {
        //            seRecetaDetalle.Item.Add(ItemOperator.GetOneByIdentity(ingrediente.CodigoRelacion));
        //        }
        //        //FALTA CONTINUAR CON LAS SUB RECETAS
        //        //if (ingrediente.TipoRelacion == "receta")
        //        //{
        //        //    pasos.Add(RecetaOperator.GetOneByIdentity(ingrediente.CodigoRelacion));
        //        //    CargaRecetas(ingrediente);
        //        //}
        //}
        //protected void SetMaximosTextBoxes()
        //{
        //    txtDescripcion.MaxLength = RecetaOperator.MaxLength.Descripcion;
        //}

        #region Session
        protected void SessionClearAll()
        {
            Session["seReceta"] = null;
        }
        protected void SessionLoadAll()
        {
            seReceta = (Receta)Session["seReceta"];
        }
        protected void SessionSaveAll()
        {
            Session["seReceta"] = seReceta;
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

                seReceta.Descripcion = txtDescripcion.Text;
                
                INVENTARIO_Producto RecetaStock = new INVENTARIO_Producto();
                if (seReceta.ID > 0) //Receta existente
                {
                    RecetaStock.Id = seReceta.StockID;
                    ActualizaStock(RecetaStock);
                }
                else ///////////Receta NUEVO\\\\\\\\\\\\\\
                {
                    seReceta.StockID = ActualizaStock(RecetaStock);
                    seReceta.EstadoID = EstadoOperator.GetHablitadoID();
                    RecetaOperator.Save(seReceta);
                    
                }
                seReceta = RecetaOperator.Save(seReceta);
                //se para los parrafos del texto y los graba uno a uno para evitar un overflow
                string[] pasos = TextBoxReceta.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for (var x = 0; x < pasos.Length; x++)
                {
                    REC_pasos paso = new REC_pasos();
                    paso.Orden = x.ToString();
                    paso.Paso = pasos[x];
                    paso.RecetaID = seReceta.ID;
                    REC_pasosOperator.Save(paso);
                }
                //00
                //recorro filas del grid y verifico si es item o receta
                //proximamente agregar una tabla ocultable que muestre items de la receta
                List<string> list = new List<string>();
                foreach (GridViewRow fila in GridView1.Rows)
                {
                    REC_detalle detalle = new REC_detalle();
                    var codigo = ((DropDownList)fila.FindControl("ddlItems")).Text;
                    Items item = ItemsOperator.GetOneByIdentity(Int32.Parse(codigo));
                    if (item.Id > 0)
                    {
                        detalle.TipoRelacion = "item";
                        detalle.CodigoRelacion = Int32.Parse(codigo);
                    }
                    else
                    {
                        detalle.TipoRelacion = "receta";
                        detalle.CodigoRelacion = Int32.Parse(codigo);

                    }
                    detalle.Cantidad = Int32.Parse(((TextBox)fila.FindControl("Cantidad")).Text);
                    detalle.Peso = Convert.ToDecimal(((TextBox)fila.FindControl("Peso")).Text);
                    detalle.RecetaID = seReceta.ID;

                    REC_detalleOperator.Save(detalle);
                }
                string url = GetRouteUrl("ListaRecetas", null);
                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                AlertaRoja(ex.Message);
            }
        }
        public int ActualizaStock(INVENTARIO_Producto Receta)
        {
            //00
            //actualiza el stock ya sea en peso o en cantidad no ambos
            Receta.RubroId = 1;
            Receta.Codigo = "";
            Receta.CodigoBarra = "";
            Receta.Descripcion = "prueba";
            Receta.CantidadNominal = 1.0m;
            Receta.Cantidad = Decimal.Parse(txtCantidad.Text);
            Receta.Costo = 1.0m;
            Receta.UnidadId = 1;//Int32.Parse(ddlUnidad.SelectedValue);
            Receta.UnidadPresentacionId = 1;
            Receta.UnidadMedidaConversionId = 1;
            Receta.TipoMovimientoId = 1;
            Receta.CentroCostoId = 1;
            if (Receta.Id > -1)
                Receta.UpdateDate = DateTime.Now;
            else
                Receta.CreateDate = DateTime.Now;

            Receta.Delete = 0;
            Receta.DeleteDate = DateTime.Now;
            Receta = INVENTARIO_ProductoOperator.Save(Receta);
            return Receta.Id;
        }
        
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList mydrop = (DropDownList)e.Row.FindControl("ddlItems");
                mydrop.DataSource = ItemsOperator.GetAllForCombo();
                mydrop.DataTextField = "Descripcion";
                mydrop.DataValueField = "ID";
                mydrop.DataBind();
            }
        }

        //////////////////////////////////
        //COMIENZA CODIGO FILAS DINAMICAS/
        //////////////////////////////////
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid(sender,e);
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
                    tablagrid.Rows[indice]["Cantidad"] = ((TextBox)fila.FindControl("Cantidad")).Text;
                    tablagrid.Rows[indice]["Peso"] = ((TextBox)fila.FindControl("Peso")).Text;
                    
                    listaitem[indice] = Int32.Parse(((DropDownList)fila.FindControl("ddlItems")).Text);
                    indice++;
                }
            }
            gridrow = tablagrid.NewRow();
            gridrow["Item"] = "1";
            gridrow["Peso"] = "1";
            gridrow["Cantidad"] = "1";
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
                        TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("Peso");
                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("Cantidad");

                        box1.SelectedValue = dt.Rows[i]["Item"].ToString();
                        box2.Text = dt.Rows[i]["Cantidad"].ToString();
                        box3.Text = dt.Rows[i]["Peso"].ToString();



                        rowIndex++;
                    }
                }
            }
        }
        
        //////////////////////////////
        //FIN CODIGO FILAS DINAMICAS//
        //////////////////////////////
    }

}