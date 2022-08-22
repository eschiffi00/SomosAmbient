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
    public partial class OrganizacionPresupuestoTimmingOperator
    {

        public static OrganizacionPresupuestoTimming GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoTimmingBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoTimming).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestoTimming where Id = " + Id.ToString()).Tables[0];
            OrganizacionPresupuestoTimming organizacionPresupuestoTimming = new OrganizacionPresupuestoTimming();
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoTimming).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(organizacionPresupuestoTimming, value, null); }
                catch (System.ArgumentException) { }
            }
            return organizacionPresupuestoTimming;
        }

        public static List<OrganizacionPresupuestoTimming> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoTimmingBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoTimming).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<OrganizacionPresupuestoTimming> lista = new List<OrganizacionPresupuestoTimming>();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestoTimming").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                OrganizacionPresupuestoTimming organizacionPresupuestoTimming = new OrganizacionPresupuestoTimming();
                foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoTimming).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(organizacionPresupuestoTimming, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(organizacionPresupuestoTimming);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int HoraInicio { get; set; } = 50;
			public static int Descripcion { get; set; } = 200;
			public static int Duracion { get; set; } = 50;


        }

        public static OrganizacionPresupuestoTimming Save(OrganizacionPresupuestoTimming organizacionPresupuestoTimming)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoTimmingSave")) throw new PermisoException();
            if (organizacionPresupuestoTimming.Id == -1) return Insert(organizacionPresupuestoTimming);
            else return Update(organizacionPresupuestoTimming);
        }

        public static OrganizacionPresupuestoTimming Insert(OrganizacionPresupuestoTimming organizacionPresupuestoTimming)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoTimmingSave")) throw new PermisoException();
            string sql = "insert into OrganizacionPresupuestoTimming(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoTimming).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestoTimming, null));
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
            organizacionPresupuestoTimming.Id = Convert.ToInt32(resp);
            return organizacionPresupuestoTimming;
        }

        public static OrganizacionPresupuestoTimming Update(OrganizacionPresupuestoTimming organizacionPresupuestoTimming)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoTimmingSave")) throw new PermisoException();
            string sql = "update OrganizacionPresupuestoTimming set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoTimming).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestoTimming, null));
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
            sql += " where Id = " + organizacionPresupuestoTimming.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return organizacionPresupuestoTimming;
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
