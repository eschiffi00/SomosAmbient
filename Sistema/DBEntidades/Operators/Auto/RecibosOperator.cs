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
    public partial class RecibosOperator
    {

        public static Recibos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecibosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Recibos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Recibos where Id = " + Id.ToString()).Tables[0];
            Recibos recibos = new Recibos();
            foreach (PropertyInfo prop in typeof(Recibos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(recibos, value, null); }
                catch (System.ArgumentException) { }
            }
            return recibos;
        }

        public static List<Recibos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecibosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Recibos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Recibos> lista = new List<Recibos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Recibos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Recibos recibos = new Recibos();
                foreach (PropertyInfo prop in typeof(Recibos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(recibos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(recibos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int NroRecibo { get; set; } = 50;
			public static int Concepto { get; set; } = 500;


        }

        public static Recibos Save(Recibos recibos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecibosSave")) throw new PermisoException();
            if (recibos.Id == -1) return Insert(recibos);
            else return Update(recibos);
        }

        public static Recibos Insert(Recibos recibos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecibosSave")) throw new PermisoException();
            string sql = "insert into Recibos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Recibos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(recibos, null));
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
            recibos.Id = Convert.ToInt32(resp);
            return recibos;
        }

        public static Recibos Update(Recibos recibos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecibosSave")) throw new PermisoException();
            string sql = "update Recibos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Recibos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(recibos, null));
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
            sql += " where Id = " + recibos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return recibos;
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
