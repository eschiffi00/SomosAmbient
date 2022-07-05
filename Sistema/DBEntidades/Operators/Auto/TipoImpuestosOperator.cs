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
    public partial class TipoImpuestosOperator
    {

        public static TipoImpuestos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoImpuestosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoImpuestos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoImpuestos where Id = " + Id.ToString()).Tables[0];
            TipoImpuestos tipoImpuestos = new TipoImpuestos();
            foreach (PropertyInfo prop in typeof(TipoImpuestos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoImpuestos, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoImpuestos;
        }

        public static List<TipoImpuestos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoImpuestosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoImpuestos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoImpuestos> lista = new List<TipoImpuestos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoImpuestos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoImpuestos tipoImpuestos = new TipoImpuestos();
                foreach (PropertyInfo prop in typeof(TipoImpuestos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoImpuestos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoImpuestos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static TipoImpuestos Save(TipoImpuestos tipoImpuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoImpuestosSave")) throw new PermisoException();
            if (tipoImpuestos.Id == -1) return Insert(tipoImpuestos);
            else return Update(tipoImpuestos);
        }

        public static TipoImpuestos Insert(TipoImpuestos tipoImpuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoImpuestosSave")) throw new PermisoException();
            string sql = "insert into TipoImpuestos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoImpuestos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoImpuestos, null));
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
            tipoImpuestos.Id = Convert.ToInt32(resp);
            return tipoImpuestos;
        }

        public static TipoImpuestos Update(TipoImpuestos tipoImpuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoImpuestosSave")) throw new PermisoException();
            string sql = "update TipoImpuestos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoImpuestos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoImpuestos, null));
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
            sql += " where Id = " + tipoImpuestos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoImpuestos;
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
