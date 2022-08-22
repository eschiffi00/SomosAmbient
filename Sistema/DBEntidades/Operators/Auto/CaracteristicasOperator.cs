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
    public partial class CaracteristicasOperator
    {

        public static Caracteristicas GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCaracteristicasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Caracteristicas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Caracteristicas where Id = " + Id.ToString()).Tables[0];
            Caracteristicas caracteristicas = new Caracteristicas();
            foreach (PropertyInfo prop in typeof(Caracteristicas).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(caracteristicas, value, null); }
                catch (System.ArgumentException) { }
            }
            return caracteristicas;
        }

        public static List<Caracteristicas> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCaracteristicasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Caracteristicas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Caracteristicas> lista = new List<Caracteristicas>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Caracteristicas").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Caracteristicas caracteristicas = new Caracteristicas();
                foreach (PropertyInfo prop in typeof(Caracteristicas).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(caracteristicas, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(caracteristicas);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static Caracteristicas Save(Caracteristicas caracteristicas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCaracteristicasSave")) throw new PermisoException();
            if (caracteristicas.Id == -1) return Insert(caracteristicas);
            else return Update(caracteristicas);
        }

        public static Caracteristicas Insert(Caracteristicas caracteristicas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCaracteristicasSave")) throw new PermisoException();
            string sql = "insert into Caracteristicas(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Caracteristicas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(caracteristicas, null));
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
            caracteristicas.Id = Convert.ToInt32(resp);
            return caracteristicas;
        }

        public static Caracteristicas Update(Caracteristicas caracteristicas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCaracteristicasSave")) throw new PermisoException();
            string sql = "update Caracteristicas set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Caracteristicas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(caracteristicas, null));
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
            sql += " where Id = " + caracteristicas.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return caracteristicas;
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
