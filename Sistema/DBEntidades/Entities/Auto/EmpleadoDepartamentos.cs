using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class EmpleadoDepartamentos
    {
		public int Id { get; set; }
		public int EmpleadoId { get; set; }
		public int DepartamentoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"DepartamentoId: " + DepartamentoId.ToString() + "\r\n " ;
		}
        public EmpleadoDepartamentos()
        {
            Id = -1;

        }

		public Empleados GetRelatedEmpleadoId()
		{
			Empleados empleados = EmpleadosOperator.GetOneByIdentity(EmpleadoId);
			return empleados;
		}

		public Departamentos GetRelatedDepartamentoId()
		{
			Departamentos departamentos = DepartamentosOperator.GetOneByIdentity(DepartamentoId);
			return departamentos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "EmpleadoId": return false;
				case "DepartamentoId": return false;
				default: return false;
			}
		}
    }
}

