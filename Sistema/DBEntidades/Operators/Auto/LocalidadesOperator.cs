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
    public partial class LocalidadesOperator
    {

        public static Localidades GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLocalidadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Localidades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Localidades where Id = " + Id.ToString()).Tables[0];
            Localidades localidades = new Localidades();
            foreach (PropertyInfo prop in typeof(Localidades).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(localidades, value, null); }
                catch (System.ArgumentException) { }
            }
            return localidades;
        }

        public static List<Localidades> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLocalidadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Localidades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Localidades> lista = new List<Localidades>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Localidades").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Localidades localidades = new Localidades();
                foreach (PropertyInfo prop in typeof(Localidades).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(localidades, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(localidades);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 200;


        }

        public static Localidades Save(Localidades localidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLocalidadesSave")) throw new PermisoException();
            if (localidades.Id == -1) return Insert(localidades);
            else return Update(localidades);
        }

        public static Localidades Insert(Localidades localidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLocalidadesSave")) throw new PermisoException();
            string sql = "insert into Localidades(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Localidades).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(localidades, null));
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
            localidades.Id = Convert.ToInt32(resp);
            return localidades;
        }

        public static Localidades Update(Localidades localidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLocalidadesSave")) throw new PermisoException();
            string sql = "update Localidades set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Localidades).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(localidades, null));
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
            sql += " where Id = " + localidades.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return localidades;
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
