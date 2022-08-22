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
    public partial class INVENTARIO_Requerimiento_DetalleOperator
    {

        public static INVENTARIO_Requerimiento_Detalle GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_Requerimiento_DetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento_Detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Requerimiento_Detalle where Id = " + Id.ToString()).Tables[0];
            INVENTARIO_Requerimiento_Detalle iNVENTARIO_Requerimiento_Detalle = new INVENTARIO_Requerimiento_Detalle();
            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento_Detalle).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(iNVENTARIO_Requerimiento_Detalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return iNVENTARIO_Requerimiento_Detalle;
        }

        public static List<INVENTARIO_Requerimiento_Detalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_Requerimiento_DetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento_Detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<INVENTARIO_Requerimiento_Detalle> lista = new List<INVENTARIO_Requerimiento_Detalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Requerimiento_Detalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                INVENTARIO_Requerimiento_Detalle iNVENTARIO_Requerimiento_Detalle = new INVENTARIO_Requerimiento_Detalle();
                foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento_Detalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(iNVENTARIO_Requerimiento_Detalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(iNVENTARIO_Requerimiento_Detalle);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static INVENTARIO_Requerimiento_Detalle Save(INVENTARIO_Requerimiento_Detalle iNVENTARIO_Requerimiento_Detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_Requerimiento_DetalleSave")) throw new PermisoException();
            if (iNVENTARIO_Requerimiento_Detalle.Id == -1) return Insert(iNVENTARIO_Requerimiento_Detalle);
            else return Update(iNVENTARIO_Requerimiento_Detalle);
        }

        public static INVENTARIO_Requerimiento_Detalle Insert(INVENTARIO_Requerimiento_Detalle iNVENTARIO_Requerimiento_Detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_Requerimiento_DetalleSave")) throw new PermisoException();
            string sql = "insert into INVENTARIO_Requerimiento_Detalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento_Detalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Requerimiento_Detalle, null));
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
            iNVENTARIO_Requerimiento_Detalle.Id = Convert.ToInt32(resp);
            return iNVENTARIO_Requerimiento_Detalle;
        }

        public static INVENTARIO_Requerimiento_Detalle Update(INVENTARIO_Requerimiento_Detalle iNVENTARIO_Requerimiento_Detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_Requerimiento_DetalleSave")) throw new PermisoException();
            string sql = "update INVENTARIO_Requerimiento_Detalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento_Detalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Requerimiento_Detalle, null));
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
            sql += " where Id = " + iNVENTARIO_Requerimiento_Detalle.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return iNVENTARIO_Requerimiento_Detalle;
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
