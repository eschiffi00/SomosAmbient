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
    public partial class AdicionalesItemsOperator
    {

        public static AdicionalesItems GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesItemsBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(AdicionalesItems).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from AdicionalesItems where Id = " + Id.ToString()).Tables[0];
            AdicionalesItems adicionalesItems = new AdicionalesItems();
            foreach (PropertyInfo prop in typeof(AdicionalesItems).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(adicionalesItems, value, null); }
                catch (System.ArgumentException) { }
            }
            return adicionalesItems;
        }

        public static List<AdicionalesItems> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesItemsBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(AdicionalesItems).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<AdicionalesItems> lista = new List<AdicionalesItems>();
            DataTable dt = db.GetDataSet("select " + columnas + " from AdicionalesItems").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                AdicionalesItems adicionalesItems = new AdicionalesItems();
                foreach (PropertyInfo prop in typeof(AdicionalesItems).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(adicionalesItems, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(adicionalesItems);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static AdicionalesItems Save(AdicionalesItems adicionalesItems)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesItemsSave")) throw new PermisoException();
            if (adicionalesItems.Id == -1) return Insert(adicionalesItems);
            else return Update(adicionalesItems);
        }

        public static AdicionalesItems Insert(AdicionalesItems adicionalesItems)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesItemsSave")) throw new PermisoException();
            string sql = "insert into AdicionalesItems(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(AdicionalesItems).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(adicionalesItems, null));
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
            adicionalesItems.Id = Convert.ToInt32(resp);
            return adicionalesItems;
        }

        public static AdicionalesItems Update(AdicionalesItems adicionalesItems)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesItemsSave")) throw new PermisoException();
            string sql = "update AdicionalesItems set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(AdicionalesItems).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(adicionalesItems, null));
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
            sql += " where Id = " + adicionalesItems.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return adicionalesItems;
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
