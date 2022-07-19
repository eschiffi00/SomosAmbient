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
    public partial class ExperienciaOperator
    {

        public static Experiencia GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExperienciaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Experiencia).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Experiencia where ID = " + ID.ToString()).Tables[0];
            Experiencia experiencia = new Experiencia();
            foreach (PropertyInfo prop in typeof(Experiencia).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(experiencia, value, null); }
                catch (System.ArgumentException) { }
            }
            return experiencia;
        }

        public static List<Experiencia> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExperienciaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Experiencia).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Experiencia> lista = new List<Experiencia>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Experiencia").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Experiencia experiencia = new Experiencia();
                foreach (PropertyInfo prop in typeof(Experiencia).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(experiencia, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(experiencia);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static Experiencia Save(Experiencia experiencia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExperienciaSave")) throw new PermisoException();
            if (experiencia.ID == -1) return Insert(experiencia);
            else return Update(experiencia);
        }

        public static Experiencia Insert(Experiencia experiencia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExperienciaSave")) throw new PermisoException();
            string sql = "insert into Experiencia(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Experiencia).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(experiencia, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.ID values (" + valores + ")";
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
            experiencia.ID = Convert.ToInt32(resp);
            return experiencia;
        }

        public static Experiencia Update(Experiencia experiencia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExperienciaSave")) throw new PermisoException();
            string sql = "update Experiencia set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Experiencia).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(experiencia, null));
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
            sql += " where ID = " + experiencia.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return experiencia;
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
