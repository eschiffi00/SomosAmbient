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
    public partial class UnidadesNegociosOperator
    {

        public static UnidadesNegocios GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegociosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UnidadesNegocios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from UnidadesNegocios where Id = " + Id.ToString()).Tables[0];
            UnidadesNegocios unidadesNegocios = new UnidadesNegocios();
            foreach (PropertyInfo prop in typeof(UnidadesNegocios).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(unidadesNegocios, value, null); }
                catch (System.ArgumentException) { }
            }
            return unidadesNegocios;
        }

        public static List<UnidadesNegocios> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegociosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UnidadesNegocios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<UnidadesNegocios> lista = new List<UnidadesNegocios>();
            DataTable dt = db.GetDataSet("select " + columnas + " from UnidadesNegocios").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                UnidadesNegocios unidadesNegocios = new UnidadesNegocios();
                foreach (PropertyInfo prop in typeof(UnidadesNegocios).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(unidadesNegocios, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(unidadesNegocios);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static UnidadesNegocios Save(UnidadesNegocios unidadesNegocios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegociosSave")) throw new PermisoException();
            if (unidadesNegocios.Id == -1) return Insert(unidadesNegocios);
            else return Update(unidadesNegocios);
        }

        public static UnidadesNegocios Insert(UnidadesNegocios unidadesNegocios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegociosSave")) throw new PermisoException();
            string sql = "insert into UnidadesNegocios(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UnidadesNegocios).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(unidadesNegocios, null));
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
            unidadesNegocios.Id = Convert.ToInt32(resp);
            return unidadesNegocios;
        }

        public static UnidadesNegocios Update(UnidadesNegocios unidadesNegocios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegociosSave")) throw new PermisoException();
            string sql = "update UnidadesNegocios set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UnidadesNegocios).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(unidadesNegocios, null));
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
            sql += " where Id = " + unidadesNegocios.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return unidadesNegocios;
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
