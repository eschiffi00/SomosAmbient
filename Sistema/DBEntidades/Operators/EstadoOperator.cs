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
    public partial class EstadoOperator
    {
        public static int GetHablitadoID()
        {
            int u = GetAll().Where(x => x.Descripcion == "Habilitado").FirstOrDefault().ID;
            return u;
        }
        public static int GetDeshabilitadoID()
        {
            int u = GetAll().Where(x => x.Descripcion == "Deshabilitado").FirstOrDefault().ID;
            return u;
        }
    }
}
