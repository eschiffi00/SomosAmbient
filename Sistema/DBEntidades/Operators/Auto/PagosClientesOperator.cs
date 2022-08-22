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
    public partial class PagosClientesOperator
    {

        public static PagosClientes GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosClientesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PagosClientes).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from PagosClientes where Id = " + Id.ToString()).Tables[0];
            PagosClientes pagosClientes = new PagosClientes();
            foreach (PropertyInfo prop in typeof(PagosClientes).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(pagosClientes, value, null); }
                catch (System.ArgumentException) { }
            }
            return pagosClientes;
        }

        public static List<PagosClientes> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosClientesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PagosClientes).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<PagosClientes> lista = new List<PagosClientes>();
            DataTable dt = db.GetDataSet("select " + columnas + " from PagosClientes").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                PagosClientes pagosClientes = new PagosClientes();
                foreach (PropertyInfo prop in typeof(PagosClientes).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(pagosClientes, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(pagosClientes);
            }
            return lista;
        }

		public static List<PagosClientes> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<PagosClientes> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<PagosClientes> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<PagosClientes> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Concepto { get; set; } = 500;
			public static int NroRecibo { get; set; } = 50;
			public static int TipoPago { get; set; } = 50;


        }

        public static PagosClientes Save(PagosClientes pagosClientes)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosClientesSave")) throw new PermisoException();
            if (pagosClientes.Id == -1) return Insert(pagosClientes);
            else return Update(pagosClientes);
        }

        public static PagosClientes Insert(PagosClientes pagosClientes)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosClientesSave")) throw new PermisoException();
            string sql = "insert into PagosClientes(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PagosClientes).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pagosClientes, null));
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
            pagosClientes.Id = Convert.ToInt32(resp);
            return pagosClientes;
        }

        public static PagosClientes Update(PagosClientes pagosClientes)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosClientesSave")) throw new PermisoException();
            string sql = "update PagosClientes set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PagosClientes).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pagosClientes, null));
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
            sql += " where Id = " + pagosClientes.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return pagosClientes;
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
