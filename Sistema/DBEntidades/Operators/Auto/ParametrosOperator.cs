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
    public partial class ParametrosOperator
    {

        public static Parametros GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametrosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Parametros).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Parametros where Id = " + Id.ToString()).Tables[0];
            Parametros parametros = new Parametros();
            foreach (PropertyInfo prop in typeof(Parametros).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(parametros, value, null); }
                catch (System.ArgumentException) { }
            }
            return parametros;
        }

        public static List<Parametros> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametrosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Parametros).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Parametros> lista = new List<Parametros>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Parametros").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Parametros parametros = new Parametros();
                foreach (PropertyInfo prop in typeof(Parametros).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(parametros, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(parametros);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;
			public static int Valor { get; set; } = 50;


        }

        public static Parametros Save(Parametros parametros)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametrosSave")) throw new PermisoException();
            if (parametros.Id == -1) return Insert(parametros);
            else return Update(parametros);
        }

        public static Parametros Insert(Parametros parametros)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametrosSave")) throw new PermisoException();
            string sql = "insert into Parametros(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Parametros).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(parametros, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.Id values (" + valores + ")";
            DB db = new DB();
            List<object> Parametros = new List<object>();
            for (int i = 0; i < param.Count; i++)
            {
                Parametros.Add(param[i]);
                Parametros.Add(valor[i]);
                SqlParameter p = new SqlParameter(param[i].ToString(), valor[i]);
                sqlParams.Add(p);
            }
            //object resp = db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            parametros.Id = Convert.ToInt32(resp);
            return parametros;
        }

        public static Parametros Update(Parametros parametros)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametrosSave")) throw new PermisoException();
            string sql = "update Parametros set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Parametros).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(parametros, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            sql += columnas;
            List<object> Parametros = new List<object>();
            for (int i = 0; i<param.Count; i++)
            {
                Parametros.Add(param[i]);
                Parametros.Add(valor[i]);
                SqlParameter p = new SqlParameter(param[i].ToString(), valor[i]);
                sqlParams.Add(p);
        }
            sql += " where Id = " + parametros.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return parametros;
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
