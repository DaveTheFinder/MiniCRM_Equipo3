using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniCRM.Models
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        public virtual List<Anotacion> Anotaciones { get; set; }
    }
}