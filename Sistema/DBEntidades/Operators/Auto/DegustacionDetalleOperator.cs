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
    public partial class DegustacionDetalleOperator
    {

        public static DegustacionDetalle GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(DegustacionDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from DegustacionDetalle where Id = " + Id.ToString()).Tables[0];
            DegustacionDetalle degustacionDetalle = new DegustacionDetalle();
            foreach (PropertyInfo prop in typeof(DegustacionDetalle).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(degustacionDetalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return degustacionDetalle;
        }

        public static List<DegustacionDetalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(DegustacionDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<DegustacionDetalle> lista = new List<DegustacionDetalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from DegustacionDetalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                DegustacionDetalle degustacionDetalle = new DegustacionDetalle();
                foreach (PropertyInfo prop in typeof(DegustacionDetalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(degustacionDetalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(degustacionDetalle);
            }
            return lista;
        }

		public static List<DegustacionDetalle> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<DegustacionDetalle> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<DegustacionDetalle> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<DegustacionDetalle> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Empresa { get; set; } = 200;
			public static int Comensal { get; set; } = 500;
			public static int EstadoEvento { get; set; } = 50;
			public static int Comentarios { get; set; } = 2000;
			public static int Telefono { get; set; } = 100;
			public static int Mail { get; set; } = 200;


        }

        public static DegustacionDetalle Save(DegustacionDetalle degustacionDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionDetalleSave")) throw new PermisoException();
            if (degustacionDetalle.Id == -1) return Insert(degustacionDetalle);
            else return Update(degustacionDetalle);
        }

        public static DegustacionDetalle Insert(DegustacionDetalle degustacionDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionDetalleSave")) throw new PermisoException();
            string sql = "insert into DegustacionDetalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(DegustacionDetalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(degustacionDetalle, null));
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
            degustacionDetalle.Id = Convert.ToInt32(resp);
            return degustacionDetalle;
        }

        public static DegustacionDetalle Update(DegustacionDetalle degustacionDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionDetalleSave")) throw new PermisoException();
            string sql = "update DegustacionDetalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(DegustacionDetalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(degustacionDetalle, null));
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
            sql += " where Id = " + degustacionDetalle.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return degustacionDetalle;
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
