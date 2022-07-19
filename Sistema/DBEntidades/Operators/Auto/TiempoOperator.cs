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
    public partial class TiempoOperator
    {

        public static Tiempo GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiempoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Tiempo).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Tiempo where ID = " + ID.ToString()).Tables[0];
            Tiempo tiempo = new Tiempo();
            foreach (PropertyInfo prop in typeof(Tiempo).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tiempo, value, null); }
                catch (System.ArgumentException) { }
            }
            return tiempo;
        }

        public static List<Tiempo> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiempoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Tiempo).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Tiempo> lista = new List<Tiempo>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Tiempo").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Tiempo tiempo = new Tiempo();
                foreach (PropertyInfo prop in typeof(Tiempo).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tiempo, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tiempo);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static Tiempo Save(Tiempo tiempo)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiempoSave")) throw new PermisoException();
            if (tiempo.ID == -1) return Insert(tiempo);
            else return Update(tiempo);
        }

        public static Tiempo Insert(Tiempo tiempo)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiempoSave")) throw new PermisoException();
            string sql = "insert into Tiempo(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Tiempo).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tiempo, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.ID values (" + valores + ")";
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
            tiempo.ID = Convert.ToInt32(resp);
            return tiempo;
        }

        public static Tiempo Update(Tiempo tiempo)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiempoSave")) throw new PermisoException();
            string sql = "update Tiempo set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Tiempo).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tiempo, null));
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
            sql += " where ID = " + tiempo.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tiempo;
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
