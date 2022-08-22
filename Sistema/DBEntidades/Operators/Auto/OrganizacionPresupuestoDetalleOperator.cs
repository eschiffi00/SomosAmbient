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
    public partial class OrganizacionPresupuestoDetalleOperator
    {

        public static OrganizacionPresupuestoDetalle GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestoDetalle where Id = " + Id.ToString()).Tables[0];
            OrganizacionPresupuestoDetalle organizacionPresupuestoDetalle = new OrganizacionPresupuestoDetalle();
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoDetalle).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(organizacionPresupuestoDetalle, value, null); }
                catch (System.ArgumentException) { }
            }
            return organizacionPresupuestoDetalle;
        }

        public static List<OrganizacionPresupuestoDetalle> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoDetalleBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoDetalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<OrganizacionPresupuestoDetalle> lista = new List<OrganizacionPresupuestoDetalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from OrganizacionPresupuestoDetalle").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                OrganizacionPresupuestoDetalle organizacionPresupuestoDetalle = new OrganizacionPresupuestoDetalle();
                foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoDetalle).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(organizacionPresupuestoDetalle, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(organizacionPresupuestoDetalle);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int MotivoFestejo { get; set; } = 200;
			public static int Mail { get; set; } = 100;
			public static int Tel { get; set; } = 100;
			public static int LocacionOtra { get; set; } = 200;
			public static int EnvioMailPresentacion { get; set; } = 2;
			public static int RealizoReunionConCliente { get; set; } = 2;
			public static int Direccion { get; set; } = 200;
			public static int Bocados { get; set; } = 1200;
			public static int Islas { get; set; } = 600;
			public static int Entrada { get; set; } = 600;
			public static int PrincipalAdultos { get; set; } = 600;
			public static int PrincipalAdolescentes { get; set; } = 600;
			public static int PostreAdultosAdolescentes { get; set; } = 600;
			public static int PrincipalChicos { get; set; } = 600;
			public static int PostreChicos { get; set; } = 600;
			public static int MesaDulce { get; set; } = 600;
			public static int FinFiesta { get; set; } = 600;
			public static int MesaPrincipal { get; set; } = 200;
			public static int Manteleria { get; set; } = 200;
			public static int Servilletas { get; set; } = 200;
			public static int Sillas { get; set; } = 200;
			public static int InvitadosDespues00 { get; set; } = 200;
			public static int CumpleaniosEnEvento { get; set; } = 200;
			public static int TortaAlegorica { get; set; } = 600;
			public static int LleganAlSalon { get; set; } = 200;
			public static int PlatosEspeciales { get; set; } = 2000;
			public static int ServiciodeVinoChampagne { get; set; } = 200;
			public static int ObservacionBarras { get; set; } = 2000;
			public static int ObservacionCatering { get; set; } = 2000;
			public static int ObservacionTecnica { get; set; } = 2000;
			public static int ObservacionAmbientacion { get; set; } = 2000;
			public static int ObservacionParticulares { get; set; } = 2000;
			public static int ObservacionesAdicionales { get; set; } = 2000;
			public static int Acreditaciones { get; set; } = 200;
			public static int ListaInvitados { get; set; } = 200;
			public static int ListaCocheras { get; set; } = 200;
			public static int Layout { get; set; } = 2000;
			public static int AlfombraRoja { get; set; } = 200;
			public static int Anexo7 { get; set; } = 200;
			public static int Ramo { get; set; } = 10;
			public static int Escenario { get; set; } = 10;
			public static int IngresoProveedoresLugar { get; set; } = 200;
			public static int ContactoResponsableLugar { get; set; } = 200;
			public static int TelefonoResponsableLugar { get; set; } = 200;
			public static int FechaArmadoLogistica { get; set; } = 200;
			public static int FechaArmadoSalon { get; set; } = 200;
			public static int FechaDesarmadoSalon { get; set; } = 200;
			public static int HoraArmadoLogistica { get; set; } = 200;
			public static int HoraDesarmadoSalon { get; set; } = 200;
			public static int HoraArmadoSalon { get; set; } = 200;
			public static int CantPersonasAfectadasArmado { get; set; } = 200;
			public static int ObservacionesHielo { get; set; } = 2000;
			public static int ObservacionesMoviliario { get; set; } = 2000;
			public static int ObservacionesLogistica { get; set; } = 2000;
			public static int ObservacionesManteleria { get; set; } = 2000;


        }

        public static OrganizacionPresupuestoDetalle Save(OrganizacionPresupuestoDetalle organizacionPresupuestoDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoDetalleSave")) throw new PermisoException();
            if (organizacionPresupuestoDetalle.Id == -1) return Insert(organizacionPresupuestoDetalle);
            else return Update(organizacionPresupuestoDetalle);
        }

        public static OrganizacionPresupuestoDetalle Insert(OrganizacionPresupuestoDetalle organizacionPresupuestoDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoDetalleSave")) throw new PermisoException();
            string sql = "insert into OrganizacionPresupuestoDetalle(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoDetalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestoDetalle, null));
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
            organizacionPresupuestoDetalle.Id = Convert.ToInt32(resp);
            return organizacionPresupuestoDetalle;
        }

        public static OrganizacionPresupuestoDetalle Update(OrganizacionPresupuestoDetalle organizacionPresupuestoDetalle)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoOrganizacionPresupuestoDetalleSave")) throw new PermisoException();
            string sql = "update OrganizacionPresupuestoDetalle set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(OrganizacionPresupuestoDetalle).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(organizacionPresupuestoDetalle, null));
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
            sql += " where Id = " + organizacionPresupuestoDetalle.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return organizacionPresupuestoDetalle;
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
