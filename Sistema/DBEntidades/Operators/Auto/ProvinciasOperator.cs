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
    public partial class ProvinciasOperator
    {

        public static Provincias GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Provincias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Provincias where Id = " + Id.ToString()).Tables[0];
            Provincias provincias = new Provincias();
            foreach (PropertyInfo prop in typeof(Provincias).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(provincias, value, null); }
                catch (System.ArgumentException) { }
            }
            return provincias;
        }

        public static List<Provincias> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Provincias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Provincias> lista = new List<Provincias>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Provincias").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Provincias provincias = new Provincias();
                foreach (PropertyInfo prop in typeof(Provincias).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(provincias, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(provincias);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static Provincias Save(Provincias provincias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciasSave")) throw new PermisoException();
            if (provincias.Id == -1) return Insert(provincias);
            else return Update(provincias);
        }

        public static Provincias Insert(Provincias provincias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciasSave")) throw new PermisoException();
            string sql = "insert into Provincias(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Provincias).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(provincias, null));
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
            provincias.Id = Convert.ToInt32(resp);
            return provincias;
        }

        public static Provincias Update(Provincias provincias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciasSave")) throw new PermisoException();
            string sql = "update Provincias set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Provincias).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(provincias, null));
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
            sql += " where Id = " + provincias.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return provincias;
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
