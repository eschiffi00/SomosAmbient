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
    public partial class FamiliasOperator
    {

        public static Familias GetOneByIdentity(int GrupoId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFamiliasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Familias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Familias where Id = " + GrupoId.ToString()).Tables[0];
            Familias familias = new Familias();
            foreach (PropertyInfo prop in typeof(Familias).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(familias, value, null); }
                catch (System.ArgumentException) { }
            }
            return familias;
        }

        public static List<Familias> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFamiliasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Familias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Familias> lista = new List<Familias>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Familias").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Familias familias = new Familias();
                foreach (PropertyInfo prop in typeof(Familias).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(familias, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(familias);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Titulo { get; set; } = 200;
			public static int Subtitulo { get; set; } = 200;
			public static int Comentario { get; set; } = 2000;
			public static int Edad { get; set; } = 50;
			public static int Fantasia { get; set; } = 500;


        }

        public static Familias Save(Familias familias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFamiliasSave")) throw new PermisoException();
            if (familias.GrupoId == -1) return Insert(familias);
            else return Update(familias);
        }

        public static Familias Insert(Familias familias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFamiliasSave")) throw new PermisoException();
            string sql = "insert into Familias(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Familias).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(familias, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted. values (" + valores + ")";
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
            familias.GrupoId = Convert.ToInt32(resp);
            return familias;
        }

        public static Familias Update(Familias familias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFamiliasSave")) throw new PermisoException();
            string sql = "update Familias set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Familias).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(familias, null));
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
            sql += " where GrupoId = " + familias.GrupoId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return familias;
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
