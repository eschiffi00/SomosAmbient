using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CostosPaquetesCIAmbientacion
    {
		public int Id { get; set; }
		public int PaqueteCIID { get; set; }
		public int SegmentoId { get; set; }
		public int CaracteristicaId { get; set; }
		public int ProveedorId { get; set; }
		public int CantidadPaquetes { get; set; }
		public int Semestre { get; set; }
		public int Anio { get; set; }
		public decimal Precio { get; set; }
		public decimal Costo { get; set; }
		public decimal Margen { get; set; }
		public decimal CostoFlete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PaqueteCIID: " + PaqueteCIID.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"CaracteristicaId: " + CaracteristicaId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"CantidadPaquetes: " + CantidadPaquetes.ToString() + "\r\n " + 
			"Semestre: " + Semestre.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Margen: " + Margen.ToString() + "\r\n " + 
			"CostoFlete: " + CostoFlete.ToString() + "\r\n " ;
		}
        public CostosPaquetesCIAmbientacion()
        {
            Id = -1;

        }

		public AmbientacionCI GetRelatedPaqueteCIID()
		{
			AmbientacionCI ambientacionCI = AmbientacionCIOperator.GetOneByIdentity(PaqueteCIID);
			return ambientacionCI;
		}

		public Segmentos GetRelatedSegmentoId()
		{
			Segmentos segmentos = SegmentosOperator.GetOneByIdentity(SegmentoId);
			return segmentos;
		}

		public Caracteristicas GetRelatedCaracteristicaId()
		{
			Caracteristicas caracteristicas = CaracteristicasOperator.GetOneByIdentity(CaracteristicaId);
			return caracteristicas;
		}

		public Proveedores GetRelatedProveedorId()
		{
			Proveedores proveedores = ProveedoresOperator.GetOneByIdentity(ProveedorId);
			return proveedores;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PaqueteCIID": return false;
				case "SegmentoId": return false;
				case "CaracteristicaId": return false;
				case "ProveedorId": return false;
				case "CantidadPaquetes": return false;
				case "Semestre": return false;
				case "Anio": return false;
				case "Precio": return false;
				case "Costo": return false;
				case "Margen": return false;
				case "CostoFlete": return false;
				default: return false;
			}
		}
    }
}

