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
    public partial class TipoDocumentosOperator
    {

        public static TipoDocumentos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoDocumentosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoDocumentos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoDocumentos where Id = " + Id.ToString()).Tables[0];
            TipoDocumentos tipoDocumentos = new TipoDocumentos();
            foreach (PropertyInfo prop in typeof(TipoDocumentos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoDocumentos, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoDocumentos;
        }

        public static List<TipoDocumentos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoDocumentosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoDocumentos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoDocumentos> lista = new List<TipoDocumentos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoDocumentos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoDocumentos tipoDocumentos = new TipoDocumentos();
                foreach (PropertyInfo prop in typeof(TipoDocumentos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoDocumentos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoDocumentos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static TipoDocumentos Save(TipoDocumentos tipoDocumentos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoDocumentosSave")) throw new PermisoException();
            if (tipoDocumentos.Id == -1) return Insert(tipoDocumentos);
            else return Update(tipoDocumentos);
        }

        public static TipoDocumentos Insert(TipoDocumentos tipoDocumentos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoDocumentosSave")) throw new PermisoException();
            string sql = "insert into TipoDocumentos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoDocumentos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoDocumentos, null));
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
            tipoDocumentos.Id = Convert.ToInt32(resp);
            return tipoDocumentos;
        }

        public static TipoDocumentos Update(TipoDocumentos tipoDocumentos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoDocumentosSave")) throw new PermisoException();
            string sql = "update TipoDocumentos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoDocumentos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoDocumentos, null));
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
            sql += " where Id = " + tipoDocumentos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoDocumentos;
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
