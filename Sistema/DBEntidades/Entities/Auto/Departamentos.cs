using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Departamentos
    {
		public int Id { get; set; }
		public string Nombre { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " ;
		}
        public Departamentos()
        {
            Id = -1;

        }



		public List<EmpleadoDepartamentos> GetRelatedEmpleadoDepartamentoses()
		{
			return EmpleadoDepartamentosOperator.GetAll().Where(x => x.DepartamentoId == Id).ToList();
		}
		public List<SectoresEmpresa> GetRelatedSectoresEmpresas()
		{
			return SectoresEmpresaOperator.GetAll().Where(x => x.DepartamentoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Nombre": return false;
				default: return false;
			}
		}
    }
}

