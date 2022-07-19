using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class RecetaDetalle
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int StockID { get; set; }
		public decimal? Peso { get; set; }
		public int? Cantidad { get; set; }
		public List<REC_detalle> Detalle { get; set; }
		public List<REC_pasos> Pasos { get; set; }
		public int EstadoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"StockID: " + StockID.ToString() + "\r\n " ;
		}
        public RecetaDetalle()
        {
			ID = -1;
			StockID = -1;
        }
    }
}

