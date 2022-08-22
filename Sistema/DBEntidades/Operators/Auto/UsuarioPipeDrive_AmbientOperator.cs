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
    public partial class UsuarioPipeDrive_AmbientOperator
    {

        public static UsuarioPipeDrive_Ambient GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioPipeDrive_AmbientBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UsuarioPipeDrive_Ambient).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from UsuarioPipeDrive_Ambient where Id = " + Id.ToString()).Tables[0];
            UsuarioPipeDrive_Ambient usuarioPipeDrive_Ambient = new UsuarioPipeDrive_Ambient();
            foreach (PropertyInfo prop in typeof(UsuarioPipeDrive_Ambient).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(usuarioPipeDrive_Ambient, value, null); }
                catch (System.ArgumentException) { }
            }
            return usuarioPipeDrive_Ambient;
        }

        public static List<UsuarioPipeDrive_Ambient> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioPipeDrive_AmbientBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UsuarioPipeDrive_Ambient).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<UsuarioPipeDrive_Ambient> lista = new List<UsuarioPipeDrive_Ambient>();
            DataTable dt = db.GetDataSet("select " + columnas + " from UsuarioPipeDrive_Ambient").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                UsuarioPipeDrive_Ambient usuarioPipeDrive_Ambient = new UsuarioPipeDrive_Ambient();
                foreach (PropertyInfo prop in typeof(UsuarioPipeDrive_Ambient).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(usuarioPipeDrive_Ambient, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(usuarioPipeDrive_Ambient);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static UsuarioPipeDrive_Ambient Save(UsuarioPipeDrive_Ambient usuarioPipeDrive_Ambient)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioPipeDrive_AmbientSave")) throw new PermisoException();
            if (usuarioPipeDrive_Ambient.Id == -1) return Insert(usuarioPipeDrive_Ambient);
            else return Update(usuarioPipeDrive_Ambient);
        }

        public static UsuarioPipeDrive_Ambient Insert(UsuarioPipeDrive_Ambient usuarioPipeDrive_Ambient)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioPipeDrive_AmbientSave")) throw new PermisoException();
            string sql = "insert into UsuarioPipeDrive_Ambient(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UsuarioPipeDrive_Ambient).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuarioPipeDrive_Ambient, null));
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
            usuarioPipeDrive_Ambient.Id = Convert.ToInt32(resp);
            return usuarioPipeDrive_Ambient;
        }

        public static UsuarioPipeDrive_Ambient Update(UsuarioPipeDrive_Ambient usuarioPipeDrive_Ambient)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioPipeDrive_AmbientSave")) throw new PermisoException();
            string sql = "update UsuarioPipeDrive_Ambient set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UsuarioPipeDrive_Ambient).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuarioPipeDrive_Ambient, null));
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
            sql += " where Id = " + usuarioPipeDrive_Ambient.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return usuarioPipeDrive_Ambient;
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
