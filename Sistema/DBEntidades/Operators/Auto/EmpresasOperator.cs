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
    public partial class EmpresasOperator
    {

        public static Empresas GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEmpresasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Empresas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Empresas where Id = " + Id.ToString()).Tables[0];
            Empresas empresas = new Empresas();
            foreach (PropertyInfo prop in typeof(Empresas).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(empresas, value, null); }
                catch (System.ArgumentException) { }
            }
            return empresas;
        }

        public static List<Empresas> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEmpresasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Empresas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Empresas> lista = new List<Empresas>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Empresas").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Empresas empresas = new Empresas();
                foreach (PropertyInfo prop in typeof(Empresas).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(empresas, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(empresas);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int RazonSocial { get; set; } = 50;


        }

        public static Empresas Save(Empresas empresas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEmpresasSave")) throw new PermisoException();
            if (empresas.Id == -1) return Insert(empresas);
            else return Update(empresas);
        }

        public static Empresas Insert(Empresas empresas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEmpresasSave")) throw new PermisoException();
            string sql = "insert into Empresas(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Empresas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(empresas, null));
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
            empresas.Id = Convert.ToInt32(resp);
            return empresas;
        }

        public static Empresas Update(Empresas empresas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEmpresasSave")) throw new PermisoException();
            string sql = "update Empresas set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Empresas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(empresas, null));
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
            sql += " where Id = " + empresas.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return empresas;
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
