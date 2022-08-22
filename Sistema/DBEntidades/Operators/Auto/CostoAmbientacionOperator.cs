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
    public partial class CostoAmbientacionOperator
    {

        public static CostoAmbientacion GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAmbientacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoAmbientacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoAmbientacion where Id = " + Id.ToString()).Tables[0];
            CostoAmbientacion costoAmbientacion = new CostoAmbientacion();
            foreach (PropertyInfo prop in typeof(CostoAmbientacion).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(costoAmbientacion, value, null); }
                catch (System.ArgumentException) { }
            }
            return costoAmbientacion;
        }

        public static List<CostoAmbientacion> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAmbientacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoAmbientacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CostoAmbientacion> lista = new List<CostoAmbientacion>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoAmbientacion").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CostoAmbientacion costoAmbientacion = new CostoAmbientacion();
                foreach (PropertyInfo prop in typeof(CostoAmbientacion).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(costoAmbientacion, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(costoAmbientacion);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static CostoAmbientacion Save(CostoAmbientacion costoAmbientacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAmbientacionSave")) throw new PermisoException();
            if (costoAmbientacion.Id == -1) return Insert(costoAmbientacion);
            else return Update(costoAmbientacion);
        }

        public static CostoAmbientacion Insert(CostoAmbientacion costoAmbientacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAmbientacionSave")) throw new PermisoException();
            string sql = "insert into CostoAmbientacion(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoAmbientacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoAmbientacion, null));
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
            costoAmbientacion.Id = Convert.ToInt32(resp);
            return costoAmbientacion;
        }

        public static CostoAmbientacion Update(CostoAmbientacion costoAmbientacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAmbientacionSave")) throw new PermisoException();
            string sql = "update CostoAmbientacion set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoAmbientacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoAmbientacion, null));
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
            sql += " where Id = " + costoAmbientacion.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return costoAmbientacion;
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
