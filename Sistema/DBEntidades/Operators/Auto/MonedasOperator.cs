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
    public partial class MonedasOperator
    {

        public static Monedas GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMonedasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Monedas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Monedas where Id = " + Id.ToString()).Tables[0];
            Monedas monedas = new Monedas();
            foreach (PropertyInfo prop in typeof(Monedas).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(monedas, value, null); }
                catch (System.ArgumentException) { }
            }
            return monedas;
        }

        public static List<Monedas> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMonedasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Monedas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Monedas> lista = new List<Monedas>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Monedas").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Monedas monedas = new Monedas();
                foreach (PropertyInfo prop in typeof(Monedas).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(monedas, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(monedas);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 100;
			public static int DescripcionCorta { get; set; } = 50;


        }

        public static Monedas Save(Monedas monedas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMonedasSave")) throw new PermisoException();
            if (monedas.Id == -1) return Insert(monedas);
            else return Update(monedas);
        }

        public static Monedas Insert(Monedas monedas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMonedasSave")) throw new PermisoException();
            string sql = "insert into Monedas(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Monedas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(monedas, null));
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
            monedas.Id = Convert.ToInt32(resp);
            return monedas;
        }

        public static Monedas Update(Monedas monedas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoMonedasSave")) throw new PermisoException();
            string sql = "update Monedas set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Monedas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(monedas, null));
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
            sql += " where Id = " + monedas.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return monedas;
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
