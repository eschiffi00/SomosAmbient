using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Pagos
    {
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public int DocumentoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"DocumentoId: " + DocumentoId.ToString() + "\r\n " ;
		}
        public Pagos()
        {
            Id = -1;

        }

		public Documentos GetRelatedDocumentoId()
		{
			Documentos documentos = DocumentosOperator.GetOneByIdentity(DocumentoId);
			return documentos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Fecha": return false;
				case "DocumentoId": return false;
				default: return false;
			}
		}
    }
}

