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
    public partial class CategoriasItemOperator
    {

        public static CategoriasItem GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasItemBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CategoriasItem).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CategoriasItem where Id = " + Id.ToString()).Tables[0];
            CategoriasItem categoriasItem = new CategoriasItem();
            foreach (PropertyInfo prop in typeof(CategoriasItem).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(categoriasItem, value, null); }
                catch (System.ArgumentException) { }
            }
            return categoriasItem;
        }

        public static List<CategoriasItem> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasItemBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CategoriasItem).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CategoriasItem> lista = new List<CategoriasItem>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CategoriasItem").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CategoriasItem categoriasItem = new CategoriasItem();
                foreach (PropertyInfo prop in typeof(CategoriasItem).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(categoriasItem, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(categoriasItem);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 500;


        }

        public static CategoriasItem Save(CategoriasItem categoriasItem)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasItemSave")) throw new PermisoException();
            if (categoriasItem.Id == -1) return Insert(categoriasItem);
            else return Update(categoriasItem);
        }

        public static CategoriasItem Insert(CategoriasItem categoriasItem)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasItemSave")) throw new PermisoException();
            string sql = "insert into CategoriasItem(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CategoriasItem).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(categoriasItem, null));
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
            categoriasItem.Id = Convert.ToInt32(resp);
            return categoriasItem;
        }

        public static CategoriasItem Update(CategoriasItem categoriasItem)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasItemSave")) throw new PermisoException();
            string sql = "update CategoriasItem set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CategoriasItem).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(categoriasItem, null));
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
            sql += " where Id = " + categoriasItem.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return categoriasItem;
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
