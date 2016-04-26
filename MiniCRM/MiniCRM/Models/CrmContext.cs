using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MiniCRM.Models
{
    public class CrmContext: DbContext
    {
        public DbSet<Contacto>Contactos { get; set; }

        public DbSet<Anotacion> Anotaciones { get; set; }

    }
}