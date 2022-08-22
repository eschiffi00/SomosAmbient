using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CodigoPorRubro
    {
		public int id { get; set; }
		public int RubroId { get; set; }
		public int Codigo { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"id: " + id.ToString() + "\r\n " + 
			"RubroId: " + RubroId.ToString() + "\r\n " + 
			"Codigo: " + Codigo.ToString() + "\r\n " ;
		}
        public CodigoPorRubro()
        {
            id = -1;

        }

		public Rubros GetRelatedRubroId()
		{
			Rubros rubros = RubrosOperator.GetOneByIdentity(RubroId);
			return rubros;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "id": return false;
				case "RubroId": return false;
				case "Codigo": return false;
				default: return false;
			}
		}
    }
}

