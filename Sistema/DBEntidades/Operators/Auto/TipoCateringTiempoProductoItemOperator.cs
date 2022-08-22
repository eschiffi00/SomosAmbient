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
    public partial class TipoCateringTiempoProductoItemOperator
    {

        public static TipoCateringTiempoProductoItem GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringTiempoProductoItemBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCateringTiempoProductoItem).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCateringTiempoProductoItem where Id = " + Id.ToString()).Tables[0];
            TipoCateringTiempoProductoItem tipoCateringTiempoProductoItem = new TipoCateringTiempoProductoItem();
            foreach (PropertyInfo prop in typeof(TipoCateringTiempoProductoItem).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoCateringTiempoProductoItem, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoCateringTiempoProductoItem;
        }

        public static List<TipoCateringTiempoProductoItem> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringTiempoProductoItemBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCateringTiempoProductoItem).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoCateringTiempoProductoItem> lista = new List<TipoCateringTiempoProductoItem>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCateringTiempoProductoItem").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoCateringTiempoProductoItem tipoCateringTiempoProductoItem = new TipoCateringTiempoProductoItem();
                foreach (PropertyInfo prop in typeof(TipoCateringTiempoProductoItem).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoCateringTiempoProductoItem, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoCateringTiempoProductoItem);
            }
            return lista;
        }

		public static List<TipoCateringTiempoProductoItem> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<TipoCateringTiempoProductoItem> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<TipoCateringTiempoProductoItem> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<TipoCateringTiempoProductoItem> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {


        }

        public static TipoCateringTiempoProductoItem Save(TipoCateringTiempoProductoItem tipoCateringTiempoProductoItem)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringTiempoProductoItemSave")) throw new PermisoException();
            if (tipoCateringTiempoProductoItem.Id == -1) return Insert(tipoCateringTiempoProductoItem);
            else return Update(tipoCateringTiempoProductoItem);
        }

        public static TipoCateringTiempoProductoItem Insert(TipoCateringTiempoProductoItem tipoCateringTiempoProductoItem)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringTiempoProductoItemSave")) throw new PermisoException();
            string sql = "insert into TipoCateringTiempoProductoItem(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCateringTiempoProductoItem).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCateringTiempoProductoItem, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.Id values (" + valores + ")";
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
            tipoCateringTiempoProductoItem.Id = Convert.ToInt32(resp);
            return tipoCateringTiempoProductoItem;
        }

        public static TipoCateringTiempoProductoItem Update(TipoCateringTiempoProductoItem tipoCateringTiempoProductoItem)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringTiempoProductoItemSave")) throw new PermisoException();
            string sql = "update TipoCateringTiempoProductoItem set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCateringTiempoProductoItem).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCateringTiempoProductoItem, null));
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
            sql += " where Id = " + tipoCateringTiempoProductoItem.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoCateringTiempoProductoItem;
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
