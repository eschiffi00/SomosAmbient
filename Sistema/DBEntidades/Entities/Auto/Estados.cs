using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Estados
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string Entidad { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Entidad: " + Entidad.ToString() + "\r\n " ;
		}
        public Estados()
        {
            Id = -1;

        }



		public List<PagosClientes> GetRelatedPagosClienteses()
		{
			return PagosClientesOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<Cheques> GetRelatedChequeses()
		{
			return ChequesOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<FacturasCliente> GetRelatedFacturasClientes()
		{
			return FacturasClienteOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<Usuarios> GetRelatedUsuarioses()
		{
			return UsuariosOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<DegustacionDetalle> GetRelatedDegustacionDetalles()
		{
			return DegustacionDetalleOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<Degustacion> GetRelatedDegustaciones()
		{
			return DegustacionOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<Empleados> GetRelatedEmpleadoses()
		{
			return EmpleadosOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<Presupuestos> GetRelatedPresupuestoses()
		{
			return PresupuestosOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<Eventos> GetRelatedEventoses()
		{
			return EventosOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<Adicionales> GetRelatedAdicionaleses()
		{
			return AdicionalesOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}
		public List<ComprobantesProveedores> GetRelatedComprobantesProveedoreses()
		{
			return ComprobantesProveedoresOperator.GetAll().Where(x => x.EstadoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Entidad": return true;
				default: return false;
			}
		}
    }
}

