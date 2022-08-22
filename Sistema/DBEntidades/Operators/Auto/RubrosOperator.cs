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
    public partial class RubrosOperator
    {

        public static Rubros GetOneByIdentity(int RubroId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubrosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Rubros).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Rubros where RubroId = " + RubroId.ToString()).Tables[0];
            Rubros rubros = new Rubros();
            foreach (PropertyInfo prop in typeof(Rubros).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(rubros, value, null); }
                catch (System.ArgumentException) { }
            }
            return rubros;
        }

        public static List<Rubros> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubrosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Rubros).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Rubros> lista = new List<Rubros>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Rubros").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Rubros rubros = new Rubros();
                foreach (PropertyInfo prop in typeof(Rubros).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(rubros, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(rubros);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 100;
			public static int LetraCodigo { get; set; } = 50;


        }

        public static Rubros Save(Rubros rubros)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubrosSave")) throw new PermisoException();
            if (rubros.RubroId == -1) return Insert(rubros);
            else return Update(rubros);
        }

        public static Rubros Insert(Rubros rubros)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubrosSave")) throw new PermisoException();
            string sql = "insert into Rubros(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Rubros).GetProperties())
            {
                if (prop.Name == "RubroId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rubros, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.RubroId values (" + valores + ")";
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
            rubros.RubroId = Convert.ToInt32(resp);
            return rubros;
        }

        public static Rubros Update(Rubros rubros)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubrosSave")) throw new PermisoException();
            string sql = "update Rubros set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Rubros).GetProperties())
            {
                if (prop.Name == "RubroId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rubros, null));
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
            sql += " where RubroId = " + rubros.RubroId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return rubros;
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
