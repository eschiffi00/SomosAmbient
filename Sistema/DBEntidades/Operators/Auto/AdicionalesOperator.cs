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
    public partial class AdicionalesOperator
    {

        public static Adicionales GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Adicionales).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Adicionales where Id = " + Id.ToString()).Tables[0];
            Adicionales adicionales = new Adicionales();
            foreach (PropertyInfo prop in typeof(Adicionales).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(adicionales, value, null); }
                catch (System.ArgumentException) { }
            }
            return adicionales;
        }

        public static List<Adicionales> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Adicionales).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Adicionales> lista = new List<Adicionales>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Adicionales").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Adicionales adicionales = new Adicionales();
                foreach (PropertyInfo prop in typeof(Adicionales).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(adicionales, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(adicionales);
            }
            return lista;
        }

		public static List<Adicionales> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Adicionales> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Adicionales> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Adicionales> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Descripcion { get; set; } = 300;
			public static int RequiereCantidad { get; set; } = 1;
			public static int RequiereCantidadRango { get; set; } = 1;
			public static int SoloMayores { get; set; } = 1;


        }

        public static Adicionales Save(Adicionales adicionales)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesSave")) throw new PermisoException();
            if (adicionales.Id == -1) return Insert(adicionales);
            else return Update(adicionales);
        }

        public static Adicionales Insert(Adicionales adicionales)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesSave")) throw new PermisoException();
            string sql = "insert into Adicionales(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Adicionales).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(adicionales, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted. values (" + valores + ")";
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
            adicionales.Id = Convert.ToInt32(resp);
            return adicionales;
        }

        public static Adicionales Update(Adicionales adicionales)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoAdicionalesSave")) throw new PermisoException();
            string sql = "update Adicionales set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Adicionales).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(adicionales, null));
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
            sql += " where  Id = " + adicionales.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return adicionales;
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
