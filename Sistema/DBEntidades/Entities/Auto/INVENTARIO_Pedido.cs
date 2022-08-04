using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Pedido
    {
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public int ProveedorId { get; set; }
		public int EstadoId { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public DateTime? DeleteFecha { get; set; }
		public int? Delete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"DeleteFecha: " + DeleteFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " ;
		}
        public INVENTARIO_Pedido()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Fecha": return false;
				case "ProveedorId": return false;
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

