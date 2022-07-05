using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Documentos
    {
		public int Id { get; set; }
		public int TipoDocumentoId { get; set; }
		public DateTime Fecha { get; set; }
		public int EntidadId { get; set; }
		public int EmpresaId { get; set; }
		public int PuntoVenta { get; set; }
		public string NroDocumento { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoDocumentoId: " + TipoDocumentoId.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"EntidadId: " + EntidadId.ToString() + "\r\n " + 
			"EmpresaId: " + EmpresaId.ToString() + "\r\n " + 
			"PuntoVenta: " + PuntoVenta.ToString() + "\r\n " + 
			"NroDocumento: " + NroDocumento.ToString() + "\r\n " ;
		}
        public Documentos()
        {
            Id = -1;

        }

		public TipoDocumentos GetRelatedTipoDocumentoId()
		{
			TipoDocumentos tipoDocumentos = TipoDocumentosOperator.GetOneByIdentity(TipoDocumentoId);
			return tipoDocumentos;
		}

		public Entidades GetRelatedEntidadId()
		{
			Entidades entidades = EntidadesOperator.GetOneByIdentity(EntidadId);
			return entidades;
		}

		public Empresas GetRelatedEmpresaId()
		{
			Empresas empresas = EmpresasOperator.GetOneByIdentity(EmpresaId);
			return empresas;
		}



		public List<Pagos> GetRelatedPagoses()
		{
			return PagosOperator.GetAll().Where(x => x.DocumentoId == Id).ToList();
		}
		public List<DocumentosDetalle> GetRelatedDocumentosDetalles()
		{
			return DocumentosDetalleOperator.GetAll().Where(x => x.DocumentoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "TipoDocumentoId": return false;
				case "Fecha": return false;
				case "EntidadId": return false;
				case "EmpresaId": return false;
				case "PuntoVenta": return false;
				case "NroDocumento": return false;
				default: return false;
			}
		}
    }
}

