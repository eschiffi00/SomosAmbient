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
    public partial class ClientesPruebaOperator
    {

        public static ClientesPrueba GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesPruebaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ClientesPrueba).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ClientesPrueba where Id = " + Id.ToString()).Tables[0];
            ClientesPrueba clientesPrueba = new ClientesPrueba();
            foreach (PropertyInfo prop in typeof(ClientesPrueba).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(clientesPrueba, value, null); }
                catch (System.ArgumentException) { }
            }
            return clientesPrueba;
        }

        public static List<ClientesPrueba> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesPruebaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ClientesPrueba).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ClientesPrueba> lista = new List<ClientesPrueba>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ClientesPrueba").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ClientesPrueba clientesPrueba = new ClientesPrueba();
                foreach (PropertyInfo prop in typeof(ClientesPrueba).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(clientesPrueba, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(clientesPrueba);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Persona { get; set; } = 500;
			public static int tel { get; set; } = 500;
			public static int mail { get; set; } = 500;
			public static int organizacion { get; set; } = 500;


        }

        public static ClientesPrueba Save(ClientesPrueba clientesPrueba)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesPruebaSave")) throw new PermisoException();
            if (clientesPrueba.Id == -1) return Insert(clientesPrueba);
            else return Update(clientesPrueba);
        }

        public static ClientesPrueba Insert(ClientesPrueba clientesPrueba)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesPruebaSave")) throw new PermisoException();
            string sql = "insert into ClientesPrueba(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ClientesPrueba).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(clientesPrueba, null));
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
            clientesPrueba.Id = Convert.ToInt32(resp);
            return clientesPrueba;
        }

        public static ClientesPrueba Update(ClientesPrueba clientesPrueba)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesPruebaSave")) throw new PermisoException();
            string sql = "update ClientesPrueba set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ClientesPrueba).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(clientesPrueba, null));
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
            sql += " where Id = " + clientesPrueba.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return clientesPrueba;
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
