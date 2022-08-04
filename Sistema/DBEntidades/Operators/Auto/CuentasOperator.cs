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
    public partial class CuentasOperator
    {

        public static Cuentas GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cuentas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cuentas where Id = " + Id.ToString()).Tables[0];
            Cuentas cuentas = new Cuentas();
            foreach (PropertyInfo prop in typeof(Cuentas).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(cuentas, value, null); }
                catch (System.ArgumentException) { }
            }
            return cuentas;
        }

        public static List<Cuentas> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cuentas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Cuentas> lista = new List<Cuentas>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cuentas").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Cuentas cuentas = new Cuentas();
                foreach (PropertyInfo prop in typeof(Cuentas).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(cuentas, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(cuentas);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Nombre { get; set; } = 50;
			public static int Descripcion { get; set; } = 200;
			public static int TipoCuenta { get; set; } = 50;


        }

        public static Cuentas Save(Cuentas cuentas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentasSave")) throw new PermisoException();
            if (cuentas.Id == -1) return Insert(cuentas);
            else return Update(cuentas);
        }

        public static Cuentas Insert(Cuentas cuentas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentasSave")) throw new PermisoException();
            string sql = "insert into Cuentas(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cuentas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cuentas, null));
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
            cuentas.Id = Convert.ToInt32(resp);
            return cuentas;
        }

        public static Cuentas Update(Cuentas cuentas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCuentasSave")) throw new PermisoException();
            string sql = "update Cuentas set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cuentas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cuentas, null));
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
            sql += " where Id = " + cuentas.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return cuentas;
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
