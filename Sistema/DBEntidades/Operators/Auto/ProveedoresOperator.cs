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
    public partial class ProveedoresOperator
    {

        public static Proveedores GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Proveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Proveedores where Id = " + Id.ToString()).Tables[0];
            Proveedores proveedores = new Proveedores();
            foreach (PropertyInfo prop in typeof(Proveedores).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(proveedores, value, null); }
                catch (System.ArgumentException) { }
            }
            return proveedores;
        }

        public static List<Proveedores> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Proveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Proveedores> lista = new List<Proveedores>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Proveedores").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Proveedores proveedores = new Proveedores();
                foreach (PropertyInfo prop in typeof(Proveedores).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(proveedores, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(proveedores);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int RazonSocial { get; set; } = 50;
			public static int Cuit { get; set; } = 50;
			public static int Propio { get; set; } = 1;
			public static int NombreFantasia { get; set; } = 500;
			public static int Telefono { get; set; } = 100;
			public static int CBU { get; set; } = 100;
			public static int NroCliente { get; set; } = 100;
			public static int NroIIBB { get; set; } = 50;
			public static int Localidad { get; set; } = 50;
			public static int Provincia { get; set; } = 50;


        }

        public static Proveedores Save(Proveedores proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresSave")) throw new PermisoException();
            if (proveedores.Id == -1) return Insert(proveedores);
            else return Update(proveedores);
        }

        public static Proveedores Insert(Proveedores proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresSave")) throw new PermisoException();
            string sql = "insert into Proveedores(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Proveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(proveedores, null));
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
            proveedores.Id = Convert.ToInt32(resp);
            return proveedores;
        }

        public static Proveedores Update(Proveedores proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresSave")) throw new PermisoException();
            string sql = "update Proveedores set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Proveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(proveedores, null));
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
            sql += " where Id = " + proveedores.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return proveedores;
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
