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
    public partial class BancosOperator
    {

        public static Bancos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBancosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Bancos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Bancos where Id = " + Id.ToString()).Tables[0];
            Bancos bancos = new Bancos();
            foreach (PropertyInfo prop in typeof(Bancos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(bancos, value, null); }
                catch (System.ArgumentException) { }
            }
            return bancos;
        }

        public static List<Bancos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBancosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Bancos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Bancos> lista = new List<Bancos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Bancos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Bancos bancos = new Bancos();
                foreach (PropertyInfo prop in typeof(Bancos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(bancos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(bancos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Codigo { get; set; } = 50;
			public static int Descripcion { get; set; } = 200;


        }

        public static Bancos Save(Bancos bancos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBancosSave")) throw new PermisoException();
            if (bancos.Id == -1) return Insert(bancos);
            else return Update(bancos);
        }

        public static Bancos Insert(Bancos bancos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBancosSave")) throw new PermisoException();
            string sql = "insert into Bancos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Bancos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(bancos, null));
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
            bancos.Id = Convert.ToInt32(resp);
            return bancos;
        }

        public static Bancos Update(Bancos bancos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBancosSave")) throw new PermisoException();
            string sql = "update Bancos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Bancos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(bancos, null));
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
            sql += " where Id = " + bancos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return bancos;
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
