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
    public partial class IntermediariosOperator
    {

        public static Intermediarios GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIntermediariosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Intermediarios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Intermediarios where Id = " + Id.ToString()).Tables[0];
            Intermediarios intermediarios = new Intermediarios();
            foreach (PropertyInfo prop in typeof(Intermediarios).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(intermediarios, value, null); }
                catch (System.ArgumentException) { }
            }
            return intermediarios;
        }

        public static List<Intermediarios> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIntermediariosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Intermediarios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Intermediarios> lista = new List<Intermediarios>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Intermediarios").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Intermediarios intermediarios = new Intermediarios();
                foreach (PropertyInfo prop in typeof(Intermediarios).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(intermediarios, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(intermediarios);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int TipoComision { get; set; } = 20;


        }

        public static Intermediarios Save(Intermediarios intermediarios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIntermediariosSave")) throw new PermisoException();
            if (intermediarios.Id == -1) return Insert(intermediarios);
            else return Update(intermediarios);
        }

        public static Intermediarios Insert(Intermediarios intermediarios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIntermediariosSave")) throw new PermisoException();
            string sql = "insert into Intermediarios(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Intermediarios).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(intermediarios, null));
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
            intermediarios.Id = Convert.ToInt32(resp);
            return intermediarios;
        }

        public static Intermediarios Update(Intermediarios intermediarios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIntermediariosSave")) throw new PermisoException();
            string sql = "update Intermediarios set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Intermediarios).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(intermediarios, null));
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
            sql += " where Id = " + intermediarios.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return intermediarios;
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
