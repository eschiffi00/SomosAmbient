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
    public partial class EstadosOperator
    {

        public static Estados GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Estados).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Estados where Id = " + Id.ToString()).Tables[0];
            Estados estados = new Estados();
            foreach (PropertyInfo prop in typeof(Estados).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(estados, value, null); }
                catch (System.ArgumentException) { }
            }
            return estados;
        }

        public static List<Estados> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Estados).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Estados> lista = new List<Estados>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Estados").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Estados estados = new Estados();
                foreach (PropertyInfo prop in typeof(Estados).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(estados, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(estados);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;
			public static int Entidad { get; set; } = 50;


        }

        public static Estados Save(Estados estados)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadosSave")) throw new PermisoException();
            if (estados.Id == -1) return Insert(estados);
            else return Update(estados);
        }

        public static Estados Insert(Estados estados)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadosSave")) throw new PermisoException();
            string sql = "insert into Estados(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Estados).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(estados, null));
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
            estados.Id = Convert.ToInt32(resp);
            return estados;
        }

        public static Estados Update(Estados estados)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadosSave")) throw new PermisoException();
            string sql = "update Estados set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Estados).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(estados, null));
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
            sql += " where Id = " + estados.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return estados;
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
