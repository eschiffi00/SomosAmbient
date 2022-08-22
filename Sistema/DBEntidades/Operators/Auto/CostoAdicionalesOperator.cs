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
    public partial class CostoAdicionalesOperator
    {

        public static CostoAdicionales GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAdicionalesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoAdicionales).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoAdicionales where Id = " + Id.ToString()).Tables[0];
            CostoAdicionales costoAdicionales = new CostoAdicionales();
            foreach (PropertyInfo prop in typeof(CostoAdicionales).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(costoAdicionales, value, null); }
                catch (System.ArgumentException) { }
            }
            return costoAdicionales;
        }

        public static List<CostoAdicionales> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAdicionalesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoAdicionales).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CostoAdicionales> lista = new List<CostoAdicionales>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoAdicionales").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CostoAdicionales costoAdicionales = new CostoAdicionales();
                foreach (PropertyInfo prop in typeof(CostoAdicionales).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(costoAdicionales, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(costoAdicionales);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static CostoAdicionales Save(CostoAdicionales costoAdicionales)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAdicionalesSave")) throw new PermisoException();
            if (costoAdicionales.Id == -1) return Insert(costoAdicionales);
            else return Update(costoAdicionales);
        }

        public static CostoAdicionales Insert(CostoAdicionales costoAdicionales)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAdicionalesSave")) throw new PermisoException();
            string sql = "insert into CostoAdicionales(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoAdicionales).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoAdicionales, null));
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
            costoAdicionales.Id = Convert.ToInt32(resp);
            return costoAdicionales;
        }

        public static CostoAdicionales Update(CostoAdicionales costoAdicionales)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoAdicionalesSave")) throw new PermisoException();
            string sql = "update CostoAdicionales set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoAdicionales).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoAdicionales, null));
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
            sql += " where Id = " + costoAdicionales.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return costoAdicionales;
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
