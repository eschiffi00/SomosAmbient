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
    public partial class REC_detalleOperator
    {
        //public static int SaveWithRelative(List<REC_detalle> rEC_detalle)
        //{
        //    if (!DbEntidades.Seguridad.Permiso("PermisoREC_detalleSave")) throw new PermisoException();
        //    foreach(var rec in rEC_detalle) 
        //    { 
        //        if (rec.RecetaID == -1)
        //        {   
        //            Insert(rec);
        //        }
        //        else
        //        {
        //             Update(rec);
        //        }
        //    }
        //}
    }
}
