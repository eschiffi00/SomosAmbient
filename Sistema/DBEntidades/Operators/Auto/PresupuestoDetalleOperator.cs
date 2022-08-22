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
    public partial class PresupuestoDetalleOperator
    {

        public static PresupuestoDetalle GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestoDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PresupuestoDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from PresupuestoDetalle where Id = " + Id.ToString()).Tables[0];
            PresupuestoDetalle presupuestoDetalle = new PresupuestoDetalle();
            foreach (PropertyInfo prop in typeof(PresupuestoDetalle).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(presupuestoDetalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return presupuestoDetalle;
        }

        public static List<PresupuestoDetalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestoDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(PresupuestoDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<PresupuestoDetalle> lista = new List<PresupuestoDetalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from PresupuestoDetalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                PresupuestoDetalle presupuestoDetalle = new PresupuestoDetalle();
                foreach (PropertyInfo prop in typeof(PresupuestoDetalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(presupuestoDetalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(presupuestoDetalle);
            }
            return lista;
        }

		public static List<PresupuestoDetalle> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<PresupuestoDetalle> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<PresupuestoDetalle> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<PresupuestoDetalle> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int CodigoItem { get; set; } = 100;
			public static int PrecioSeleccionado { get; set; } = 50;
			public static int Comentario { get; set; } = 2000;
			public static int ComentarioProveedor { get; set; } = 2000;


        }

        public static PresupuestoDetalle Save(PresupuestoDetalle presupuestoDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestoDetalleSave")) throw new PermisoException();
            if (presupuestoDetalle.Id == -1) return Insert(presupuestoDetalle);
            else return Update(presupuestoDetalle);
        }

        public static PresupuestoDetalle Insert(PresupuestoDetalle presupuestoDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestoDetalleSave")) throw new PermisoException();
            string sql = "insert into PresupuestoDetalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PresupuestoDetalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(presupuestoDetalle, null));
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
            presupuestoDetalle.Id = Convert.ToInt32(resp);
            return presupuestoDetalle;
        }

        public static PresupuestoDetalle Update(PresupuestoDetalle presupuestoDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPresupuestoDetalleSave")) throw new PermisoException();
            string sql = "update PresupuestoDetalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(PresupuestoDetalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(presupuestoDetalle, null));
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
            sql += " where Id = " + presupuestoDetalle.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return presupuestoDetalle;
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
