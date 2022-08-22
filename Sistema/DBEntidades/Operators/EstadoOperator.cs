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
    public partial class EstadosOperator
    {
        public static int GetHablitadoID()
        {
            int u = GetAll().Where(x => x.Descripcion == "Activo" && x.Entidad == "Items").FirstOrDefault().Id;
            return u;
        }
        public static int GetDeshabilitadoID()
        {
            int u = GetAll().Where(x => x.Descripcion == "Inactivo" && x.Entidad == "Items").FirstOrDefault().Id;
            return u;
        }
    }
}
