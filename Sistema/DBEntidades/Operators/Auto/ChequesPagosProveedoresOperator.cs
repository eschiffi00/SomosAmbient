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
    public partial class ChequesPagosProveedoresOperator
    {

        public static ChequesPagosProveedores GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesPagosProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ChequesPagosProveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ChequesPagosProveedores where Id = " + Id.ToString()).Tables[0];
            ChequesPagosProveedores chequesPagosProveedores = new ChequesPagosProveedores();
            foreach (PropertyInfo prop in typeof(ChequesPagosProveedores).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(chequesPagosProveedores, value, null); }
                catch (System.ArgumentException) { }
            }
            return chequesPagosProveedores;
        }

        public static List<ChequesPagosProveedores> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesPagosProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ChequesPagosProveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ChequesPagosProveedores> lista = new List<ChequesPagosProveedores>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ChequesPagosProveedores").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ChequesPagosProveedores chequesPagosProveedores = new ChequesPagosProveedores();
                foreach (PropertyInfo prop in typeof(ChequesPagosProveedores).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(chequesPagosProveedores, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(chequesPagosProveedores);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static ChequesPagosProveedores Save(ChequesPagosProveedores chequesPagosProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesPagosProveedoresSave")) throw new PermisoException();
            if (chequesPagosProveedores.Id == -1) return Insert(chequesPagosProveedores);
            else return Update(chequesPagosProveedores);
        }

        public static ChequesPagosProveedores Insert(ChequesPagosProveedores chequesPagosProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesPagosProveedoresSave")) throw new PermisoException();
            string sql = "insert into ChequesPagosProveedores(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ChequesPagosProveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(chequesPagosProveedores, null));
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
            chequesPagosProveedores.Id = Convert.ToInt32(resp);
            return chequesPagosProveedores;
        }

        public static ChequesPagosProveedores Update(ChequesPagosProveedores chequesPagosProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesPagosProveedoresSave")) throw new PermisoException();
            string sql = "update ChequesPagosProveedores set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ChequesPagosProveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(chequesPagosProveedores, null));
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
            sql += " where Id = " + chequesPagosProveedores.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return chequesPagosProveedores;
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
