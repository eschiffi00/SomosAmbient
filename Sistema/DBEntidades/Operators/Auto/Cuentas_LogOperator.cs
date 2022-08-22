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
    public partial class Cuentas_LogOperator
    {

        public static Cuentas_Log GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentas_LogBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cuentas_Log).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cuentas_Log where Id = " + Id.ToString()).Tables[0];
            Cuentas_Log cuentas_Log = new Cuentas_Log();
            foreach (PropertyInfo prop in typeof(Cuentas_Log).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(cuentas_Log, value, null); }
                catch (System.ArgumentException) { }
            }
            return cuentas_Log;
        }

        public static List<Cuentas_Log> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentas_LogBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cuentas_Log).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Cuentas_Log> lista = new List<Cuentas_Log>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cuentas_Log").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Cuentas_Log cuentas_Log = new Cuentas_Log();
                foreach (PropertyInfo prop in typeof(Cuentas_Log).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(cuentas_Log, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(cuentas_Log);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 200;
			public static int TipoMovimiento { get; set; } = 50;


        }

        public static Cuentas_Log Save(Cuentas_Log cuentas_Log)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentas_LogSave")) throw new PermisoException();
            if (cuentas_Log.Id == -1) return Insert(cuentas_Log);
            else return Update(cuentas_Log);
        }

        public static Cuentas_Log Insert(Cuentas_Log cuentas_Log)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentas_LogSave")) throw new PermisoException();
            string sql = "insert into Cuentas_Log(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cuentas_Log).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cuentas_Log, null));
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
            cuentas_Log.Id = Convert.ToInt32(resp);
            return cuentas_Log;
        }

        public static Cuentas_Log Update(Cuentas_Log cuentas_Log)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentas_LogSave")) throw new PermisoException();
            string sql = "update Cuentas_Log set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cuentas_Log).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cuentas_Log, null));
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
            sql += " where Id = " + cuentas_Log.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return cuentas_Log;
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
