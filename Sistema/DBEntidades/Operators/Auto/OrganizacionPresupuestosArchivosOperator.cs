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
    public partial class OrganizacionPresupuestosArchivosOperator
    {

        public static OrganizacionPresupuestosArchivos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestosArchivosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestosArchivos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestosArchivos where Id = " + Id.ToString()).Tables[0];
            OrganizacionPresupuestosArchivos organizacionPresupuestosArchivos = new OrganizacionPresupuestosArchivos();
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestosArchivos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(organizacionPresupuestosArchivos, value, null); }
                catch (System.ArgumentException) { }
            }
            return organizacionPresupuestosArchivos;
        }

        public static List<OrganizacionPresupuestosArchivos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestosArchivosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestosArchivos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<OrganizacionPresupuestosArchivos> lista = new List<OrganizacionPresupuestosArchivos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestosArchivos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                OrganizacionPresupuestosArchivos organizacionPresupuestosArchivos = new OrganizacionPresupuestosArchivos();
                foreach (PropertyInfo prop in typeof(OrganizacionPresupuestosArchivos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(organizacionPresupuestosArchivos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(organizacionPresupuestosArchivos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Desripcion { get; set; } = 200;
			public static int NombreArchivo { get; set; } = 500;
			public static int Extension { get; set; } = 50;


        }

        public static OrganizacionPresupuestosArchivos Save(OrganizacionPresupuestosArchivos organizacionPresupuestosArchivos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestosArchivosSave")) throw new PermisoException();
            if (organizacionPresupuestosArchivos.Id == -1) return Insert(organizacionPresupuestosArchivos);
            else return Update(organizacionPresupuestosArchivos);
        }

        public static OrganizacionPresupuestosArchivos Insert(OrganizacionPresupuestosArchivos organizacionPresupuestosArchivos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestosArchivosSave")) throw new PermisoException();
            string sql = "insert into OrganizacionPresupuestosArchivos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestosArchivos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestosArchivos, null));
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
            organizacionPresupuestosArchivos.Id = Convert.ToInt32(resp);
            return organizacionPresupuestosArchivos;
        }

        public static OrganizacionPresupuestosArchivos Update(OrganizacionPresupuestosArchivos organizacionPresupuestosArchivos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestosArchivosSave")) throw new PermisoException();
            string sql = "update OrganizacionPresupuestosArchivos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestosArchivos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestosArchivos, null));
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
            sql += " where Id = " + organizacionPresupuestosArchivos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return organizacionPresupuestosArchivos;
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
