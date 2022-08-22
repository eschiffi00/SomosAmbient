using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class FormasdePago
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public FormasdePago()
        {
            Id = -1;

        }



		public List<PagosClientes> GetRelatedPagosClienteses()
		{
			return PagosClientesOperator.GetAll().Where(x => x.FormadePagoId == Id).ToList();
		}
		public List<ProveedoresFormasdePago> GetRelatedProveedoresFormasdePagos()
		{
			return ProveedoresFormasdePagoOperator.GetAll().Where(x => x.FormadePagoId == Id).ToList();
		}
		public List<Recibos> GetRelatedReciboses()
		{
			return RecibosOperator.GetAll().Where(x => x.FormadePagoId == Id).ToList();
		}
		public List<Eventos> GetRelatedEventoses()
		{
			return EventosOperator.GetAll().Where(x => x.FormadePagoId == Id).ToList();
		}
		public List<ComprobantesProveedores> GetRelatedComprobantesProveedoreses()
		{
			return ComprobantesProveedoresOperator.GetAll().Where(x => x.FormadePagoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return true;
				default: return false;
			}
		}
    }
}

