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
    public partial class CiudadesOperator
    {

        public static Ciudades GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCiudadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Ciudades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Ciudades where Id = " + Id.ToString()).Tables[0];
            Ciudades ciudades = new Ciudades();
            foreach (PropertyInfo prop in typeof(Ciudades).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(ciudades, value, null); }
                catch (System.ArgumentException) { }
            }
            return ciudades;
        }

        public static List<Ciudades> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCiudadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Ciudades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Ciudades> lista = new List<Ciudades>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Ciudades").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Ciudades ciudades = new Ciudades();
                foreach (PropertyInfo prop in typeof(Ciudades).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(ciudades, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(ciudades);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 100;
			public static int CP { get; set; } = 50;


        }

        public static Ciudades Save(Ciudades ciudades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCiudadesSave")) throw new PermisoException();
            if (ciudades.Id == -1) return Insert(ciudades);
            else return Update(ciudades);
        }

        public static Ciudades Insert(Ciudades ciudades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCiudadesSave")) throw new PermisoException();
            string sql = "insert into Ciudades(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Ciudades).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(ciudades, null));
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
            ciudades.Id = Convert.ToInt32(resp);
            return ciudades;
        }

        public static Ciudades Update(Ciudades ciudades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCiudadesSave")) throw new PermisoException();
            string sql = "update Ciudades set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Ciudades).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(ciudades, null));
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
            sql += " where Id = " + ciudades.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return ciudades;
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
