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
    public partial class FormOperator
    {

        public static Form GetOneByIdentity(int FormId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFormBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Form).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Form where FormId = " + FormId.ToString()).Tables[0];
            Form form = new Form();
            foreach (PropertyInfo prop in typeof(Form).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(form, value, null); }
                catch (System.ArgumentException) { }
            }
            return form;
        }

        public static List<Form> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFormBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Form).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Form> lista = new List<Form>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Form").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Form form = new Form();
                foreach (PropertyInfo prop in typeof(Form).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(form, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(form);
            }
            return lista;
        }

		public static List<Form> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Form> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Form> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Form> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Nombre { get; set; } = 255;


        }

        public static Form Save(Form form)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFormSave")) throw new PermisoException();
            if (form.FormId == -1) return Insert(form);
            else return Update(form);
        }

        public static Form Insert(Form form)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFormSave")) throw new PermisoException();
            string sql = "insert into Form(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Form).GetProperties())
            {
                if (prop.Name == "FormId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(form, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.FormId values (" + valores + ")";
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
            form.FormId = Convert.ToInt32(resp);
            return form;
        }

        public static Form Update(Form form)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoFormSave")) throw new PermisoException();
            string sql = "update Form set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Form).GetProperties())
            {
                if (prop.Name == "FormId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(form, null));
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
            sql += " where FormId = " + form.FormId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return form;
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
