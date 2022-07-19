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
    public partial class MomentoOperator
    {

        public static Momento GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMomentoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Momento).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Momento where ID = " + ID.ToString()).Tables[0];
            Momento momento = new Momento();
            foreach (PropertyInfo prop in typeof(Momento).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(momento, value, null); }
                catch (System.ArgumentException) { }
            }
            return momento;
        }

        public static List<Momento> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMomentoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Momento).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Momento> lista = new List<Momento>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Momento").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Momento momento = new Momento();
                foreach (PropertyInfo prop in typeof(Momento).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(momento, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(momento);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static Momento Save(Momento momento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMomentoSave")) throw new PermisoException();
            if (momento.ID == -1) return Insert(momento);
            else return Update(momento);
        }

        public static Momento Insert(Momento momento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMomentoSave")) throw new PermisoException();
            string sql = "insert into Momento(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Momento).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(momento, null));
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
            momento.ID = Convert.ToInt32(resp);
            return momento;
        }

        public static Momento Update(Momento momento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMomentoSave")) throw new PermisoException();
            string sql = "update Momento set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Momento).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(momento, null));
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
            sql += " where ID = " + momento.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return momento;
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
