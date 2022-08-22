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
    public partial class CentroCostosOperator
    {

        public static CentroCostos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCentroCostosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CentroCostos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CentroCostos where Id = " + Id.ToString()).Tables[0];
            CentroCostos centroCostos = new CentroCostos();
            foreach (PropertyInfo prop in typeof(CentroCostos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(centroCostos, value, null); }
                catch (System.ArgumentException) { }
            }
            return centroCostos;
        }

        public static List<CentroCostos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCentroCostosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CentroCostos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CentroCostos> lista = new List<CentroCostos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CentroCostos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CentroCostos centroCostos = new CentroCostos();
                foreach (PropertyInfo prop in typeof(CentroCostos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(centroCostos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(centroCostos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 300;


        }

        public static CentroCostos Save(CentroCostos centroCostos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCentroCostosSave")) throw new PermisoException();
            if (centroCostos.Id == -1) return Insert(centroCostos);
            else return Update(centroCostos);
        }

        public static CentroCostos Insert(CentroCostos centroCostos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCentroCostosSave")) throw new PermisoException();
            string sql = "insert into CentroCostos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CentroCostos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(centroCostos, null));
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
            centroCostos.Id = Convert.ToInt32(resp);
            return centroCostos;
        }

        public static CentroCostos Update(CentroCostos centroCostos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCentroCostosSave")) throw new PermisoException();
            string sql = "update CentroCostos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CentroCostos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(centroCostos, null));
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
            sql += " where Id = " + centroCostos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return centroCostos;
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
