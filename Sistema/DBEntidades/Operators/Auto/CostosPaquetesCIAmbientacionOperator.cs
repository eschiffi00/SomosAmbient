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
    public partial class CostosPaquetesCIAmbientacionOperator
    {

        public static CostosPaquetesCIAmbientacion GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostosPaquetesCIAmbientacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostosPaquetesCIAmbientacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostosPaquetesCIAmbientacion where Id = " + Id.ToString()).Tables[0];
            CostosPaquetesCIAmbientacion costosPaquetesCIAmbientacion = new CostosPaquetesCIAmbientacion();
            foreach (PropertyInfo prop in typeof(CostosPaquetesCIAmbientacion).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(costosPaquetesCIAmbientacion, value, null); }
                catch (System.ArgumentException) { }
            }
            return costosPaquetesCIAmbientacion;
        }

        public static List<CostosPaquetesCIAmbientacion> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostosPaquetesCIAmbientacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostosPaquetesCIAmbientacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CostosPaquetesCIAmbientacion> lista = new List<CostosPaquetesCIAmbientacion>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostosPaquetesCIAmbientacion").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CostosPaquetesCIAmbientacion costosPaquetesCIAmbientacion = new CostosPaquetesCIAmbientacion();
                foreach (PropertyInfo prop in typeof(CostosPaquetesCIAmbientacion).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(costosPaquetesCIAmbientacion, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(costosPaquetesCIAmbientacion);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static CostosPaquetesCIAmbientacion Save(CostosPaquetesCIAmbientacion costosPaquetesCIAmbientacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostosPaquetesCIAmbientacionSave")) throw new PermisoException();
            if (costosPaquetesCIAmbientacion.Id == -1) return Insert(costosPaquetesCIAmbientacion);
            else return Update(costosPaquetesCIAmbientacion);
        }

        public static CostosPaquetesCIAmbientacion Insert(CostosPaquetesCIAmbientacion costosPaquetesCIAmbientacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostosPaquetesCIAmbientacionSave")) throw new PermisoException();
            string sql = "insert into CostosPaquetesCIAmbientacion(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostosPaquetesCIAmbientacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costosPaquetesCIAmbientacion, null));
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
            costosPaquetesCIAmbientacion.Id = Convert.ToInt32(resp);
            return costosPaquetesCIAmbientacion;
        }

        public static CostosPaquetesCIAmbientacion Update(CostosPaquetesCIAmbientacion costosPaquetesCIAmbientacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostosPaquetesCIAmbientacionSave")) throw new PermisoException();
            string sql = "update CostosPaquetesCIAmbientacion set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostosPaquetesCIAmbientacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costosPaquetesCIAmbientacion, null));
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
            sql += " where Id = " + costosPaquetesCIAmbientacion.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return costosPaquetesCIAmbientacion;
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
