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
		public string Cuit { get; set; }
		public int TipoEmpresa { get; set; }
		public string CondicionIva { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"RazonSocial: " + RazonSocial.ToString() + "\r\n " + 
			"Cuit: " + Cuit.ToString() + "\r\n " + 
			"TipoEmpresa: " + TipoEmpresa.ToString() + "\r\n " + 
			"CondicionIva: " + CondicionIva.ToString() + "\r\n " ;
		}
        public Empresas()
        {
            Id = -1;

			TipoEmpresa = 1;
        }



		public List<PagosClientes> GetRelatedPagosClienteses()
		{
			return PagosClientesOperator.GetAll().Where(x => x.EmpresaId == Id).ToList();
		}
		public List<FacturasCliente> GetRelatedFacturasClientes()
		{
			return FacturasClienteOperator.GetAll().Where(x => x.EmpresaId == Id).ToList();
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
				case "Cuit": return true;
				case "TipoEmpresa": return false;
				case "CondicionIva": return false;
				default: return false;
			}
		}
    }
}

