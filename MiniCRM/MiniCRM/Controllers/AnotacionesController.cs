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
    public class AnotacionesController : ApiController
    {
        private CrmContext db = new CrmContext();

        // GET: api/Anotaciones
        public IQueryable<Anotacion> GetAnotaciones()
        {
            return db.Anotaciones;
        }

        // GET: api/Anotaciones/5
        [ResponseType(typeof(Anotacion))]
        public IHttpActionResult GetAnotacion(int id)
        {
            Anotacion anotacion = db.Anotaciones.Find(id);
            if (anotacion == null)
            {
                return NotFound();
            }

            return Ok(anotacion);
        }

        // PUT: api/Anotaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnotacion(int id, Anotacion anotacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anotacion.Id)
            {
                return BadRequest();
            }

            db.Entry(anotacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnotacionExists(id))
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

        // POST: api/Anotaciones
        [ResponseType(typeof(Anotacion))]
        public IHttpActionResult PostAnotacion(Anotacion anotacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Anotaciones.Add(anotacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = anotacion.Id }, anotacion);
        }

        // DELETE: api/Anotaciones/5
        [ResponseType(typeof(Anotacion))]
        public IHttpActionResult DeleteAnotacion(int id)
        {
            Anotacion anotacion = db.Anotaciones.Find(id);
            if (anotacion == null)
            {
                return NotFound();
            }

            db.Anotaciones.Remove(anotacion);
            db.SaveChanges();

            return Ok(anotacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnotacionExists(int id)
        {
            return db.Anotaciones.Count(e => e.Id == id) > 0;
        }
    }
}