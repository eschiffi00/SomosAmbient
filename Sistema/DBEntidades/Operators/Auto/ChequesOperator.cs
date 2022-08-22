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
    public partial class ChequesOperator
    {

        public static Cheques GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cheques).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cheques where Id = " + Id.ToString()).Tables[0];
            Cheques cheques = new Cheques();
            foreach (PropertyInfo prop in typeof(Cheques).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(cheques, value, null); }
                catch (System.ArgumentException) { }
            }
            return cheques;
        }

        public static List<Cheques> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cheques).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Cheques> lista = new List<Cheques>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cheques").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Cheques cheques = new Cheques();
                foreach (PropertyInfo prop in typeof(Cheques).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(cheques, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(cheques);
            }
            return lista;
        }

		public static List<Cheques> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Cheques> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Cheques> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Cheques> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int NroCheque { get; set; } = 50;
			public static int EmitidoA { get; set; } = 300;
			public static int Observaciones { get; set; } = 2000;
			public static int TipoCheque { get; set; } = 50;


        }

        public static Cheques Save(Cheques cheques)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesSave")) throw new PermisoException();
            if (cheques.Id == -1) return Insert(cheques);
            else return Update(cheques);
        }

        public static Cheques Insert(Cheques cheques)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesSave")) throw new PermisoException();
            string sql = "insert into Cheques(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cheques).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cheques, null));
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
            cheques.Id = Convert.ToInt32(resp);
            return cheques;
        }

        public static Cheques Update(Cheques cheques)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoChequesSave")) throw new PermisoException();
            string sql = "update Cheques set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cheques).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cheques, null));
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
            sql += " where Id = " + cheques.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return cheques;
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
