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
    public partial class DuracionEventoOperator
    {

        public static DuracionEvento GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDuracionEventoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(DuracionEvento).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from DuracionEvento where Id = " + Id.ToString()).Tables[0];
            DuracionEvento duracionEvento = new DuracionEvento();
            foreach (PropertyInfo prop in typeof(DuracionEvento).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(duracionEvento, value, null); }
                catch (System.ArgumentException) { }
            }
            return duracionEvento;
        }

        public static List<DuracionEvento> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDuracionEventoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(DuracionEvento).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<DuracionEvento> lista = new List<DuracionEvento>();
            DataTable dt = db.GetDataSet("select " + columnas + " from DuracionEvento").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                DuracionEvento duracionEvento = new DuracionEvento();
                foreach (PropertyInfo prop in typeof(DuracionEvento).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(duracionEvento, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(duracionEvento);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 50;


        }

        public static DuracionEvento Save(DuracionEvento duracionEvento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDuracionEventoSave")) throw new PermisoException();
            if (duracionEvento.Id == -1) return Insert(duracionEvento);
            else return Update(duracionEvento);
        }

        public static DuracionEvento Insert(DuracionEvento duracionEvento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDuracionEventoSave")) throw new PermisoException();
            string sql = "insert into DuracionEvento(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(DuracionEvento).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(duracionEvento, null));
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
            duracionEvento.Id = Convert.ToInt32(resp);
            return duracionEvento;
        }

        public static DuracionEvento Update(DuracionEvento duracionEvento)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDuracionEventoSave")) throw new PermisoException();
            string sql = "update DuracionEvento set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(DuracionEvento).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(duracionEvento, null));
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
            sql += " where Id = " + duracionEvento.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return duracionEvento;
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
