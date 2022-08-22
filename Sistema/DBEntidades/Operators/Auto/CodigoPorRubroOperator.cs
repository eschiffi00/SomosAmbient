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
    public partial class CodigoPorRubroOperator
    {

        public static CodigoPorRubro GetOneByIdentity(int id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCodigoPorRubroBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CodigoPorRubro).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CodigoPorRubro where id = " + id.ToString()).Tables[0];
            CodigoPorRubro codigoPorRubro = new CodigoPorRubro();
            foreach (PropertyInfo prop in typeof(CodigoPorRubro).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(codigoPorRubro, value, null); }
                catch (System.ArgumentException) { }
            }
            return codigoPorRubro;
        }

        public static List<CodigoPorRubro> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCodigoPorRubroBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CodigoPorRubro).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CodigoPorRubro> lista = new List<CodigoPorRubro>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CodigoPorRubro").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CodigoPorRubro codigoPorRubro = new CodigoPorRubro();
                foreach (PropertyInfo prop in typeof(CodigoPorRubro).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(codigoPorRubro, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(codigoPorRubro);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static CodigoPorRubro Save(CodigoPorRubro codigoPorRubro)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCodigoPorRubroSave")) throw new PermisoException();
            if (codigoPorRubro.id == -1) return Insert(codigoPorRubro);
            else return Update(codigoPorRubro);
        }

        public static CodigoPorRubro Insert(CodigoPorRubro codigoPorRubro)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCodigoPorRubroSave")) throw new PermisoException();
            string sql = "insert into CodigoPorRubro(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CodigoPorRubro).GetProperties())
            {
                if (prop.Name == "id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(codigoPorRubro, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.id values (" + valores + ")";
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
            codigoPorRubro.id = Convert.ToInt32(resp);
            return codigoPorRubro;
        }

        public static CodigoPorRubro Update(CodigoPorRubro codigoPorRubro)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCodigoPorRubroSave")) throw new PermisoException();
            string sql = "update CodigoPorRubro set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CodigoPorRubro).GetProperties())
            {
                if (prop.Name == "id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(codigoPorRubro, null));
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
            sql += " where id = " + codigoPorRubro.id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return codigoPorRubro;
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
