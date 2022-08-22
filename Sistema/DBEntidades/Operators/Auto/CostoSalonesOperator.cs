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
    public partial class CostoSalonesOperator
    {

        public static CostoSalones GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoSalonesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoSalones).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoSalones where Id = " + Id.ToString()).Tables[0];
            CostoSalones costoSalones = new CostoSalones();
            foreach (PropertyInfo prop in typeof(CostoSalones).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(costoSalones, value, null); }
                catch (System.ArgumentException) { }
            }
            return costoSalones;
        }

        public static List<CostoSalones> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoSalonesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoSalones).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CostoSalones> lista = new List<CostoSalones>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoSalones").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CostoSalones costoSalones = new CostoSalones();
                foreach (PropertyInfo prop in typeof(CostoSalones).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(costoSalones, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(costoSalones);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Dia { get; set; } = 10;


        }

        public static CostoSalones Save(CostoSalones costoSalones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoSalonesSave")) throw new PermisoException();
            if (costoSalones.Id == -1) return Insert(costoSalones);
            else return Update(costoSalones);
        }

        public static CostoSalones Insert(CostoSalones costoSalones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoSalonesSave")) throw new PermisoException();
            string sql = "insert into CostoSalones(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoSalones).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoSalones, null));
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
            costoSalones.Id = Convert.ToInt32(resp);
            return costoSalones;
        }

        public static CostoSalones Update(CostoSalones costoSalones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoSalonesSave")) throw new PermisoException();
            string sql = "update CostoSalones set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoSalones).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoSalones, null));
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
            sql += " where Id = " + costoSalones.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return costoSalones;
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
