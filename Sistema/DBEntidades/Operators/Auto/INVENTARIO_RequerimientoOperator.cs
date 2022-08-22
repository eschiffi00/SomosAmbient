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
    public partial class INVENTARIO_RequerimientoOperator
    {

        public static INVENTARIO_Requerimiento GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_RequerimientoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Requerimiento where Id = " + Id.ToString()).Tables[0];
            INVENTARIO_Requerimiento iNVENTARIO_Requerimiento = new INVENTARIO_Requerimiento();
            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(iNVENTARIO_Requerimiento, value, null); }
                catch (System.ArgumentException) { }
            }
            return iNVENTARIO_Requerimiento;
        }

        public static List<INVENTARIO_Requerimiento> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_RequerimientoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<INVENTARIO_Requerimiento> lista = new List<INVENTARIO_Requerimiento>();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Requerimiento").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                INVENTARIO_Requerimiento iNVENTARIO_Requerimiento = new INVENTARIO_Requerimiento();
                foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(iNVENTARIO_Requerimiento, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(iNVENTARIO_Requerimiento);
            }
            return lista;
        }

		public static List<INVENTARIO_Requerimiento> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<INVENTARIO_Requerimiento> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<INVENTARIO_Requerimiento> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<INVENTARIO_Requerimiento> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Detalle { get; set; } = 1000;


        }

        public static INVENTARIO_Requerimiento Save(INVENTARIO_Requerimiento iNVENTARIO_Requerimiento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_RequerimientoSave")) throw new PermisoException();
            if (iNVENTARIO_Requerimiento.Id == -1) return Insert(iNVENTARIO_Requerimiento);
            else return Update(iNVENTARIO_Requerimiento);
        }

        public static INVENTARIO_Requerimiento Insert(INVENTARIO_Requerimiento iNVENTARIO_Requerimiento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_RequerimientoSave")) throw new PermisoException();
            string sql = "insert into INVENTARIO_Requerimiento(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Requerimiento, null));
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
            iNVENTARIO_Requerimiento.Id = Convert.ToInt32(resp);
            return iNVENTARIO_Requerimiento;
        }

        public static INVENTARIO_Requerimiento Update(INVENTARIO_Requerimiento iNVENTARIO_Requerimiento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_RequerimientoSave")) throw new PermisoException();
            string sql = "update INVENTARIO_Requerimiento set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Requerimiento).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Requerimiento, null));
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
            sql += " where Id = " + iNVENTARIO_Requerimiento.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return iNVENTARIO_Requerimiento;
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
