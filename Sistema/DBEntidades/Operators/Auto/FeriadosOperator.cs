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
    public partial class FeriadosOperator
    {

        public static Feriados GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFeriadosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Feriados).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Feriados where Id = " + Id.ToString()).Tables[0];
            Feriados feriados = new Feriados();
            foreach (PropertyInfo prop in typeof(Feriados).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(feriados, value, null); }
                catch (System.ArgumentException) { }
            }
            return feriados;
        }

        public static List<Feriados> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFeriadosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Feriados).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Feriados> lista = new List<Feriados>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Feriados").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Feriados feriados = new Feriados();
                foreach (PropertyInfo prop in typeof(Feriados).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(feriados, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(feriados);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 500;


        }

        public static Feriados Save(Feriados feriados)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFeriadosSave")) throw new PermisoException();
            if (feriados.Id == -1) return Insert(feriados);
            else return Update(feriados);
        }

        public static Feriados Insert(Feriados feriados)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFeriadosSave")) throw new PermisoException();
            string sql = "insert into Feriados(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Feriados).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(feriados, null));
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
            feriados.Id = Convert.ToInt32(resp);
            return feriados;
        }

        public static Feriados Update(Feriados feriados)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFeriadosSave")) throw new PermisoException();
            string sql = "update Feriados set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Feriados).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(feriados, null));
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
            sql += " where Id = " + feriados.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return feriados;
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
