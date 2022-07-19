namespace DBEntidades.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Perfiles
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }
    }
}
