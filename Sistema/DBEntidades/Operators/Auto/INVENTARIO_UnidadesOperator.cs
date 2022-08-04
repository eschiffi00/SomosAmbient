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
    public partial class INVENTARIO_UnidadesOperator
    {

        public static INVENTARIO_Unidades GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Unidades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Unidades where Id = " + Id.ToString()).Tables[0];
            INVENTARIO_Unidades iNVENTARIO_Unidades = new INVENTARIO_Unidades();
            foreach (PropertyInfo prop in typeof(INVENTARIO_Unidades).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(iNVENTARIO_Unidades, value, null); }
                catch (System.ArgumentException) { }
            }
            return iNVENTARIO_Unidades;
        }

        public static List<INVENTARIO_Unidades> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Unidades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<INVENTARIO_Unidades> lista = new List<INVENTARIO_Unidades>();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Unidades").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                INVENTARIO_Unidades iNVENTARIO_Unidades = new INVENTARIO_Unidades();
                foreach (PropertyInfo prop in typeof(INVENTARIO_Unidades).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(iNVENTARIO_Unidades, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(iNVENTARIO_Unidades);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 100;
			public static int DescripcionCorta { get; set; } = 10;


        }

        public static INVENTARIO_Unidades Save(INVENTARIO_Unidades iNVENTARIO_Unidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesSave")) throw new PermisoException();
            if (iNVENTARIO_Unidades.Id == -1) return Insert(iNVENTARIO_Unidades);
            else return Update(iNVENTARIO_Unidades);
        }

        public static INVENTARIO_Unidades Insert(INVENTARIO_Unidades iNVENTARIO_Unidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesSave")) throw new PermisoException();
            string sql = "insert into INVENTARIO_Unidades(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Unidades).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Unidades, null));
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
            iNVENTARIO_Unidades.Id = Convert.ToInt32(resp);
            return iNVENTARIO_Unidades;
        }

        public static INVENTARIO_Unidades Update(INVENTARIO_Unidades iNVENTARIO_Unidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_UnidadesSave")) throw new PermisoException();
            string sql = "update INVENTARIO_Unidades set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Unidades).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Unidades, null));
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
            sql += " where Id = " + iNVENTARIO_Unidades.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return iNVENTARIO_Unidades;
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
