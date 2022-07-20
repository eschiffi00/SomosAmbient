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
    public partial class RecetaEdit : System.Web.UI.Page
    {
        Receta seReceta = null; 
        RecetaDetalle seRecetaDetalle = null;
        static DropDownList[] arregloCombos;
        static int contadorControles;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblContador.Text = "";
                arregloCombos = new DropDownList[20];
                contadorControles = 0;
                try
                {
                    for (int i = 0; i < contadorControles; i++)
                        AgregarControles(arregloCombos[i]);
                }
                catch (Exception ex)
                {
                    //lblContador.Text = ex.Message;
                }
            
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
                    foreach(var ingrediente in detalleReceta)
                    {
                        CargaRecetas(ingrediente);
                    }
                    //busco el stock para el StockID
                    Stock stockCant = new Stock();
                    stockCant = StockOperator.GetOneByIdentity(seReceta.StockID);
                    txtCantidad.Text = stockCant.Cantidad.ToString();
                    
                    btnSubmit.Text = "Modifica Receta";
                }
                else
                {
                    h4Titulo.InnerText = "Creación de nuevo Receta";
                    
                }
                SetMaximosTextBoxes();
                SessionSaveAll();
            }
            SessionLoadAll();
        }
        public void CargaRecetas(REC_detalle ingrediente)
        {
                if (ingrediente.TipoRelacion == "item")
                {
                    seRecetaDetalle.Item.Add(ItemOperator.GetOneByIdentity(ingrediente.CodigoRelacion));
                }
                //FALTA CONTINUAR CON LAS SUB RECETAS
                //if (ingrediente.TipoRelacion == "receta")
                //{
                //    pasos.Add(RecetaOperator.GetOneByIdentity(ingrediente.CodigoRelacion));
                //    CargaRecetas(ingrediente);
                //}
        }
        protected void SetMaximosTextBoxes()
        {
            txtDescripcion.MaxLength = RecetaOperator.MaxLength.Descripcion;
        }

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
                Stock RecetaStock = new Stock();
                if (seReceta.ID > 0) //Receta existente
                {
                    RecetaStock.ID = seReceta.StockID;
                    ActualizaStock(RecetaStock);
                }
                else ///////////Receta NUEVO\\\\\\\\\\\\\\
                {
                    seReceta.StockID = ActualizaStock(RecetaStock);
                    seReceta.EstadoID = EstadoOperator.GetHablitadoID();
                    RecetaOperator.Save(seReceta);
                    string url = GetRouteUrl("ListaRecetas", null);
                    Response.Redirect(url);
                }
                RecetaOperator.Save(seReceta);
            }
            catch (Exception ex)
            {
                AlertaRoja(ex.Message);
            }
        }
        public int ActualizaStock(Stock RecetaStock)
        {
            if (txtCantidad.Text != "")
            {
                RecetaStock.Cantidad = Int32.Parse(txtCantidad.Text);
                RecetaStock.Peso = null;
            }
            else if (txtPeso.Text != "")
            {
                RecetaStock.Peso = Decimal.Parse(txtPeso.Text, CultureInfo.InvariantCulture);
                RecetaStock.Cantidad = null;
            }
            RecetaStock = StockOperator.Save(RecetaStock);
            return RecetaStock.ID;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int numeroRegistro = contadorControles;
                DropDownList nuevoCmb = new DropDownList();
                nuevoCmb.ID = "cmb" + numeroRegistro.ToString();
                nuevoCmb.Items.Add("---Seleccione el Plazo---");
                nuevoCmb.Items.Add("Corto Plazo");
                nuevoCmb.Items.Add("Mediano Plazo");
                nuevoCmb.Items.Add("Largo Plazo");
                nuevoCmb.SelectedIndex = 0;
                arregloCombos[numeroRegistro] = nuevoCmb;
                AgregarControles(nuevoCmb);
                contadorControles++;
            }
            catch (Exception ex)
            {
                //lblContador.Text = ex.Message;
            }
        }
        protected void AgregarControles(DropDownList cmb)
        {
            try
            {
                PanelReceta.Controls.Add(cmb);
                PanelReceta.Controls.Add(new LiteralControl(""));
            }
            catch (Exception ex)
            {
                //lblContador.Text = ex.Message;
            }
        }
    }

}