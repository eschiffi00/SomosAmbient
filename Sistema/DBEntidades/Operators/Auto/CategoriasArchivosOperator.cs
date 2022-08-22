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
    public partial class CategoriasArchivosOperator
    {

        public static CategoriasArchivos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasArchivosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CategoriasArchivos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CategoriasArchivos where Id = " + Id.ToString()).Tables[0];
            CategoriasArchivos categoriasArchivos = new CategoriasArchivos();
            foreach (PropertyInfo prop in typeof(CategoriasArchivos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(categoriasArchivos, value, null); }
                catch (System.ArgumentException) { }
            }
            return categoriasArchivos;
        }

        public static List<CategoriasArchivos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasArchivosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CategoriasArchivos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CategoriasArchivos> lista = new List<CategoriasArchivos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CategoriasArchivos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CategoriasArchivos categoriasArchivos = new CategoriasArchivos();
                foreach (PropertyInfo prop in typeof(CategoriasArchivos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(categoriasArchivos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(categoriasArchivos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static CategoriasArchivos Save(CategoriasArchivos categoriasArchivos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasArchivosSave")) throw new PermisoException();
            if (categoriasArchivos.Id == -1) return Insert(categoriasArchivos);
            else return Update(categoriasArchivos);
        }

        public static CategoriasArchivos Insert(CategoriasArchivos categoriasArchivos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasArchivosSave")) throw new PermisoException();
            string sql = "insert into CategoriasArchivos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CategoriasArchivos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(categoriasArchivos, null));
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
            categoriasArchivos.Id = Convert.ToInt32(resp);
            return categoriasArchivos;
        }

        public static CategoriasArchivos Update(CategoriasArchivos categoriasArchivos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCategoriasArchivosSave")) throw new PermisoException();
            string sql = "update CategoriasArchivos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CategoriasArchivos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(categoriasArchivos, null));
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
            sql += " where Id = " + categoriasArchivos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return categoriasArchivos;
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
