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
    public partial class ProcesoCierreOperator
    {

        public static ProcesoCierre GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProcesoCierreBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ProcesoCierre).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ProcesoCierre where Id = " + Id.ToString()).Tables[0];
            ProcesoCierre procesoCierre = new ProcesoCierre();
            foreach (PropertyInfo prop in typeof(ProcesoCierre).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(procesoCierre, value, null); }
                catch (System.ArgumentException) { }
            }
            return procesoCierre;
        }

        public static List<ProcesoCierre> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProcesoCierreBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ProcesoCierre).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ProcesoCierre> lista = new List<ProcesoCierre>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ProcesoCierre").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ProcesoCierre procesoCierre = new ProcesoCierre();
                foreach (PropertyInfo prop in typeof(ProcesoCierre).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(procesoCierre, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(procesoCierre);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 200;


        }

        public static ProcesoCierre Save(ProcesoCierre procesoCierre)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProcesoCierreSave")) throw new PermisoException();
            if (procesoCierre.Id == -1) return Insert(procesoCierre);
            else return Update(procesoCierre);
        }

        public static ProcesoCierre Insert(ProcesoCierre procesoCierre)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProcesoCierreSave")) throw new PermisoException();
            string sql = "insert into ProcesoCierre(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ProcesoCierre).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(procesoCierre, null));
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
            procesoCierre.Id = Convert.ToInt32(resp);
            return procesoCierre;
        }

        public static ProcesoCierre Update(ProcesoCierre procesoCierre)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProcesoCierreSave")) throw new PermisoException();
            string sql = "update ProcesoCierre set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ProcesoCierre).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(procesoCierre, null));
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
            sql += " where Id = " + procesoCierre.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return procesoCierre;
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
