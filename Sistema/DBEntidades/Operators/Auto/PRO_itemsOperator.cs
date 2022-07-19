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
    public partial class PRO_itemsOperator
    {

        public static PRO_items GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPRO_itemsBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PRO_items).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from PRO_items where ID = " + ID.ToString()).Tables[0];
            PRO_items pRO_items = new PRO_items();
            foreach (PropertyInfo prop in typeof(PRO_items).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(pRO_items, value, null); }
                catch (System.ArgumentException) { }
            }
            return pRO_items;
        }

        public static List<PRO_items> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPRO_itemsBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PRO_items).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<PRO_items> lista = new List<PRO_items>();
            DataTable dt = db.GetDataSet("select " + columnas + " from PRO_items").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                PRO_items pRO_items = new PRO_items();
                foreach (PropertyInfo prop in typeof(PRO_items).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(pRO_items, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(pRO_items);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static PRO_items Save(PRO_items pRO_items)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPRO_itemsSave")) throw new PermisoException();
            if (pRO_items.ID == -1) return Insert(pRO_items);
            else return Update(pRO_items);
        }

        public static PRO_items Insert(PRO_items pRO_items)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPRO_itemsSave")) throw new PermisoException();
            string sql = "insert into PRO_items(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PRO_items).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pRO_items, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.ID values (" + valores + ")";
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
            pRO_items.ID = Convert.ToInt32(resp);
            return pRO_items;
        }

        public static PRO_items Update(PRO_items pRO_items)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPRO_itemsSave")) throw new PermisoException();
            string sql = "update PRO_items set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PRO_items).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pRO_items, null));
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
            sql += " where ID = " + pRO_items.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return pRO_items;
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
