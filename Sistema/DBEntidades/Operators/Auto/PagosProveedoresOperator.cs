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
    public partial class PagosProveedoresOperator
    {

        public static PagosProveedores GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PagosProveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from PagosProveedores where Id = " + Id.ToString()).Tables[0];
            PagosProveedores pagosProveedores = new PagosProveedores();
            foreach (PropertyInfo prop in typeof(PagosProveedores).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(pagosProveedores, value, null); }
                catch (System.ArgumentException) { }
            }
            return pagosProveedores;
        }

        public static List<PagosProveedores> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PagosProveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<PagosProveedores> lista = new List<PagosProveedores>();
            DataTable dt = db.GetDataSet("select " + columnas + " from PagosProveedores").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                PagosProveedores pagosProveedores = new PagosProveedores();
                foreach (PropertyInfo prop in typeof(PagosProveedores).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(pagosProveedores, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(pagosProveedores);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int NroOrdenPago { get; set; } = 50;
			public static int NroComprobanteTransferencia { get; set; } = 50;


        }

        public static PagosProveedores Save(PagosProveedores pagosProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosProveedoresSave")) throw new PermisoException();
            if (pagosProveedores.Id == -1) return Insert(pagosProveedores);
            else return Update(pagosProveedores);
        }

        public static PagosProveedores Insert(PagosProveedores pagosProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosProveedoresSave")) throw new PermisoException();
            string sql = "insert into PagosProveedores(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PagosProveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pagosProveedores, null));
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
            pagosProveedores.Id = Convert.ToInt32(resp);
            return pagosProveedores;
        }

        public static PagosProveedores Update(PagosProveedores pagosProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPagosProveedoresSave")) throw new PermisoException();
            string sql = "update PagosProveedores set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PagosProveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pagosProveedores, null));
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
            sql += " where Id = " + pagosProveedores.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return pagosProveedores;
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
