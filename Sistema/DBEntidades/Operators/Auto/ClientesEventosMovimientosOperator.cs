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
    public partial class ClientesEventosMovimientosOperator
    {

        public static ClientesEventosMovimientos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesEventosMovimientosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ClientesEventosMovimientos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ClientesEventosMovimientos where Id = " + Id.ToString()).Tables[0];
            ClientesEventosMovimientos clientesEventosMovimientos = new ClientesEventosMovimientos();
            foreach (PropertyInfo prop in typeof(ClientesEventosMovimientos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(clientesEventosMovimientos, value, null); }
                catch (System.ArgumentException) { }
            }
            return clientesEventosMovimientos;
        }

        public static List<ClientesEventosMovimientos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesEventosMovimientosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ClientesEventosMovimientos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ClientesEventosMovimientos> lista = new List<ClientesEventosMovimientos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ClientesEventosMovimientos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ClientesEventosMovimientos clientesEventosMovimientos = new ClientesEventosMovimientos();
                foreach (PropertyInfo prop in typeof(ClientesEventosMovimientos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(clientesEventosMovimientos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(clientesEventosMovimientos);
            }
            return lista;
        }

		public static List<ClientesEventosMovimientos> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<ClientesEventosMovimientos> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<ClientesEventosMovimientos> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<ClientesEventosMovimientos> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Comentario { get; set; } = 2000;


        }

        public static ClientesEventosMovimientos Save(ClientesEventosMovimientos clientesEventosMovimientos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesEventosMovimientosSave")) throw new PermisoException();
            if (clientesEventosMovimientos.Id == -1) return Insert(clientesEventosMovimientos);
            else return Update(clientesEventosMovimientos);
        }

        public static ClientesEventosMovimientos Insert(ClientesEventosMovimientos clientesEventosMovimientos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesEventosMovimientosSave")) throw new PermisoException();
            string sql = "insert into ClientesEventosMovimientos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ClientesEventosMovimientos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(clientesEventosMovimientos, null));
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
            clientesEventosMovimientos.Id = Convert.ToInt32(resp);
            return clientesEventosMovimientos;
        }

        public static ClientesEventosMovimientos Update(ClientesEventosMovimientos clientesEventosMovimientos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesEventosMovimientosSave")) throw new PermisoException();
            string sql = "update ClientesEventosMovimientos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ClientesEventosMovimientos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(clientesEventosMovimientos, null));
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
            sql += " where Id = " + clientesEventosMovimientos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return clientesEventosMovimientos;
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
