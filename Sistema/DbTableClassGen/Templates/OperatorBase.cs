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
    public partial class <TableName>Operator
    {

        public static <TableName> GetOneByIdentity(int <identity>)
        {
            if (!DbEntidades.Seguridad.Permiso("Permiso<TableName>Browse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(<TableName>).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from <TableName> where <identity> = " + <identity>.ToString()).Tables[0];
            <TableName> <varName> = new <TableName>();
            foreach (PropertyInfo prop in typeof(<TableName>).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(<varName>, value, null); }
                catch (System.ArgumentException) { }
            }
            return <varName>;
        }

        public static List<<TableName>> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("Permiso<TableName>Browse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(<TableName>).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<<TableName>> lista = new List<<TableName>>();
            DataTable dt = db.GetDataSet("select " + columnas + " from <TableName>").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                <TableName> <varName> = new <TableName>();
                foreach (PropertyInfo prop in typeof(<TableName>).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(<varName>, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(<varName>);
            }
            return lista;
        }

<GetAllEstado1>

        public class MaxLength
        {
<MaxLength>

        }

        public static <TableName> Save(<TableName> <varName>)
        {
            if (!DbEntidades.Seguridad.Permiso("Permiso<TableName>Save")) throw new PermisoException();
            if (<varName>.<identity> == -1) return Insert(<varName>);
            else return Update(<varName>);
        }

        public static <TableName> Insert(<TableName> <varName>)
        {
            if (!DbEntidades.Seguridad.Permiso("Permiso<TableName>Save")) throw new PermisoException();
            string sql = "insert into <TableName>(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(<TableName>).GetProperties())
            {
                if (prop.Name == "<identity>") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(<varName>, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.<identity> values (" + valores + ")";
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
            <varName>.<identity> = Convert.ToInt32(resp);
            return <varName>;
        }

        public static <TableName> Update(<TableName> <varName>)
        {
            if (!DbEntidades.Seguridad.Permiso("Permiso<TableName>Save")) throw new PermisoException();
            string sql = "update <TableName> set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(<TableName>).GetProperties())
            {
                if (prop.Name == "<identity>") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(<varName>, null));
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
            sql += " where <identity> = " + <varName>.<identity>;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return <varName>;
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
