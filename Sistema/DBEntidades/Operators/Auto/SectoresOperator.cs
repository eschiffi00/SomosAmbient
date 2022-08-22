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
    public partial class SectoresOperator
    {

        public static Sectores GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Sectores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Sectores where Id = " + Id.ToString()).Tables[0];
            Sectores sectores = new Sectores();
            foreach (PropertyInfo prop in typeof(Sectores).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(sectores, value, null); }
                catch (System.ArgumentException) { }
            }
            return sectores;
        }

        public static List<Sectores> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Sectores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Sectores> lista = new List<Sectores>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Sectores").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Sectores sectores = new Sectores();
                foreach (PropertyInfo prop in typeof(Sectores).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(sectores, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(sectores);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 200;


        }

        public static Sectores Save(Sectores sectores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresSave")) throw new PermisoException();
            if (sectores.Id == -1) return Insert(sectores);
            else return Update(sectores);
        }

        public static Sectores Insert(Sectores sectores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresSave")) throw new PermisoException();
            string sql = "insert into Sectores(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Sectores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(sectores, null));
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
            sectores.Id = Convert.ToInt32(resp);
            return sectores;
        }

        public static Sectores Update(Sectores sectores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoSectoresSave")) throw new PermisoException();
            string sql = "update Sectores set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Sectores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(sectores, null));
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
            sql += " where Id = " + sectores.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return sectores;
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
