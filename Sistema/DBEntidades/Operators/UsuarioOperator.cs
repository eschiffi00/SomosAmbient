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
    public partial class UsuarioOperator
    { 
        public static void Delete(int id)
        {
            Usuario u = GetOneByIdentity(id);
            u.EstadoId = 2;
            Update(u);
        }

        public static Usuario GetOneByLoginAndEstado(string loginName, int estado)
        {
            Usuario u = GetAllEstadoN(estado).Where(x => x.LoginName == loginName).FirstOrDefault();
            return u;
        }

        public static Usuario GetOneByEmailAndEstado(string loginName, int estado)
        {
            Usuario u = GetAllEstadoN(estado).Where(x => x.Email == loginName).FirstOrDefault();
            return u;
        }

    }
}