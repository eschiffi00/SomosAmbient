using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Empresas
    {
		public int Id { get; set; }
		public string RazonSocial { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"RazonSocial: " + RazonSocial.ToString() + "\r\n " ;
		}
        public Empresas()
        {
            Id = -1;

        }



		public List<Cuentas> GetRelatedCuentases()
		{
			return CuentasOperator.GetAll().Where(x => x.EmpresaId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "RazonSocial": return false;
				default: return false;
			}
		}
    }
}

