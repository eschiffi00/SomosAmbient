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
    public partial class TransferenciasOperator
    {

        public static Transferencias GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTransferenciasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Transferencias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Transferencias where Id = " + Id.ToString()).Tables[0];
            Transferencias transferencias = new Transferencias();
            foreach (PropertyInfo prop in typeof(Transferencias).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(transferencias, value, null); }
                catch (System.ArgumentException) { }
            }
            return transferencias;
        }

        public static List<Transferencias> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTransferenciasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Transferencias).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Transferencias> lista = new List<Transferencias>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Transferencias").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Transferencias transferencias = new Transferencias();
                foreach (PropertyInfo prop in typeof(Transferencias).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(transferencias, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(transferencias);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int NroTransferencia { get; set; } = 200;
			public static int NombreArchivo { get; set; } = 500;
			public static int ComprobanteExtension { get; set; } = 50;


        }

        public static Transferencias Save(Transferencias transferencias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTransferenciasSave")) throw new PermisoException();
            if (transferencias.Id == -1) return Insert(transferencias);
            else return Update(transferencias);
        }

        public static Transferencias Insert(Transferencias transferencias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTransferenciasSave")) throw new PermisoException();
            string sql = "insert into Transferencias(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Transferencias).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(transferencias, null));
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
            transferencias.Id = Convert.ToInt32(resp);
            return transferencias;
        }

        public static Transferencias Update(Transferencias transferencias)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTransferenciasSave")) throw new PermisoException();
            string sql = "update Transferencias set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Transferencias).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(transferencias, null));
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
            sql += " where Id = " + transferencias.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return transferencias;
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
