using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Bancos
    {
		public int Id { get; set; }
		public string Codigo { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Codigo: " + Codigo.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public Bancos()
        {
            Id = -1;

        }



		public List<Cheques> GetRelatedChequeses()
		{
			return ChequesOperator.GetAll().Where(x => x.BancoId == Id).ToList();
		}
		public List<Transferencias> GetRelatedTransferenciases()
		{
			return TransferenciasOperator.GetAll().Where(x => x.BancoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Codigo": return false;
				case "Descripcion": return false;
				default: return false;
			}
		}
    }
}

