using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using DbEntidades.Entities;
using System.Data.SqlClient;
using LibDB2;

namespace DbEntidades.Operators
{
    public partial class PermisoOperator
    {
        public static Boolean TienePermiso(int usuarioId, string formName)
        {
            return true;
            if (usuarioId == 1) return true;
            string query = @"
            select 1 from Usuario u where u.UsuarioId = @UsuarioId and u.EsAdmin = 1
            union
            select 1 
            from Form f
            inner join Permiso p on p.FormId = f.FormId and p.EstadoId = 1
            inner join UsuarioRol ur on ur.RolId = p.RolId and ur.EstadoId = 1
            inner join Usuario u on u.UsuarioId = ur.UsuarioId and u.EstadoId = 1
            where f.Nombre = @FormName
              and f.EstadoId = 1
              and u.UsuarioId = @UsuarioId
            ";
            DB db = new DB();
            object o = db.ExecuteScalar(query, new SqlParameter("@UsuarioId", usuarioId), new SqlParameter("@FormName", formName));
            if (o != null && Convert.ToInt32(o) == 1) return true;
            else return false;
        }
        
        public static List<string> ListaPermisos(int usuarioId)
        {
            string query = @"
            select f.Nombre 
            from Form f
            inner join Permiso p on p.FormId = f.FormId and p.EstadoId = 1
            inner join UsuarioRol ur on ur.RolId = p.RolId and ur.EstadoId = 1
            inner join Usuario u on (u.UsuarioId = ur.UsuarioId or u.EsAdmin = 1) and u.EstadoId = 1
            where f.EstadoId = 1
              and u.UsuarioId = @UsuarioId
            ";
            DB db = new DB();
            DataTable dt = db.GetDataTable(query, CommandType.Text, new SqlParameter("@UsuarioId", usuarioId));
            List<string> listaDeForms = new List<string>();
            foreach (DataRow dr in dt.Rows) listaDeForms.Add(dr.Field<string>("Nombre"));
            return listaDeForms;
        }
    }
}
