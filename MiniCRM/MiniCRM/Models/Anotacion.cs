using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiniCRM.Models
{
    public class Anotacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Fecha { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int ContactoId { get; set; }

        [ForeignKey("ContactoId")]
        public virtual Contacto Contacto { get; set;}

    }
}