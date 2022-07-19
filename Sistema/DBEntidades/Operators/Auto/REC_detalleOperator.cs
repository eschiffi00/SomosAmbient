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
    public partial class REC_detalleOperator
    {

        public static REC_detalle GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoREC_detalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from REC_detalle where ID = " + ID.ToString()).Tables[0];
            REC_detalle rEC_detalle = new REC_detalle();
            foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(rEC_detalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return rEC_detalle;
        }

        public static List<REC_detalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoREC_detalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<REC_detalle> lista = new List<REC_detalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from REC_detalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                REC_detalle rEC_detalle = new REC_detalle();
                foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(rEC_detalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(rEC_detalle);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int TipoRelacion { get; set; } = 50;


        }

        public static REC_detalle Save(REC_detalle rEC_detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoREC_detalleSave")) throw new PermisoException();
            if (rEC_detalle.ID == -1) return Insert(rEC_detalle);
            else return Update(rEC_detalle);
        }

        public static REC_detalle Insert(REC_detalle rEC_detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoREC_detalleSave")) throw new PermisoException();
            string sql = "insert into REC_detalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rEC_detalle, null));
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
            rEC_detalle.ID = Convert.ToInt32(resp);
            return rEC_detalle;
        }

        public static REC_detalle Update(REC_detalle rEC_detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoREC_detalleSave")) throw new PermisoException();
            string sql = "update REC_detalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rEC_detalle, null));
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
            sql += " where ID = " + rEC_detalle.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return rEC_detalle;
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
