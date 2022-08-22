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
    public partial class CostoTecnicaOperator
    {

        public static CostoTecnica GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoTecnicaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoTecnica).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoTecnica where Id = " + Id.ToString()).Tables[0];
            CostoTecnica costoTecnica = new CostoTecnica();
            foreach (PropertyInfo prop in typeof(CostoTecnica).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(costoTecnica, value, null); }
                catch (System.ArgumentException) { }
            }
            return costoTecnica;
        }

        public static List<CostoTecnica> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoTecnicaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoTecnica).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CostoTecnica> lista = new List<CostoTecnica>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoTecnica").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CostoTecnica costoTecnica = new CostoTecnica();
                foreach (PropertyInfo prop in typeof(CostoTecnica).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(costoTecnica, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(costoTecnica);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Dia { get; set; } = 50;


        }

        public static CostoTecnica Save(CostoTecnica costoTecnica)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoTecnicaSave")) throw new PermisoException();
            if (costoTecnica.Id == -1) return Insert(costoTecnica);
            else return Update(costoTecnica);
        }

        public static CostoTecnica Insert(CostoTecnica costoTecnica)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoTecnicaSave")) throw new PermisoException();
            string sql = "insert into CostoTecnica(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoTecnica).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoTecnica, null));
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
            costoTecnica.Id = Convert.ToInt32(resp);
            return costoTecnica;
        }

        public static CostoTecnica Update(CostoTecnica costoTecnica)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoTecnicaSave")) throw new PermisoException();
            string sql = "update CostoTecnica set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoTecnica).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoTecnica, null));
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
            sql += " where Id = " + costoTecnica.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return costoTecnica;
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
