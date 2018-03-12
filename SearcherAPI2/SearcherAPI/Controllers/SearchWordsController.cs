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
    public class SearchWordsController : ApiController
    {
        private SearcherContext db = new SearcherContext();

        // GET: api/SearchWords
        public async Task<IHttpActionResult> GetSearchWords()
        {
            var list = await db.SearchWords.Include("Texts").ToListAsync();
            return Ok(list);
        }

        // GET: api/SearchWords/5
        [ResponseType(typeof(SearchWords))]
        public async Task<IHttpActionResult> GetSearchWords(int id)
        {
            SearchWords searchWords = await db.SearchWords.Include("Texts").FirstOrDefaultAsync(x => x.id == id);
            if (searchWords == null)
            {
                return NotFound();
            }

            return Ok(searchWords);
        }

        // PUT: api/SearchWords/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSearchWords(int id, SearchWords searchWords)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != searchWords.id)
            {
                return BadRequest();
            }

            db.Entry(searchWords).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchWordsExists(id))
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

        // POST: api/SearchWords
        [ResponseType(typeof(SearchWords))]
        public async Task<IHttpActionResult> PostSearchWords(SearchWords searchWords)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SearchWords.Add(searchWords);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = searchWords.id }, searchWords);
        }

        // DELETE: api/SearchWords/5
        [ResponseType(typeof(SearchWords))]
        public async Task<IHttpActionResult> DeleteSearchWords(int id)
        {
            SearchWords searchWords = await db.SearchWords.FindAsync(id);
            if (searchWords == null)
            {
                return NotFound();
            }

            db.SearchWords.Remove(searchWords);
            await db.SaveChangesAsync();

            return Ok(searchWords);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SearchWordsExists(int id)
        {
            return db.SearchWords.Count(e => e.id == id) > 0;
        }
    }
}