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
    public partial class EntidadesOperator
    {

        public static Entidades GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEntidadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Entidades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Entidades where Id = " + Id.ToString()).Tables[0];
            Entidades entidades = new Entidades();
            foreach (PropertyInfo prop in typeof(Entidades).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(entidades, value, null); }
                catch (System.ArgumentException) { }
            }
            return entidades;
        }

        public static List<Entidades> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEntidadesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Entidades).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Entidades> lista = new List<Entidades>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Entidades").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Entidades entidades = new Entidades();
                foreach (PropertyInfo prop in typeof(Entidades).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(entidades, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(entidades);
            }
            return lista;
        }

		public static List<Entidades> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Entidades> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Entidades> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Entidades> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int IsProveedor { get; set; } = 1;
			public static int IsCliente { get; set; } = 1;
			public static int IsContacto { get; set; } = 1;
			public static int RazonSocial { get; set; } = 50;
			public static int ApellidoNombre { get; set; } = 50;
			public static int NombreFantasia { get; set; } = 50;
			public static int CUITCUIL { get; set; } = 11;
			public static int CBU { get; set; } = 40;
			public static int NroIIBB { get; set; } = 30;
			public static int Email { get; set; } = 50;
			public static int Localidad { get; set; } = 50;
			public static int CP { get; set; } = 10;


        }

        public static Entidades Save(Entidades entidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEntidadesSave")) throw new PermisoException();
            if (entidades.Id == -1) return Insert(entidades);
            else return Update(entidades);
        }

        public static Entidades Insert(Entidades entidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEntidadesSave")) throw new PermisoException();
            string sql = "insert into Entidades(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Entidades).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(entidades, null));
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
            entidades.Id = Convert.ToInt32(resp);
            return entidades;
        }

        public static Entidades Update(Entidades entidades)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoEntidadesSave")) throw new PermisoException();
            string sql = "update Entidades set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Entidades).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(entidades, null));
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
            sql += " where Id = " + entidades.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return entidades;
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
