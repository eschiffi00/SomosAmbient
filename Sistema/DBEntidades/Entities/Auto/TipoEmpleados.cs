using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoEmpleados
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int? SectorEmpresaId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"SectorEmpresaId: " + SectorEmpresaId.ToString() + "\r\n " ;
		}
        public TipoEmpleados()
        {
            Id = -1;

        }

		public SectoresEmpresa GetRelatedSectorEmpresaId()
		{
			if (SectorEmpresaId != null)
			{
				SectoresEmpresa sectoresEmpresa = SectoresEmpresaOperator.GetOneByIdentity(SectorEmpresaId ?? 0);
				return sectoresEmpresa;
			}
			return null;
		}



		public List<Empleados> GetRelatedEmpleadoses()
		{
			return EmpleadosOperator.GetAll().Where(x => x.TipoEmpleadoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "SectorEmpresaId": return true;
				default: return false;
			}
		}
    }
}

