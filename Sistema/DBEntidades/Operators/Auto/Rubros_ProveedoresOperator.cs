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
    public partial class Rubros_ProveedoresOperator
    {

        public static Rubros_Proveedores GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubros_ProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Rubros_Proveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Rubros_Proveedores where Id = " + Id.ToString()).Tables[0];
            Rubros_Proveedores rubros_Proveedores = new Rubros_Proveedores();
            foreach (PropertyInfo prop in typeof(Rubros_Proveedores).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(rubros_Proveedores, value, null); }
                catch (System.ArgumentException) { }
            }
            return rubros_Proveedores;
        }

        public static List<Rubros_Proveedores> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubros_ProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Rubros_Proveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Rubros_Proveedores> lista = new List<Rubros_Proveedores>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Rubros_Proveedores").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Rubros_Proveedores rubros_Proveedores = new Rubros_Proveedores();
                foreach (PropertyInfo prop in typeof(Rubros_Proveedores).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(rubros_Proveedores, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(rubros_Proveedores);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static Rubros_Proveedores Save(Rubros_Proveedores rubros_Proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubros_ProveedoresSave")) throw new PermisoException();
            if (rubros_Proveedores.Id == -1) return Insert(rubros_Proveedores);
            else return Update(rubros_Proveedores);
        }

        public static Rubros_Proveedores Insert(Rubros_Proveedores rubros_Proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubros_ProveedoresSave")) throw new PermisoException();
            string sql = "insert into Rubros_Proveedores(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Rubros_Proveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rubros_Proveedores, null));
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
            rubros_Proveedores.Id = Convert.ToInt32(resp);
            return rubros_Proveedores;
        }

        public static Rubros_Proveedores Update(Rubros_Proveedores rubros_Proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRubros_ProveedoresSave")) throw new PermisoException();
            string sql = "update Rubros_Proveedores set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Rubros_Proveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rubros_Proveedores, null));
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
            sql += " where Id = " + rubros_Proveedores.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return rubros_Proveedores;
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
