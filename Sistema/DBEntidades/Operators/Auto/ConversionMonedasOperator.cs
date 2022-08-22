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
    public partial class ConversionMonedasOperator
    {

        public static ConversionMonedas GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoConversionMonedasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ConversionMonedas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ConversionMonedas where Id = " + Id.ToString()).Tables[0];
            ConversionMonedas conversionMonedas = new ConversionMonedas();
            foreach (PropertyInfo prop in typeof(ConversionMonedas).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(conversionMonedas, value, null); }
                catch (System.ArgumentException) { }
            }
            return conversionMonedas;
        }

        public static List<ConversionMonedas> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoConversionMonedasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ConversionMonedas).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ConversionMonedas> lista = new List<ConversionMonedas>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ConversionMonedas").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ConversionMonedas conversionMonedas = new ConversionMonedas();
                foreach (PropertyInfo prop in typeof(ConversionMonedas).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(conversionMonedas, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(conversionMonedas);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Conversion { get; set; } = 50;


        }

        public static ConversionMonedas Save(ConversionMonedas conversionMonedas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoConversionMonedasSave")) throw new PermisoException();
            if (conversionMonedas.Id == -1) return Insert(conversionMonedas);
            else return Update(conversionMonedas);
        }

        public static ConversionMonedas Insert(ConversionMonedas conversionMonedas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoConversionMonedasSave")) throw new PermisoException();
            string sql = "insert into ConversionMonedas(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ConversionMonedas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(conversionMonedas, null));
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
            conversionMonedas.Id = Convert.ToInt32(resp);
            return conversionMonedas;
        }

        public static ConversionMonedas Update(ConversionMonedas conversionMonedas)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoConversionMonedasSave")) throw new PermisoException();
            string sql = "update ConversionMonedas set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ConversionMonedas).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(conversionMonedas, null));
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
            sql += " where Id = " + conversionMonedas.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return conversionMonedas;
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
