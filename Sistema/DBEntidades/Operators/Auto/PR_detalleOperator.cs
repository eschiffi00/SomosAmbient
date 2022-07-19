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
    public partial class PR_detalleOperator
    {

        public static PR_detalle GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPR_detalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PR_detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from PR_detalle where ID = " + ID.ToString()).Tables[0];
            PR_detalle pR_detalle = new PR_detalle();
            foreach (PropertyInfo prop in typeof(PR_detalle).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(pR_detalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return pR_detalle;
        }

        public static List<PR_detalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPR_detalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PR_detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<PR_detalle> lista = new List<PR_detalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from PR_detalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                PR_detalle pR_detalle = new PR_detalle();
                foreach (PropertyInfo prop in typeof(PR_detalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(pR_detalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(pR_detalle);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int TipoRelacion { get; set; } = 50;


        }

        public static PR_detalle Save(PR_detalle pR_detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPR_detalleSave")) throw new PermisoException();
            if (pR_detalle.ID == -1) return Insert(pR_detalle);
            else return Update(pR_detalle);
        }

        public static PR_detalle Insert(PR_detalle pR_detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPR_detalleSave")) throw new PermisoException();
            string sql = "insert into PR_detalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PR_detalle).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pR_detalle, null));
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
            pR_detalle.ID = Convert.ToInt32(resp);
            return pR_detalle;
        }

        public static PR_detalle Update(PR_detalle pR_detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPR_detalleSave")) throw new PermisoException();
            string sql = "update PR_detalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PR_detalle).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pR_detalle, null));
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
            sql += " where ID = " + pR_detalle.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return pR_detalle;
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
