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
    public partial class TipoComprobante_ImpuestosOperator
    {

        public static TipoComprobante_Impuestos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoComprobante_ImpuestosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoComprobante_Impuestos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoComprobante_Impuestos where Id = " + Id.ToString()).Tables[0];
            TipoComprobante_Impuestos tipoComprobante_Impuestos = new TipoComprobante_Impuestos();
            foreach (PropertyInfo prop in typeof(TipoComprobante_Impuestos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoComprobante_Impuestos, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoComprobante_Impuestos;
        }

        public static List<TipoComprobante_Impuestos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoComprobante_ImpuestosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoComprobante_Impuestos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoComprobante_Impuestos> lista = new List<TipoComprobante_Impuestos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoComprobante_Impuestos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoComprobante_Impuestos tipoComprobante_Impuestos = new TipoComprobante_Impuestos();
                foreach (PropertyInfo prop in typeof(TipoComprobante_Impuestos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoComprobante_Impuestos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoComprobante_Impuestos);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static TipoComprobante_Impuestos Save(TipoComprobante_Impuestos tipoComprobante_Impuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoComprobante_ImpuestosSave")) throw new PermisoException();
            if (tipoComprobante_Impuestos.Id == -1) return Insert(tipoComprobante_Impuestos);
            else return Update(tipoComprobante_Impuestos);
        }

        public static TipoComprobante_Impuestos Insert(TipoComprobante_Impuestos tipoComprobante_Impuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoComprobante_ImpuestosSave")) throw new PermisoException();
            string sql = "insert into TipoComprobante_Impuestos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoComprobante_Impuestos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoComprobante_Impuestos, null));
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
            tipoComprobante_Impuestos.Id = Convert.ToInt32(resp);
            return tipoComprobante_Impuestos;
        }

        public static TipoComprobante_Impuestos Update(TipoComprobante_Impuestos tipoComprobante_Impuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoComprobante_ImpuestosSave")) throw new PermisoException();
            string sql = "update TipoComprobante_Impuestos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoComprobante_Impuestos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoComprobante_Impuestos, null));
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
            sql += " where Id = " + tipoComprobante_Impuestos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoComprobante_Impuestos;
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
