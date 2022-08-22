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
    public partial class SectoresEmpresaOperator
    {

        public static SectoresEmpresa GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresEmpresaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(SectoresEmpresa).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from SectoresEmpresa where Id = " + Id.ToString()).Tables[0];
            SectoresEmpresa sectoresEmpresa = new SectoresEmpresa();
            foreach (PropertyInfo prop in typeof(SectoresEmpresa).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(sectoresEmpresa, value, null); }
                catch (System.ArgumentException) { }
            }
            return sectoresEmpresa;
        }

        public static List<SectoresEmpresa> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresEmpresaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(SectoresEmpresa).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<SectoresEmpresa> lista = new List<SectoresEmpresa>();
            DataTable dt = db.GetDataSet("select " + columnas + " from SectoresEmpresa").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                SectoresEmpresa sectoresEmpresa = new SectoresEmpresa();
                foreach (PropertyInfo prop in typeof(SectoresEmpresa).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(sectoresEmpresa, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(sectoresEmpresa);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 200;


        }

        public static SectoresEmpresa Save(SectoresEmpresa sectoresEmpresa)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresEmpresaSave")) throw new PermisoException();
            if (sectoresEmpresa.Id == -1) return Insert(sectoresEmpresa);
            else return Update(sectoresEmpresa);
        }

        public static SectoresEmpresa Insert(SectoresEmpresa sectoresEmpresa)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresEmpresaSave")) throw new PermisoException();
            string sql = "insert into SectoresEmpresa(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(SectoresEmpresa).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(sectoresEmpresa, null));
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
            sectoresEmpresa.Id = Convert.ToInt32(resp);
            return sectoresEmpresa;
        }

        public static SectoresEmpresa Update(SectoresEmpresa sectoresEmpresa)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresEmpresaSave")) throw new PermisoException();
            string sql = "update SectoresEmpresa set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(SectoresEmpresa).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(sectoresEmpresa, null));
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
            sql += " where Id = " + sectoresEmpresa.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return sectoresEmpresa;
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
