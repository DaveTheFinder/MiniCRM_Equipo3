using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MiniCRM.Models;

namespace MiniCRM.Controllers
{
    public class ContactosController : ApiController
    {
        private CrmContext db = new CrmContext();

        // GET: api/Contactos
        public IQueryable<Contacto> GetContactos()
        {
            return db.Contactos;
        }

        // GET: api/Contactos/5
        [ResponseType(typeof(Contacto))]
        public IHttpActionResult GetContacto(int id)
        {
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        // PUT: api/Contactos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContacto(int id, Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacto.Id)
            {
                return BadRequest();
            }

            db.Entry(contacto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Contactos
        [ResponseType(typeof(Contacto))]
        public IHttpActionResult PostContacto(Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contactos.Add(contacto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contacto.Id }, contacto);
        }

        // DELETE: api/Contactos/5
        [ResponseType(typeof(Contacto))]
        public IHttpActionResult DeleteContacto(int id)
        {
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            db.Contactos.Remove(contacto);
            db.SaveChanges();

            return Ok(contacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactoExists(int id)
        {
            return db.Contactos.Count(e => e.Id == id) > 0;
        }
    }
}