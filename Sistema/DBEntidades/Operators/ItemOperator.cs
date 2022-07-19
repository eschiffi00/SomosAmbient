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
    public partial class ItemOperator
    {

        public static void Delete(int id)
        {
            Item u = GetOneByIdentity(id);
            u.EstadoID = EstadoOperator.GetDeshabilitadoID();
            Update(u);
        }
        public static List<ItemDetalle> GetAllWithDetails()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Item).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ItemDetalle> lista = new List<ItemDetalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Item").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Item item = new Item();
                foreach (PropertyInfo prop in typeof(Item).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(item, value, null); }
                    catch (System.ArgumentException) { }
                }
                if(item.StockID > 0)
                {
                    ItemDetalle itemStock = new ItemDetalle();
                    itemStock.ID = item.ID;
                    itemStock.Descripcion = item.Descripcion;
                    itemStock.CuentaID = item.CuentaID;
                    itemStock.CuentaDescripcion = "Pendiente tabla cuentas";
                    itemStock.StockID = item.StockID;
                    itemStock.ProItemID = item.ProItemID;
                    itemStock.EstadoID = item.EstadoID;
                    itemStock.Cantidad = StockOperator.GetOneByIdentity(itemStock.StockID).Cantidad;
                    itemStock.Peso = StockOperator.GetOneByIdentity(itemStock.StockID).Peso;
                    lista.Add(itemStock);
                }
            }
            return lista;
        }
    }
}
