using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Depositos
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public int Delete { get; set; }
		public DateTime? DeleteDate { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CreateDate: " + CreateDate.ToString() + "\r\n " + 
			"UpdateDate: " + UpdateDate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"DeleteDate: " + DeleteDate.ToString() + "\r\n " ;
		}
        public INVENTARIO_Depositos()
        {
            Id = -1;

			CreateDate = DateTime.Now;
			Delete = 0;
        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "CreateDate": return false;
				case "UpdateDate": return true;
				case "Delete": return false;
				case "DeleteDate": return true;
				default: return false;
			}
		}
    }
}

