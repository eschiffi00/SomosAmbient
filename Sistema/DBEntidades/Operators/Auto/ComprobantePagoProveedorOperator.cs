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
    public partial class ComprobantePagoProveedorOperator
    {

        public static ComprobantePagoProveedor GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantePagoProveedorBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ComprobantePagoProveedor).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ComprobantePagoProveedor where Id = " + Id.ToString()).Tables[0];
            ComprobantePagoProveedor comprobantePagoProveedor = new ComprobantePagoProveedor();
            foreach (PropertyInfo prop in typeof(ComprobantePagoProveedor).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(comprobantePagoProveedor, value, null); }
                catch (System.ArgumentException) { }
            }
            return comprobantePagoProveedor;
        }

        public static List<ComprobantePagoProveedor> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantePagoProveedorBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ComprobantePagoProveedor).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ComprobantePagoProveedor> lista = new List<ComprobantePagoProveedor>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ComprobantePagoProveedor").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ComprobantePagoProveedor comprobantePagoProveedor = new ComprobantePagoProveedor();
                foreach (PropertyInfo prop in typeof(ComprobantePagoProveedor).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(comprobantePagoProveedor, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(comprobantePagoProveedor);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static ComprobantePagoProveedor Save(ComprobantePagoProveedor comprobantePagoProveedor)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantePagoProveedorSave")) throw new PermisoException();
            if (comprobantePagoProveedor.Id == -1) return Insert(comprobantePagoProveedor);
            else return Update(comprobantePagoProveedor);
        }

        public static ComprobantePagoProveedor Insert(ComprobantePagoProveedor comprobantePagoProveedor)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantePagoProveedorSave")) throw new PermisoException();
            string sql = "insert into ComprobantePagoProveedor(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ComprobantePagoProveedor).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(comprobantePagoProveedor, null));
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
            comprobantePagoProveedor.Id = Convert.ToInt32(resp);
            return comprobantePagoProveedor;
        }

        public static ComprobantePagoProveedor Update(ComprobantePagoProveedor comprobantePagoProveedor)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantePagoProveedorSave")) throw new PermisoException();
            string sql = "update ComprobantePagoProveedor set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ComprobantePagoProveedor).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(comprobantePagoProveedor, null));
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
            sql += " where Id = " + comprobantePagoProveedor.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return comprobantePagoProveedor;
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
