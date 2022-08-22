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
    public partial class AmbientacionSalonOperator
    {

        public static AmbientacionSalon GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAmbientacionSalonBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(AmbientacionSalon).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from AmbientacionSalon where Id = " + Id.ToString()).Tables[0];
            AmbientacionSalon ambientacionSalon = new AmbientacionSalon();
            foreach (PropertyInfo prop in typeof(AmbientacionSalon).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(ambientacionSalon, value, null); }
                catch (System.ArgumentException) { }
            }
            return ambientacionSalon;
        }

        public static List<AmbientacionSalon> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAmbientacionSalonBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(AmbientacionSalon).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<AmbientacionSalon> lista = new List<AmbientacionSalon>();
            DataTable dt = db.GetDataSet("select " + columnas + " from AmbientacionSalon").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                AmbientacionSalon ambientacionSalon = new AmbientacionSalon();
                foreach (PropertyInfo prop in typeof(AmbientacionSalon).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(ambientacionSalon, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(ambientacionSalon);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static AmbientacionSalon Save(AmbientacionSalon ambientacionSalon)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAmbientacionSalonSave")) throw new PermisoException();
            if (ambientacionSalon.Id == -1) return Insert(ambientacionSalon);
            else return Update(ambientacionSalon);
        }

        public static AmbientacionSalon Insert(AmbientacionSalon ambientacionSalon)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAmbientacionSalonSave")) throw new PermisoException();
            string sql = "insert into AmbientacionSalon(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(AmbientacionSalon).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(ambientacionSalon, null));
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
            ambientacionSalon.Id = Convert.ToInt32(resp);
            return ambientacionSalon;
        }

        public static AmbientacionSalon Update(AmbientacionSalon ambientacionSalon)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAmbientacionSalonSave")) throw new PermisoException();
            string sql = "update AmbientacionSalon set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(AmbientacionSalon).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(ambientacionSalon, null));
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
            sql += " where Id = " + ambientacionSalon.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return ambientacionSalon;
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
