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
    public partial class PresupuestosOperator
    {

        public static Presupuestos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Presupuestos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Presupuestos where Id = " + Id.ToString()).Tables[0];
            Presupuestos presupuestos = new Presupuestos();
            foreach (PropertyInfo prop in typeof(Presupuestos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(presupuestos, value, null); }
                catch (System.ArgumentException) { }
            }
            return presupuestos;
        }

        public static List<Presupuestos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Presupuestos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Presupuestos> lista = new List<Presupuestos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Presupuestos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Presupuestos presupuestos = new Presupuestos();
                foreach (PropertyInfo prop in typeof(Presupuestos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(presupuestos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(presupuestos);
            }
            return lista;
        }

		public static List<Presupuestos> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Presupuestos> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Presupuestos> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Presupuestos> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Comentario { get; set; } = 1000;
			public static int TipoEventoOtro { get; set; } = 100;
			public static int LocacionOtra { get; set; } = 100;
			public static int DireccionOtra { get; set; } = 200;
			public static int HorarioEvento { get; set; } = 50;
			public static int HoraFinalizado { get; set; } = 50;
			public static int HorarioArmado { get; set; } = 50;


        }

        public static Presupuestos Save(Presupuestos presupuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestosSave")) throw new PermisoException();
            if (presupuestos.Id == -1) return Insert(presupuestos);
            else return Update(presupuestos);
        }

        public static Presupuestos Insert(Presupuestos presupuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestosSave")) throw new PermisoException();
            string sql = "insert into Presupuestos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Presupuestos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(presupuestos, null));
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
            presupuestos.Id = Convert.ToInt32(resp);
            return presupuestos;
        }

        public static Presupuestos Update(Presupuestos presupuestos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestosSave")) throw new PermisoException();
            string sql = "update Presupuestos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Presupuestos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(presupuestos, null));
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
            sql += " where Id = " + presupuestos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return presupuestos;
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
