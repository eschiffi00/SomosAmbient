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
    public partial class INVENTARIO_ProductoDepositoOperator
    {

        public static INVENTARIO_ProductoDeposito GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoDepositoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_ProductoDeposito).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_ProductoDeposito where Id = " + Id.ToString()).Tables[0];
            INVENTARIO_ProductoDeposito iNVENTARIO_ProductoDeposito = new INVENTARIO_ProductoDeposito();
            foreach (PropertyInfo prop in typeof(INVENTARIO_ProductoDeposito).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(iNVENTARIO_ProductoDeposito, value, null); }
                catch (System.ArgumentException) { }
            }
            return iNVENTARIO_ProductoDeposito;
        }

        public static List<INVENTARIO_ProductoDeposito> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoDepositoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_ProductoDeposito).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<INVENTARIO_ProductoDeposito> lista = new List<INVENTARIO_ProductoDeposito>();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_ProductoDeposito").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                INVENTARIO_ProductoDeposito iNVENTARIO_ProductoDeposito = new INVENTARIO_ProductoDeposito();
                foreach (PropertyInfo prop in typeof(INVENTARIO_ProductoDeposito).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(iNVENTARIO_ProductoDeposito, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(iNVENTARIO_ProductoDeposito);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static INVENTARIO_ProductoDeposito Save(INVENTARIO_ProductoDeposito iNVENTARIO_ProductoDeposito)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoDepositoSave")) throw new PermisoException();
            if (iNVENTARIO_ProductoDeposito.Id == -1) return Insert(iNVENTARIO_ProductoDeposito);
            else return Update(iNVENTARIO_ProductoDeposito);
        }

        public static INVENTARIO_ProductoDeposito Insert(INVENTARIO_ProductoDeposito iNVENTARIO_ProductoDeposito)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoDepositoSave")) throw new PermisoException();
            string sql = "insert into INVENTARIO_ProductoDeposito(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_ProductoDeposito).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_ProductoDeposito, null));
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
            iNVENTARIO_ProductoDeposito.Id = Convert.ToInt32(resp);
            return iNVENTARIO_ProductoDeposito;
        }

        public static INVENTARIO_ProductoDeposito Update(INVENTARIO_ProductoDeposito iNVENTARIO_ProductoDeposito)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoDepositoSave")) throw new PermisoException();
            string sql = "update INVENTARIO_ProductoDeposito set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_ProductoDeposito).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_ProductoDeposito, null));
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
            sql += " where Id = " + iNVENTARIO_ProductoDeposito.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return iNVENTARIO_ProductoDeposito;
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
