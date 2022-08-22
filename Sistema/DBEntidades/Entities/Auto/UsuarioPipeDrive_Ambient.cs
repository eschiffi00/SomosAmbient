using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class UsuarioPipeDrive_Ambient
    {
		public int Id { get; set; }
		public int UserPipeDriveId { get; set; }
		public int UserAmbientId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"UserPipeDriveId: " + UserPipeDriveId.ToString() + "\r\n " + 
			"UserAmbientId: " + UserAmbientId.ToString() + "\r\n " ;
		}
        public UsuarioPipeDrive_Ambient()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "UserPipeDriveId": return false;
				case "UserAmbientId": return false;
				default: return false;
			}
		}
    }
}

