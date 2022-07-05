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
    public partial class ParametroOperator
    {

        public static Parametro GetOneByIdentity(int ParametroId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametroBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Parametro).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Parametro where ParametroId = " + ParametroId.ToString()).Tables[0];
            Parametro parametro = new Parametro();
            foreach (PropertyInfo prop in typeof(Parametro).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(parametro, value, null); }
                catch (System.ArgumentException) { }
            }
            return parametro;
        }

        public static List<Parametro> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametroBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Parametro).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Parametro> lista = new List<Parametro>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Parametro").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Parametro parametro = new Parametro();
                foreach (PropertyInfo prop in typeof(Parametro).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(parametro, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(parametro);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Name { get; set; } = 255;


        }

        public static Parametro Save(Parametro parametro)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametroSave")) throw new PermisoException();
            if (parametro.ParametroId == -1) return Insert(parametro);
            else return Update(parametro);
        }

        public static Parametro Insert(Parametro parametro)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametroSave")) throw new PermisoException();
            string sql = "insert into Parametro(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Parametro).GetProperties())
            {
                if (prop.Name == "ParametroId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(parametro, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.ParametroId values (" + valores + ")";
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
            parametro.ParametroId = Convert.ToInt32(resp);
            return parametro;
        }

        public static Parametro Update(Parametro parametro)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoParametroSave")) throw new PermisoException();
            string sql = "update Parametro set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Parametro).GetProperties())
            {
                if (prop.Name == "ParametroId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(parametro, null));
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
            sql += " where ParametroId = " + parametro.ParametroId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return parametro;
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
