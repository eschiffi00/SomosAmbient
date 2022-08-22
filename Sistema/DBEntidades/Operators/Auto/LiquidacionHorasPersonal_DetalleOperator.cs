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
    public partial class LiquidacionHorasPersonal_DetalleOperator
    {

        public static LiquidacionHorasPersonal_Detalle GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLiquidacionHorasPersonal_DetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(LiquidacionHorasPersonal_Detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from LiquidacionHorasPersonal_Detalle where Id = " + Id.ToString()).Tables[0];
            LiquidacionHorasPersonal_Detalle liquidacionHorasPersonal_Detalle = new LiquidacionHorasPersonal_Detalle();
            foreach (PropertyInfo prop in typeof(LiquidacionHorasPersonal_Detalle).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(liquidacionHorasPersonal_Detalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return liquidacionHorasPersonal_Detalle;
        }

        public static List<LiquidacionHorasPersonal_Detalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLiquidacionHorasPersonal_DetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(LiquidacionHorasPersonal_Detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<LiquidacionHorasPersonal_Detalle> lista = new List<LiquidacionHorasPersonal_Detalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from LiquidacionHorasPersonal_Detalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                LiquidacionHorasPersonal_Detalle liquidacionHorasPersonal_Detalle = new LiquidacionHorasPersonal_Detalle();
                foreach (PropertyInfo prop in typeof(LiquidacionHorasPersonal_Detalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(liquidacionHorasPersonal_Detalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(liquidacionHorasPersonal_Detalle);
            }
            return lista;
        }

		public static List<LiquidacionHorasPersonal_Detalle> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<LiquidacionHorasPersonal_Detalle> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<LiquidacionHorasPersonal_Detalle> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<LiquidacionHorasPersonal_Detalle> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int HoraEntrada { get; set; } = 5;
			public static int HoraSalida { get; set; } = 5;


        }

        public static LiquidacionHorasPersonal_Detalle Save(LiquidacionHorasPersonal_Detalle liquidacionHorasPersonal_Detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLiquidacionHorasPersonal_DetalleSave")) throw new PermisoException();
            if (liquidacionHorasPersonal_Detalle.Id == -1) return Insert(liquidacionHorasPersonal_Detalle);
            else return Update(liquidacionHorasPersonal_Detalle);
        }

        public static LiquidacionHorasPersonal_Detalle Insert(LiquidacionHorasPersonal_Detalle liquidacionHorasPersonal_Detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLiquidacionHorasPersonal_DetalleSave")) throw new PermisoException();
            string sql = "insert into LiquidacionHorasPersonal_Detalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(LiquidacionHorasPersonal_Detalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(liquidacionHorasPersonal_Detalle, null));
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
            liquidacionHorasPersonal_Detalle.Id = Convert.ToInt32(resp);
            return liquidacionHorasPersonal_Detalle;
        }

        public static LiquidacionHorasPersonal_Detalle Update(LiquidacionHorasPersonal_Detalle liquidacionHorasPersonal_Detalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoLiquidacionHorasPersonal_DetalleSave")) throw new PermisoException();
            string sql = "update LiquidacionHorasPersonal_Detalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(LiquidacionHorasPersonal_Detalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(liquidacionHorasPersonal_Detalle, null));
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
            sql += " where Id = " + liquidacionHorasPersonal_Detalle.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return liquidacionHorasPersonal_Detalle;
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
