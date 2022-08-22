using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Proveedores
    {
		public int Id { get; set; }
		public string RazonSocial { get; set; }
		public string Cuit { get; set; }
		public string Propio { get; set; }
		public string NombreFantasia { get; set; }
		public string Telefono { get; set; }
		public string CBU { get; set; }
		public string NroCliente { get; set; }
		public string NroIIBB { get; set; }
		public string Localidad { get; set; }
		public string Provincia { get; set; }
		public int? CondicionIvaId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"RazonSocial: " + RazonSocial.ToString() + "\r\n " + 
			"Cuit: " + Cuit.ToString() + "\r\n " + 
			"Propio: " + Propio.ToString() + "\r\n " + 
			"NombreFantasia: " + NombreFantasia.ToString() + "\r\n " + 
			"Telefono: " + Telefono.ToString() + "\r\n " + 
			"CBU: " + CBU.ToString() + "\r\n " + 
			"NroCliente: " + NroCliente.ToString() + "\r\n " + 
			"NroIIBB: " + NroIIBB.ToString() + "\r\n " + 
			"Localidad: " + Localidad.ToString() + "\r\n " + 
			"Provincia: " + Provincia.ToString() + "\r\n " + 
			"CondicionIvaId: " + CondicionIvaId.ToString() + "\r\n " ;
		}
        public Proveedores()
        {
            Id = -1;

        }

		public CondicionIva GetRelatedCondicionIvaId()
		{
			if (CondicionIvaId != null)
			{
				CondicionIva condicionIva = CondicionIvaOperator.GetOneByIdentity(CondicionIvaId ?? 0);
				return condicionIva;
			}
			return null;
		}



		public List<TecnicaSalon> GetRelatedTecnicaSalones()
		{
			return TecnicaSalonOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}
		public List<AmbientacionSalon> GetRelatedAmbientacionSalones()
		{
			return AmbientacionSalonOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}
		public List<Intermediarios> GetRelatedIntermediarioses()
		{
			return IntermediariosOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}
		public List<CostosPaquetesCIAmbientacion> GetRelatedCostosPaquetesCIAmbientaciones()
		{
			return CostosPaquetesCIAmbientacionOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}
		public List<ProveedoresFormasdePago> GetRelatedProveedoresFormasdePagos()
		{
			return ProveedoresFormasdePagoOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}
		public List<UnidadesNegocios_Proveedores> GetRelatedUnidadesNegocios_Proveedoreses()
		{
			return UnidadesNegocios_ProveedoresOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}
		public List<Rubros_Proveedores> GetRelatedRubros_Proveedoreses()
		{
			return Rubros_ProveedoresOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}
		public List<ComprobantesProveedores> GetRelatedComprobantesProveedoreses()
		{
			return ComprobantesProveedoresOperator.GetAll().Where(x => x.ProveedorId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "RazonSocial": return false;
				case "Cuit": return true;
				case "Propio": return true;
				case "NombreFantasia": return true;
				case "Telefono": return true;
				case "CBU": return true;
				case "NroCliente": return true;
				case "NroIIBB": return true;
				case "Localidad": return true;
				case "Provincia": return true;
				case "CondicionIvaId": return true;
				default: return false;
			}
		}
    }
}

