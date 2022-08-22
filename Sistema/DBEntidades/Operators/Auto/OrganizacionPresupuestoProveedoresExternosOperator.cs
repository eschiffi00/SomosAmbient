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
    public partial class OrganizacionPresupuestoProveedoresExternosOperator
    {

        public static OrganizacionPresupuestoProveedoresExternos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoProveedoresExternosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoProveedoresExternos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestoProveedoresExternos where Id = " + Id.ToString()).Tables[0];
            OrganizacionPresupuestoProveedoresExternos organizacionPresupuestoProveedoresExternos = new OrganizacionPresupuestoProveedoresExternos();
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoProveedoresExternos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(organizacionPresupuestoProveedoresExternos, value, null); }
                catch (System.ArgumentException) { }
            }
            return organizacionPresupuestoProveedoresExternos;
        }

        public static List<OrganizacionPresupuestoProveedoresExternos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoProveedoresExternosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoProveedoresExternos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<OrganizacionPresupuestoProveedoresExternos> lista = new List<OrganizacionPresupuestoProveedoresExternos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestoProveedoresExternos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                OrganizacionPresupuestoProveedoresExternos organizacionPresupuestoProveedoresExternos = new OrganizacionPresupuestoProveedoresExternos();
                foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoProveedoresExternos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(organizacionPresupuestoProveedoresExternos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(organizacionPresupuestoProveedoresExternos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int ProveedorExterno { get; set; } = 200;
			public static int Rubro { get; set; } = 200;
			public static int Contacto { get; set; } = 200;
			public static int Telefono { get; set; } = 200;
			public static int Correo { get; set; } = 200;
			public static int Observaciones { get; set; } = 2000;


        }

        public static OrganizacionPresupuestoProveedoresExternos Save(OrganizacionPresupuestoProveedoresExternos organizacionPresupuestoProveedoresExternos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoProveedoresExternosSave")) throw new PermisoException();
            if (organizacionPresupuestoProveedoresExternos.Id == -1) return Insert(organizacionPresupuestoProveedoresExternos);
            else return Update(organizacionPresupuestoProveedoresExternos);
        }

        public static OrganizacionPresupuestoProveedoresExternos Insert(OrganizacionPresupuestoProveedoresExternos organizacionPresupuestoProveedoresExternos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoProveedoresExternosSave")) throw new PermisoException();
            string sql = "insert into OrganizacionPresupuestoProveedoresExternos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoProveedoresExternos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestoProveedoresExternos, null));
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
            organizacionPresupuestoProveedoresExternos.Id = Convert.ToInt32(resp);
            return organizacionPresupuestoProveedoresExternos;
        }

        public static OrganizacionPresupuestoProveedoresExternos Update(OrganizacionPresupuestoProveedoresExternos organizacionPresupuestoProveedoresExternos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoProveedoresExternosSave")) throw new PermisoException();
            string sql = "update OrganizacionPresupuestoProveedoresExternos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoProveedoresExternos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestoProveedoresExternos, null));
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
            sql += " where Id = " + organizacionPresupuestoProveedoresExternos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return organizacionPresupuestoProveedoresExternos;
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
