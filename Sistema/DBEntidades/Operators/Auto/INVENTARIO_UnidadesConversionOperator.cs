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
    public partial class INVENTARIO_UnidadesConversionOperator
    {

        public static INVENTARIO_UnidadesConversion GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesConversionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_UnidadesConversion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_UnidadesConversion where Id = " + Id.ToString()).Tables[0];
            INVENTARIO_UnidadesConversion iNVENTARIO_UnidadesConversion = new INVENTARIO_UnidadesConversion();
            foreach (PropertyInfo prop in typeof(INVENTARIO_UnidadesConversion).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(iNVENTARIO_UnidadesConversion, value, null); }
                catch (System.ArgumentException) { }
            }
            return iNVENTARIO_UnidadesConversion;
        }

        public static List<INVENTARIO_UnidadesConversion> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesConversionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_UnidadesConversion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<INVENTARIO_UnidadesConversion> lista = new List<INVENTARIO_UnidadesConversion>();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_UnidadesConversion").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                INVENTARIO_UnidadesConversion iNVENTARIO_UnidadesConversion = new INVENTARIO_UnidadesConversion();
                foreach (PropertyInfo prop in typeof(INVENTARIO_UnidadesConversion).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(iNVENTARIO_UnidadesConversion, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(iNVENTARIO_UnidadesConversion);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static INVENTARIO_UnidadesConversion Save(INVENTARIO_UnidadesConversion iNVENTARIO_UnidadesConversion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesConversionSave")) throw new PermisoException();
            if (iNVENTARIO_UnidadesConversion.Id == -1) return Insert(iNVENTARIO_UnidadesConversion);
            else return Update(iNVENTARIO_UnidadesConversion);
        }

        public static INVENTARIO_UnidadesConversion Insert(INVENTARIO_UnidadesConversion iNVENTARIO_UnidadesConversion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesConversionSave")) throw new PermisoException();
            string sql = "insert into INVENTARIO_UnidadesConversion(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_UnidadesConversion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_UnidadesConversion, null));
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
            iNVENTARIO_UnidadesConversion.Id = Convert.ToInt32(resp);
            return iNVENTARIO_UnidadesConversion;
        }

        public static INVENTARIO_UnidadesConversion Update(INVENTARIO_UnidadesConversion iNVENTARIO_UnidadesConversion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesConversionSave")) throw new PermisoException();
            string sql = "update INVENTARIO_UnidadesConversion set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_UnidadesConversion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_UnidadesConversion, null));
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
            sql += " where Id = " + iNVENTARIO_UnidadesConversion.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return iNVENTARIO_UnidadesConversion;
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