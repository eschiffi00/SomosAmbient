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
    public partial class EjecucionTareasProgramadasOperator
    {

        public static EjecucionTareasProgramadas GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEjecucionTareasProgramadasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(EjecucionTareasProgramadas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from EjecucionTareasProgramadas where Id = " + Id.ToString()).Tables[0];
            EjecucionTareasProgramadas ejecucionTareasProgramadas = new EjecucionTareasProgramadas();
            foreach (PropertyInfo prop in typeof(EjecucionTareasProgramadas).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(ejecucionTareasProgramadas, value, null); }
                catch (System.ArgumentException) { }
            }
            return ejecucionTareasProgramadas;
        }

        public static List<EjecucionTareasProgramadas> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEjecucionTareasProgramadasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(EjecucionTareasProgramadas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<EjecucionTareasProgramadas> lista = new List<EjecucionTareasProgramadas>();
            DataTable dt = db.GetDataSet("select " + columnas + " from EjecucionTareasProgramadas").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                EjecucionTareasProgramadas ejecucionTareasProgramadas = new EjecucionTareasProgramadas();
                foreach (PropertyInfo prop in typeof(EjecucionTareasProgramadas).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(ejecucionTareasProgramadas, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(ejecucionTareasProgramadas);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static EjecucionTareasProgramadas Save(EjecucionTareasProgramadas ejecucionTareasProgramadas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEjecucionTareasProgramadasSave")) throw new PermisoException();
            if (ejecucionTareasProgramadas.Id == -1) return Insert(ejecucionTareasProgramadas);
            else return Update(ejecucionTareasProgramadas);
        }

        public static EjecucionTareasProgramadas Insert(EjecucionTareasProgramadas ejecucionTareasProgramadas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEjecucionTareasProgramadasSave")) throw new PermisoException();
            string sql = "insert into EjecucionTareasProgramadas(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(EjecucionTareasProgramadas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(ejecucionTareasProgramadas, null));
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
            ejecucionTareasProgramadas.Id = Convert.ToInt32(resp);
            return ejecucionTareasProgramadas;
        }

        public static EjecucionTareasProgramadas Update(EjecucionTareasProgramadas ejecucionTareasProgramadas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEjecucionTareasProgramadasSave")) throw new PermisoException();
            string sql = "update EjecucionTareasProgramadas set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(EjecucionTareasProgramadas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(ejecucionTareasProgramadas, null));
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
            sql += " where Id = " + ejecucionTareasProgramadas.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return ejecucionTareasProgramadas;
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
