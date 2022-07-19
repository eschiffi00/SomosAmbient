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
    public partial class EstadoOperator
    {

        public static Estado GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Estado).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Estado where ID = " + ID.ToString()).Tables[0];
            Estado estado = new Estado();
            foreach (PropertyInfo prop in typeof(Estado).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(estado, value, null); }
                catch (System.ArgumentException) { }
            }
            return estado;
        }

        public static List<Estado> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Estado).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Estado> lista = new List<Estado>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Estado").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Estado estado = new Estado();
                foreach (PropertyInfo prop in typeof(Estado).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(estado, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(estado);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;
			public static int Entidad { get; set; } = 50;


        }

        public static Estado Save(Estado estado)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadoSave")) throw new PermisoException();
            if (estado.ID == -1) return Insert(estado);
            else return Update(estado);
        }

        public static Estado Insert(Estado estado)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadoSave")) throw new PermisoException();
            string sql = "insert into Estado(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Estado).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(estado, null));
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
            estado.ID = Convert.ToInt32(resp);
            return estado;
        }

        public static Estado Update(Estado estado)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEstadoSave")) throw new PermisoException();
            string sql = "update Estado set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Estado).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(estado, null));
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
            sql += " where ID = " + estado.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return estado;
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
