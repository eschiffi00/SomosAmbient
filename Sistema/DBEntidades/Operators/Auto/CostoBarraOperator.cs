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
    public partial class CostoBarraOperator
    {

        public static CostoBarra GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoBarraBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoBarra).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoBarra where Id = " + Id.ToString()).Tables[0];
            CostoBarra costoBarra = new CostoBarra();
            foreach (PropertyInfo prop in typeof(CostoBarra).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(costoBarra, value, null); }
                catch (System.ArgumentException) { }
            }
            return costoBarra;
        }

        public static List<CostoBarra> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoBarraBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoBarra).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CostoBarra> lista = new List<CostoBarra>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoBarra").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CostoBarra costoBarra = new CostoBarra();
                foreach (PropertyInfo prop in typeof(CostoBarra).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(costoBarra, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(costoBarra);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static CostoBarra Save(CostoBarra costoBarra)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoBarraSave")) throw new PermisoException();
            if (costoBarra.Id == -1) return Insert(costoBarra);
            else return Update(costoBarra);
        }

        public static CostoBarra Insert(CostoBarra costoBarra)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoBarraSave")) throw new PermisoException();
            string sql = "insert into CostoBarra(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoBarra).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoBarra, null));
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
            costoBarra.Id = Convert.ToInt32(resp);
            return costoBarra;
        }

        public static CostoBarra Update(CostoBarra costoBarra)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoBarraSave")) throw new PermisoException();
            string sql = "update CostoBarra set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoBarra).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoBarra, null));
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
            sql += " where Id = " + costoBarra.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return costoBarra;
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
