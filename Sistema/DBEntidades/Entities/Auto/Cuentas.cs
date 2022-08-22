using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Cuentas
    {
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public string TipoCuenta { get; set; }
		public int MonedaId { get; set; }
		public int? EmpresaId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"TipoCuenta: " + TipoCuenta.ToString() + "\r\n " + 
			"MonedaId: " + MonedaId.ToString() + "\r\n " + 
			"EmpresaId: " + EmpresaId.ToString() + "\r\n " ;
		}
        public Cuentas()
        {
            Id = -1;

        }

		public Monedas GetRelatedMonedaId()
		{
			Monedas monedas = MonedasOperator.GetOneByIdentity(MonedaId);
			return monedas;
		}

		public Empresas GetRelatedEmpresaId()
		{
			if (EmpresaId != null)
			{
				Empresas empresas = EmpresasOperator.GetOneByIdentity(EmpresaId ?? 0);
				return empresas;
			}
			return null;
		}



		public List<Cuentas_Log> GetRelatedCuentas_Loges()
		{
			return Cuentas_LogOperator.GetAll().Where(x => x.CuentaId == Id).ToList();
		}
		public List<PagosProveedores> GetRelatedPagosProveedoreses()
		{
			return PagosProveedoresOperator.GetAll().Where(x => x.CuentaId == Id).ToList();
		}
		public List<ComprobantesProveedores> GetRelatedComprobantesProveedoreses()
		{
			return ComprobantesProveedoresOperator.GetAll().Where(x => x.CuentaId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Nombre": return false;
				case "Descripcion": return true;
				case "TipoCuenta": return false;
				case "MonedaId": return false;
				case "EmpresaId": return true;
				default: return false;
			}
		}
    }
}

