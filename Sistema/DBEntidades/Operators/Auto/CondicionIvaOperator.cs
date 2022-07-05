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
    public partial class CondicionIvaOperator
    {

        public static CondicionIva GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIvaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CondicionIva).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CondicionIva where Id = " + Id.ToString()).Tables[0];
            CondicionIva condicionIva = new CondicionIva();
            foreach (PropertyInfo prop in typeof(CondicionIva).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(condicionIva, value, null); }
                catch (System.ArgumentException) { }
            }
            return condicionIva;
        }

        public static List<CondicionIva> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIvaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CondicionIva).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CondicionIva> lista = new List<CondicionIva>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CondicionIva").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CondicionIva condicionIva = new CondicionIva();
                foreach (PropertyInfo prop in typeof(CondicionIva).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(condicionIva, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(condicionIva);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 100;


        }

        public static CondicionIva Save(CondicionIva condicionIva)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIvaSave")) throw new PermisoException();
            if (condicionIva.Id == -1) return Insert(condicionIva);
            else return Update(condicionIva);
        }

        public static CondicionIva Insert(CondicionIva condicionIva)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIvaSave")) throw new PermisoException();
            string sql = "insert into CondicionIva(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CondicionIva).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(condicionIva, null));
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
            condicionIva.Id = Convert.ToInt32(resp);
            return condicionIva;
        }

        public static CondicionIva Update(CondicionIva condicionIva)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIvaSave")) throw new PermisoException();
            string sql = "update CondicionIva set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CondicionIva).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(condicionIva, null));
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
            sql += " where Id = " + condicionIva.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return condicionIva;
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
