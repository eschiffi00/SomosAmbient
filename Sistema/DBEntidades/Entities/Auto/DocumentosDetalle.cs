using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class DocumentosDetalle
    {
		public int Id { get; set; }
		public int DocumentoId { get; set; }
		public int ProductoId { get; set; }
		public decimal Neto { get; set; }
		public decimal Subtotal { get; set; }
		public decimal Total { get; set; }
		public int Cantidad { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"DocumentoId: " + DocumentoId.ToString() + "\r\n " + 
			"ProductoId: " + ProductoId.ToString() + "\r\n " + 
			"Neto: " + Neto.ToString() + "\r\n " + 
			"Subtotal: " + Subtotal.ToString() + "\r\n " + 
			"Total: " + Total.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " ;
		}
        public DocumentosDetalle()
        {
            Id = -1;

        }

		public Documentos GetRelatedDocumentoId()
		{
			Documentos documentos = DocumentosOperator.GetOneByIdentity(DocumentoId);
			return documentos;
		}

		public Productos GetRelatedProductoId()
		{
			Productos productos = ProductosOperator.GetOneByIdentity(ProductoId);
			return productos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "DocumentoId": return false;
				case "ProductoId": return false;
				case "Neto": return true;
				case "Subtotal": return false;
				case "Total": return false;
				case "Cantidad": return false;
				default: return false;
			}
		}
    }
}

