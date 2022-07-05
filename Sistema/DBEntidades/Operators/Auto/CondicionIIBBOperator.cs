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
    public partial class CondicionIIBBOperator
    {

        public static CondicionIIBB GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIIBBBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CondicionIIBB).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CondicionIIBB where Id = " + Id.ToString()).Tables[0];
            CondicionIIBB condicionIIBB = new CondicionIIBB();
            foreach (PropertyInfo prop in typeof(CondicionIIBB).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(condicionIIBB, value, null); }
                catch (System.ArgumentException) { }
            }
            return condicionIIBB;
        }

        public static List<CondicionIIBB> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIIBBBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CondicionIIBB).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CondicionIIBB> lista = new List<CondicionIIBB>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CondicionIIBB").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CondicionIIBB condicionIIBB = new CondicionIIBB();
                foreach (PropertyInfo prop in typeof(CondicionIIBB).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(condicionIIBB, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(condicionIIBB);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static CondicionIIBB Save(CondicionIIBB condicionIIBB)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIIBBSave")) throw new PermisoException();
            if (condicionIIBB.Id == -1) return Insert(condicionIIBB);
            else return Update(condicionIIBB);
        }

        public static CondicionIIBB Insert(CondicionIIBB condicionIIBB)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIIBBSave")) throw new PermisoException();
            string sql = "insert into CondicionIIBB(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CondicionIIBB).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(condicionIIBB, null));
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
            condicionIIBB.Id = Convert.ToInt32(resp);
            return condicionIIBB;
        }

        public static CondicionIIBB Update(CondicionIIBB condicionIIBB)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCondicionIIBBSave")) throw new PermisoException();
            string sql = "update CondicionIIBB set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CondicionIIBB).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(condicionIIBB, null));
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
            sql += " where Id = " + condicionIIBB.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return condicionIIBB;
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
