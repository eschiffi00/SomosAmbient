using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using DbEntidades.Entities;
using System.Data.SqlClient;
using LibDB2;

namespace DbEntidades.Operators
{
    public partial class ProductoOperator
    {
        public static List<ProductoDetalle> GetAllWithDetails()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Producto).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ProductoDetalle> lista = new List<ProductoDetalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Producto").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Producto Producto = new Producto();
                foreach (PropertyInfo prop in typeof(Producto).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(Producto, value, null); }
                    catch (System.ArgumentException) { }
                }
                if (Producto.StockID > 0)
                {
                    ProductoDetalle ProductoStock = new ProductoDetalle();
                    ProductoStock.ID = Producto.ID;
                    ProductoStock.Descripcion = Producto.Descripcion;
                    ProductoStock.CategoriaID = Producto.CategoriaID;
                    ProductoStock.CategoriaDescripcion = "Pendiente tabla cuentas";
                    ProductoStock.Costo = Producto.Costo;
                    ProductoStock.Margen = Producto.Margen;
                    ProductoStock.Precio = Producto.Precio;
                    ProductoStock.StockID = Producto.StockID;
                    ProductoStock.EstadoID = Producto.EstadoID;
                    ProductoStock.Cantidad = StockOperator.GetOneByIdentity(ProductoStock.StockID).Cantidad;
                    ProductoStock.Peso = StockOperator.GetOneByIdentity(ProductoStock.StockID).Peso;
                    lista.Add(ProductoStock);
                }
            }
            return lista;
        }
        public static void Delete(int id)
        {
            Producto u = GetOneByIdentity(id);
            u.EstadoID = EstadoOperator.GetDeshabilitadoID();
            Update(u);
        }
    }
}