using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ExpTiempoProd
    {
		public int ID { get; set; }
		public int ExperienciaID { get; set; }
		public int TiempoID { get; set; }
		public int ProductoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"ExperienciaID: " + ExperienciaID.ToString() + "\r\n " + 
			"TiempoID: " + TiempoID.ToString() + "\r\n " + 
			"ProductoID: " + ProductoID.ToString() + "\r\n " ;
		}
        public ExpTiempoProd()
        {
            ID = -1;

        }

		public Experiencia GetRelatedExperienciaID()
		{
			Experiencia experiencia = ExperienciaOperator.GetOneByIdentity(ExperienciaID);
			return experiencia;
		}

		public Tiempo GetRelatedTiempoID()
		{
			Tiempo tiempo = TiempoOperator.GetOneByIdentity(TiempoID);
			return tiempo;
		}

		public Items GetRelatedProductoID()
		{
			Items producto = ItemsOperator.GetOneByIdentity(ProductoID);
			return producto;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "ExperienciaID": return false;
				case "TiempoID": return false;
				case "ProductoID": return false;
				default: return false;
			}
		}
    }
}

