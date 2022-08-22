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
    public partial class EventosOperator
    {

        public static Eventos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEventosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Eventos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Eventos where Id = " + Id.ToString()).Tables[0];
            Eventos eventos = new Eventos();
            foreach (PropertyInfo prop in typeof(Eventos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(eventos, value, null); }
                catch (System.ArgumentException) { }
            }
            return eventos;
        }

        public static List<Eventos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEventosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Eventos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Eventos> lista = new List<Eventos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Eventos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Eventos eventos = new Eventos();
                foreach (PropertyInfo prop in typeof(Eventos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(eventos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(eventos);
            }
            return lista;
        }

		public static List<Eventos> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Eventos> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Eventos> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Eventos> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int ApellidoNombreCliente { get; set; } = 300;
			public static int RazonSocial { get; set; } = 300;
			public static int Mail { get; set; } = 300;
			public static int Tel { get; set; } = 100;
			public static int Comentario { get; set; } = 2000;
			public static int ComprobanteAprovacionExtension { get; set; } = 10;
			public static int NroComprobanteTransSenia { get; set; } = 100;
			public static int ComprobanteTransferenciaExtension { get; set; } = 10;
			public static int TipoIndexacion { get; set; } = 50;


        }

        public static Eventos Save(Eventos eventos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEventosSave")) throw new PermisoException();
            if (eventos.Id == -1) return Insert(eventos);
            else return Update(eventos);
        }

        public static Eventos Insert(Eventos eventos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEventosSave")) throw new PermisoException();
            string sql = "insert into Eventos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Eventos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(eventos, null));
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
            eventos.Id = Convert.ToInt32(resp);
            return eventos;
        }

        public static Eventos Update(Eventos eventos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEventosSave")) throw new PermisoException();
            string sql = "update Eventos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Eventos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(eventos, null));
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
            sql += " where Id = " + eventos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return eventos;
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
