using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Requerimiento
    {
		public int Id { get; set; }
		public string Detalle { get; set; }
		public DateTime Fecha { get; set; }
		public int EstadoId { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public DateTime? DeleteFecha { get; set; }
		public int? Delete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Detalle: " + Detalle.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"DeleteFecha: " + DeleteFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " ;
		}
        public INVENTARIO_Requerimiento()
        {
            Id = -1;

        }



		public List<INVENTARIO_Requerimiento_Detalle> GetRelatedINVENTARIO_Requerimiento_Detalles()
		{
			return INVENTARIO_Requerimiento_DetalleOperator.GetAll().Where(x => x.RequerimientoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Detalle": return false;
				case "Fecha": return false;
				case "EstadoId": return false;
				case "CreateFecha": return false;
				case "UpdateFecha": return true;
				case "DeleteFecha": return true;
				case "Delete": return true;
				default: return false;
			}
		}
    }
}

