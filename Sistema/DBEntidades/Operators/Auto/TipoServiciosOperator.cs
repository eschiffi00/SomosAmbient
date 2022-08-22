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
    public partial class TipoServiciosOperator
    {

        public static TipoServicios GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServiciosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoServicios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoServicios where Id = " + Id.ToString()).Tables[0];
            TipoServicios tipoServicios = new TipoServicios();
            foreach (PropertyInfo prop in typeof(TipoServicios).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoServicios, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoServicios;
        }

        public static List<TipoServicios> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServiciosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoServicios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoServicios> lista = new List<TipoServicios>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoServicios").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoServicios tipoServicios = new TipoServicios();
                foreach (PropertyInfo prop in typeof(TipoServicios).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoServicios, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoServicios);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static TipoServicios Save(TipoServicios tipoServicios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServiciosSave")) throw new PermisoException();
            if (tipoServicios.Id == -1) return Insert(tipoServicios);
            else return Update(tipoServicios);
        }

        public static TipoServicios Insert(TipoServicios tipoServicios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServiciosSave")) throw new PermisoException();
            string sql = "insert into TipoServicios(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoServicios).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoServicios, null));
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
            tipoServicios.Id = Convert.ToInt32(resp);
            return tipoServicios;
        }

        public static TipoServicios Update(TipoServicios tipoServicios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServiciosSave")) throw new PermisoException();
            string sql = "update TipoServicios set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoServicios).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoServicios, null));
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
            sql += " where Id = " + tipoServicios.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoServicios;
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
