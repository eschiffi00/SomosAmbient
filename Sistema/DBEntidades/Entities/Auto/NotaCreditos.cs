using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class NotaCreditos
    {
		public int Id { get; set; }
		public int? ComprobanteProveedorId { get; set; }
		public DateTime Fecha { get; set; }
		public decimal Importe { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public int Delete { get; set; }
		public DateTime? DeleteFecha { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ComprobanteProveedorId: " + ComprobanteProveedorId.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"DeleteFecha: " + DeleteFecha.ToString() + "\r\n " ;
		}
        public NotaCreditos()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ComprobanteProveedorId": return true;
				case "Fecha": return false;
				case "Importe": return false;
				case "CreateFecha": return false;
				case "UpdateFecha": return true;
				case "Delete": return false;
				case "DeleteFecha": return true;
				default: return false;
			}
		}
    }
}

