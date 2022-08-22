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

        public static ItemDetalle GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ItemDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ItemDetalle where Id = " + Id.ToString()).Tables[0];
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

        public static List<ItemDetalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ItemDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ItemDetalle> lista = new List<ItemDetalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ItemDetalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ItemDetalle itemDetalle = new ItemDetalle();
                foreach (PropertyInfo prop in typeof(ItemDetalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(itemDetalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(itemDetalle);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static ItemDetalle Save(ItemDetalle itemDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemDetalleSave")) throw new PermisoException();
            if (itemDetalle.Id == -1) return Insert(itemDetalle);
            else return Update(itemDetalle);
        }

        public static ItemDetalle Insert(ItemDetalle itemDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemDetalleSave")) throw new PermisoException();
            string sql = "insert into ItemDetalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ItemDetalle).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(itemDetalle, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted. values (" + valores + ")";
            DB db = new DB();
            List<object> parametros = new List<object>();
            for (int i = 0; i < param.Count; i++)
            {
                parametros.Add(param[i]);
                parametros.Add(valor[i]);
                SqlParameter p = new SqlParameter(param[i].ToString(), valor[i]);
                sqlParams.Add(p);
            }
            //object resp = db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            itemDetalle.Id = Convert.ToInt32(resp);
            return itemDetalle;
        }

        public static ItemDetalle Update(ItemDetalle itemDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemDetalleSave")) throw new PermisoException();
            string sql = "update ItemDetalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ItemDetalle).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(itemDetalle, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            sql += columnas;
            List<object> parametros = new List<object>();
            for (int i = 0; i<param.Count; i++)
            {
                parametros.Add(param[i]);
                parametros.Add(valor[i]);
                SqlParameter p = new SqlParameter(param[i].ToString(), valor[i]);
                sqlParams.Add(p);
        }
            sql += " where Id = " + itemDetalle.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return itemDetalle;
    }

        private static string GetComilla(string tipo)
        {
            switch (tipo) //son tipos de c#
            {
                case "Int32": return "";
                case "String": return "'";
                case "DateTime": return "'";
                case "Nullable`1": return "'";
            }
            return "";
        }

        public static string VerificaStringNull(string v)
        {
            return v == string.Empty ? null : v;
        }
    }
}
