using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Impuestos
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public decimal Porcentaje { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Porcentaje: " + Porcentaje.ToString() + "\r\n " ;
		}
        public Impuestos()
        {
            Id = -1;

        }



		public List<ComprobantesProveedores_Detalles> GetRelatedComprobantesProveedores_Detalleses()
		{
			return ComprobantesProveedores_DetallesOperator.GetAll().Where(x => x.TipoImpuestoId == Id).ToList();
		}
		public List<TipoComprobante_Impuestos> GetRelatedTipoComprobante_Impuestoses()
		{
			return TipoComprobante_ImpuestosOperator.GetAll().Where(x => x.ImpuestoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Porcentaje": return true;
				default: return false;
			}
		}
    }
}

