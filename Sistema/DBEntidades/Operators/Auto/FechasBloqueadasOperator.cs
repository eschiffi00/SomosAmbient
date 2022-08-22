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
    public partial class FechasBloqueadasOperator
    {

        public static FechasBloqueadas GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFechasBloqueadasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(FechasBloqueadas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from FechasBloqueadas where Id = " + Id.ToString()).Tables[0];
            FechasBloqueadas fechasBloqueadas = new FechasBloqueadas();
            foreach (PropertyInfo prop in typeof(FechasBloqueadas).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(fechasBloqueadas, value, null); }
                catch (System.ArgumentException) { }
            }
            return fechasBloqueadas;
        }

        public static List<FechasBloqueadas> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFechasBloqueadasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(FechasBloqueadas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<FechasBloqueadas> lista = new List<FechasBloqueadas>();
            DataTable dt = db.GetDataSet("select " + columnas + " from FechasBloqueadas").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                FechasBloqueadas fechasBloqueadas = new FechasBloqueadas();
                foreach (PropertyInfo prop in typeof(FechasBloqueadas).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(fechasBloqueadas, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(fechasBloqueadas);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static FechasBloqueadas Save(FechasBloqueadas fechasBloqueadas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFechasBloqueadasSave")) throw new PermisoException();
            if (fechasBloqueadas.Id == -1) return Insert(fechasBloqueadas);
            else return Update(fechasBloqueadas);
        }

        public static FechasBloqueadas Insert(FechasBloqueadas fechasBloqueadas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFechasBloqueadasSave")) throw new PermisoException();
            string sql = "insert into FechasBloqueadas(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(FechasBloqueadas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(fechasBloqueadas, null));
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
            fechasBloqueadas.Id = Convert.ToInt32(resp);
            return fechasBloqueadas;
        }

        public static FechasBloqueadas Update(FechasBloqueadas fechasBloqueadas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFechasBloqueadasSave")) throw new PermisoException();
            string sql = "update FechasBloqueadas set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(FechasBloqueadas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(fechasBloqueadas, null));
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
            sql += " where Id = " + fechasBloqueadas.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return fechasBloqueadas;
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
