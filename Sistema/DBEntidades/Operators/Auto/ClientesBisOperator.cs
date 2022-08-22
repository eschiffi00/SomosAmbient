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
    public partial class ClientesBisOperator
    {

        public static ClientesBis GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesBisBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ClientesBis).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ClientesBis where Id = " + Id.ToString()).Tables[0];
            ClientesBis clientesBis = new ClientesBis();
            foreach (PropertyInfo prop in typeof(ClientesBis).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(clientesBis, value, null); }
                catch (System.ArgumentException) { }
            }
            return clientesBis;
        }

        public static List<ClientesBis> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesBisBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ClientesBis).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ClientesBis> lista = new List<ClientesBis>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ClientesBis").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ClientesBis clientesBis = new ClientesBis();
                foreach (PropertyInfo prop in typeof(ClientesBis).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(clientesBis, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(clientesBis);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int ApellidoNombre { get; set; } = 200;
			public static int RazonSocial { get; set; } = 200;
			public static int CUILCUIT { get; set; } = 50;
			public static int CondicionIva { get; set; } = 50;
			public static int Direccion { get; set; } = 100;
			public static int PersonaFisicaJuridica { get; set; } = 100;
			public static int TipoCliente { get; set; } = 1;
			public static int MailContactoContratacion { get; set; } = 100;
			public static int MailContactoAdministracion { get; set; } = 100;
			public static int MailContactoTesoreia { get; set; } = 100;
			public static int MailContactoOrganizacion { get; set; } = 100;
			public static int TelContactoContratacion { get; set; } = 50;
			public static int TelContactoAdministracion { get; set; } = 50;
			public static int TelContactoTesoreria { get; set; } = 50;
			public static int TelContactoOrganizacion { get; set; } = 50;


        }

        public static ClientesBis Save(ClientesBis clientesBis)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesBisSave")) throw new PermisoException();
            if (clientesBis.Id == -1) return Insert(clientesBis);
            else return Update(clientesBis);
        }

        public static ClientesBis Insert(ClientesBis clientesBis)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesBisSave")) throw new PermisoException();
            string sql = "insert into ClientesBis(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ClientesBis).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(clientesBis, null));
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
            clientesBis.Id = Convert.ToInt32(resp);
            return clientesBis;
        }

        public static ClientesBis Update(ClientesBis clientesBis)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoClientesBisSave")) throw new PermisoException();
            string sql = "update ClientesBis set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ClientesBis).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(clientesBis, null));
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
            sql += " where Id = " + clientesBis.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return clientesBis;
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
