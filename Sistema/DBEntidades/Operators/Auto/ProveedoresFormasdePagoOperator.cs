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
    public partial class ProveedoresFormasdePagoOperator
    {

        public static ProveedoresFormasdePago GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresFormasdePagoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ProveedoresFormasdePago).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ProveedoresFormasdePago where Id = " + Id.ToString()).Tables[0];
            ProveedoresFormasdePago proveedoresFormasdePago = new ProveedoresFormasdePago();
            foreach (PropertyInfo prop in typeof(ProveedoresFormasdePago).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(proveedoresFormasdePago, value, null); }
                catch (System.ArgumentException) { }
            }
            return proveedoresFormasdePago;
        }

        public static List<ProveedoresFormasdePago> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresFormasdePagoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ProveedoresFormasdePago).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ProveedoresFormasdePago> lista = new List<ProveedoresFormasdePago>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ProveedoresFormasdePago").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ProveedoresFormasdePago proveedoresFormasdePago = new ProveedoresFormasdePago();
                foreach (PropertyInfo prop in typeof(ProveedoresFormasdePago).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(proveedoresFormasdePago, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(proveedoresFormasdePago);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static ProveedoresFormasdePago Save(ProveedoresFormasdePago proveedoresFormasdePago)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresFormasdePagoSave")) throw new PermisoException();
            if (proveedoresFormasdePago.Id == -1) return Insert(proveedoresFormasdePago);
            else return Update(proveedoresFormasdePago);
        }

        public static ProveedoresFormasdePago Insert(ProveedoresFormasdePago proveedoresFormasdePago)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresFormasdePagoSave")) throw new PermisoException();
            string sql = "insert into ProveedoresFormasdePago(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ProveedoresFormasdePago).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(proveedoresFormasdePago, null));
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
            proveedoresFormasdePago.Id = Convert.ToInt32(resp);
            return proveedoresFormasdePago;
        }

        public static ProveedoresFormasdePago Update(ProveedoresFormasdePago proveedoresFormasdePago)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProveedoresFormasdePagoSave")) throw new PermisoException();
            string sql = "update ProveedoresFormasdePago set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ProveedoresFormasdePago).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(proveedoresFormasdePago, null));
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
            sql += " where Id = " + proveedoresFormasdePago.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return proveedoresFormasdePago;
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
