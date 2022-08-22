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
    public partial class TipoCateringOperator
    {

        public static TipoCatering GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCatering).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCatering where Id = " + Id.ToString()).Tables[0];
            TipoCatering tipoCatering = new TipoCatering();
            foreach (PropertyInfo prop in typeof(TipoCatering).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoCatering, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoCatering;
        }

        public static List<TipoCatering> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCatering).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoCatering> lista = new List<TipoCatering>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCatering").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoCatering tipoCatering = new TipoCatering();
                foreach (PropertyInfo prop in typeof(TipoCatering).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoCatering, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoCatering);
            }
            return lista;
        }

		public static List<TipoCatering> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<TipoCatering> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<TipoCatering> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<TipoCatering> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Descripcion { get; set; } = 400;
			public static int EsAdicional { get; set; } = 1;


        }

        public static TipoCatering Save(TipoCatering tipoCatering)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringSave")) throw new PermisoException();
            if (tipoCatering.Id == -1) return Insert(tipoCatering);
            else return Update(tipoCatering);
        }

        public static TipoCatering Insert(TipoCatering tipoCatering)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringSave")) throw new PermisoException();
            string sql = "insert into TipoCatering(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCatering).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCatering, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted. values (" + valores + ")";
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
            tipoCatering.Id = Convert.ToInt32(resp);
            return tipoCatering;
        }

        public static TipoCatering Update(TipoCatering tipoCatering)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringSave")) throw new PermisoException();
            string sql = "update TipoCatering set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCatering).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCatering, null));
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
            sql += " where Id = " + tipoCatering.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoCatering;
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
