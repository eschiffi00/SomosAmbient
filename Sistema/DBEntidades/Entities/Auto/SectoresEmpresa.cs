using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class SectoresEmpresa
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int DepartamentoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"DepartamentoId: " + DepartamentoId.ToString() + "\r\n " ;
		}
        public SectoresEmpresa()
        {
			Id = -1;

        }

		public Departamentos GetRelatedDepartamentoId()
		{
			Departamentos departamentos = DepartamentosOperator.GetOneByIdentity(DepartamentoId);
			return departamentos;
		}



		public List<TipoEmpleados> GetRelatedTipoEmpleadoses()
		{
			return TipoEmpleadosOperator.GetAll().Where(x => x.SectorEmpresaId == Id).ToList();
		}
		public List<Empleados> GetRelatedEmpleadoses()
		{
			return EmpleadosOperator.GetAll().Where(x => x.SectorEmpresaId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "DepartamentoId": return false;
				default: return false;
			}
		}
    }
}

