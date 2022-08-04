using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Tiempo
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int Duracion { get; set; }
		public int Orden { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Duracion: " + Duracion.ToString() + "\r\n " + 
			"Orden: " + Orden.ToString() + "\r\n " ;
		}
        public Tiempo()
        {
            ID = -1;

        }



		public List<ExpTiempoProd> GetRelatedExpTiempoProdes()
		{
			return ExpTiempoProdOperator.GetAll().Where(x => x.TiempoID == ID).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "Duracion": return false;
				case "Orden": return false;
				default: return false;
			}
		}
    }
}

