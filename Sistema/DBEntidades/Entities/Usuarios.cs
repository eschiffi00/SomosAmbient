namespace DBEntidades.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {
        [Key]
        public int EmpleadoID { get; set; }

        [Required]
        [StringLength(50)]
        public string User { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int PerfilID { get; set; }

        public int? EstadoID { get; set; }
    }
}
