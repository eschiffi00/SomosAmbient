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
    public partial class UsuarioRolOperator
    {

        public static void DeleteForUser(int usuarioId)
        {
            string query = "delete from UsuarioRol where UsuarioId = " + usuarioId.ToString();
            DB db = new DB();
            db.ExecuteNonQuery(query);
        }

        public static Boolean ExisteRolEnUsuario(int usuarioId, int rolId)
        {
            List<UsuarioRol> p = GetAllEstado1().Where(x => x.UsuarioId == usuarioId && x.RolId == rolId).ToList();
            if (p.Count > 0) return true;
            else return false;
        }

        public static List<string> GetRolesDeUsuario(int usuarioId)
        {
            List<string> roles = new List<String>();

            string query = @"
            select r.Nombre from UsuarioRol ur 
            join Rol r on r.RolId = ur.RolId
            where ur.UsuarioId = @usuarioId
            and ur.EstadoId = 1 and r.EstadoId = 1
            ";
            DB db = new DB();
            DataTable dt = db.GetDataTable(query, CommandType.Text, new SqlParameter("@usuarioId", usuarioId));
            foreach (DataRow dr in dt.Rows)
            {
                roles.Add(dr[0].ToString());
            }
            return roles;
        }

        public static Boolean EsVendedor(int usuarioId)
        {
            List<string> roles = GetRolesDeUsuario(usuarioId);
            if (roles.Contains("Vendedores") && roles.Count == 1) return true;
            else return false;
        }
        public static Boolean EsGerente(int usuarioId)
        {
            List<string> roles = GetRolesDeUsuario(usuarioId);
            if (roles.Contains("Gerentes") && roles.Count == 1) return true;
            else return false;
        }

    }
}
