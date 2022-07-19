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
    public partial class Fa_categoriasOperator
    {

        public static Fa_categorias GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFa_categoriasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Fa_categorias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Fa_categorias where ID = " + ID.ToString()).Tables[0];
            Fa_categorias fa_categorias = new Fa_categorias();
            foreach (PropertyInfo prop in typeof(Fa_categorias).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(fa_categorias, value, null); }
                catch (System.ArgumentException) { }
            }
            return fa_categorias;
        }

        public static List<Fa_categorias> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFa_categoriasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Fa_categorias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Fa_categorias> lista = new List<Fa_categorias>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Fa_categorias").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Fa_categorias fa_categorias = new Fa_categorias();
                foreach (PropertyInfo prop in typeof(Fa_categorias).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(fa_categorias, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(fa_categorias);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static Fa_categorias Save(Fa_categorias fa_categorias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFa_categoriasSave")) throw new PermisoException();
            if (fa_categorias.ID == -1) return Insert(fa_categorias);
            else return Update(fa_categorias);
        }

        public static Fa_categorias Insert(Fa_categorias fa_categorias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFa_categoriasSave")) throw new PermisoException();
            string sql = "insert into Fa_categorias(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Fa_categorias).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(fa_categorias, null));
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
            fa_categorias.ID = Convert.ToInt32(resp);
            return fa_categorias;
        }

        public static Fa_categorias Update(Fa_categorias fa_categorias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFa_categoriasSave")) throw new PermisoException();
            string sql = "update Fa_categorias set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Fa_categorias).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(fa_categorias, null));
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
            sql += " where ID = " + fa_categorias.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return fa_categorias;
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
