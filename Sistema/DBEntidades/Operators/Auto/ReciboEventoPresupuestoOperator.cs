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
    public partial class ReciboEventoPresupuestoOperator
    {

        public static ReciboEventoPresupuesto GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoReciboEventoPresupuestoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ReciboEventoPresupuesto).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ReciboEventoPresupuesto where Id = " + Id.ToString()).Tables[0];
            ReciboEventoPresupuesto reciboEventoPresupuesto = new ReciboEventoPresupuesto();
            foreach (PropertyInfo prop in typeof(ReciboEventoPresupuesto).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(reciboEventoPresupuesto, value, null); }
                catch (System.ArgumentException) { }
            }
            return reciboEventoPresupuesto;
        }

        public static List<ReciboEventoPresupuesto> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoReciboEventoPresupuestoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ReciboEventoPresupuesto).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ReciboEventoPresupuesto> lista = new List<ReciboEventoPresupuesto>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ReciboEventoPresupuesto").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ReciboEventoPresupuesto reciboEventoPresupuesto = new ReciboEventoPresupuesto();
                foreach (PropertyInfo prop in typeof(ReciboEventoPresupuesto).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(reciboEventoPresupuesto, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(reciboEventoPresupuesto);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static ReciboEventoPresupuesto Save(ReciboEventoPresupuesto reciboEventoPresupuesto)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoReciboEventoPresupuestoSave")) throw new PermisoException();
            if (reciboEventoPresupuesto.Id == -1) return Insert(reciboEventoPresupuesto);
            else return Update(reciboEventoPresupuesto);
        }

        public static ReciboEventoPresupuesto Insert(ReciboEventoPresupuesto reciboEventoPresupuesto)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoReciboEventoPresupuestoSave")) throw new PermisoException();
            string sql = "insert into ReciboEventoPresupuesto(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ReciboEventoPresupuesto).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(reciboEventoPresupuesto, null));
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
            reciboEventoPresupuesto.Id = Convert.ToInt32(resp);
            return reciboEventoPresupuesto;
        }

        public static ReciboEventoPresupuesto Update(ReciboEventoPresupuesto reciboEventoPresupuesto)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoReciboEventoPresupuestoSave")) throw new PermisoException();
            string sql = "update ReciboEventoPresupuesto set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ReciboEventoPresupuesto).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(reciboEventoPresupuesto, null));
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
            sql += " where Id = " + reciboEventoPresupuesto.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return reciboEventoPresupuesto;
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
