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
    public partial class ItemDetalleOperator
    {
        public static void Delete(int Id)
        {
            string query = "delete from ItemDetalle where Id = " + Id.ToString();
            DB db = new DB();
            db.ExecuteNonQuery(query);
        }
    
        public static ItemDetalle GetOneRelative(int IdItem,int IdRel)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ItemDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ItemDetalle where ItemId = " + IdItem.ToString() + "and ItemDetalleId = " + IdRel.ToString()).Tables[0];
            ItemDetalle itemDetalle = new ItemDetalle();
            foreach (PropertyInfo prop in typeof(ItemDetalle).GetProperties())
            {
                object value = dt.Rows[0][prop.Name];
                if (value == DBNull.Value) value = null;
                try { prop.SetValue(itemDetalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return itemDetalle;
        }
    }
}
