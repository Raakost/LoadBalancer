using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SearcherAPI.Models;

namespace SearcherAPI.Controllers
{
    public class TextsController : ApiController
    {
        private SearcherContext db = new SearcherContext();

        // GET: api/Texts
        public async Task<IHttpActionResult> GetTexts()
        {
            var list = await db.Texts.ToListAsync();
            return Ok(list);
        }

        // GET: api/Texts/5
        [ResponseType(typeof(Texts))]
        public async Task<IHttpActionResult> GetTexts(int id)
        {
            Texts texts = await db.Texts.FirstOrDefaultAsync(x => x.id == id);
            if (texts == null)
            {
                return NotFound();
            }

            return Ok(texts);
        }

        // PUT: api/Texts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTexts(int id, Texts texts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != texts.id)
            {
                return BadRequest();
            }

            db.Entry(texts).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextsExists(id))
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

        // POST: api/Texts
        [ResponseType(typeof(Texts))]
        public async Task<IHttpActionResult> PostTexts(Texts texts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Texts.Add(texts);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = texts.id }, texts);
        }

        // DELETE: api/Texts/5
        [ResponseType(typeof(Texts))]
        public async Task<IHttpActionResult> DeleteTexts(int id)
        {
            Texts texts = await db.Texts.FindAsync(id);
            if (texts == null)
            {
                return NotFound();
            }

            db.Texts.Remove(texts);
            await db.SaveChangesAsync();

            return Ok(texts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TextsExists(int id)
        {
            return db.Texts.Count(e => e.id == id) > 0;
        }
    }
}