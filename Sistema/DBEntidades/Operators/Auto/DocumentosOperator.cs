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
    public partial class DocumentosOperator
    {

        public static Documentos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDocumentosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Documentos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Documentos where Id = " + Id.ToString()).Tables[0];
            Documentos documentos = new Documentos();
            foreach (PropertyInfo prop in typeof(Documentos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(documentos, value, null); }
                catch (System.ArgumentException) { }
            }
            return documentos;
        }

        public static List<Documentos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDocumentosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Documentos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Documentos> lista = new List<Documentos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Documentos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Documentos documentos = new Documentos();
                foreach (PropertyInfo prop in typeof(Documentos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(documentos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(documentos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int NroDocumento { get; set; } = 50;


        }

        public static Documentos Save(Documentos documentos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDocumentosSave")) throw new PermisoException();
            if (documentos.Id == -1) return Insert(documentos);
            else return Update(documentos);
        }

        public static Documentos Insert(Documentos documentos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDocumentosSave")) throw new PermisoException();
            string sql = "insert into Documentos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Documentos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(documentos, null));
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
            documentos.Id = Convert.ToInt32(resp);
            return documentos;
        }

        public static Documentos Update(Documentos documentos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDocumentosSave")) throw new PermisoException();
            string sql = "update Documentos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Documentos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(documentos, null));
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
            sql += " where Id = " + documentos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return documentos;
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
