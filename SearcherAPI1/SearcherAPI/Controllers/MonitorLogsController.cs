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
    public class MonitorLogsController : ApiController
    {
        private SearcherContext db = new SearcherContext();

        // GET: api/MonitorLogs
        public async Task<IHttpActionResult> GetLogging()
        {
            var list = await db.Logging.ToListAsync();
            return Ok(list);
        }

        // GET: api/MonitorLogs/5
        [ResponseType(typeof(MonitorLog))]
        public async Task<IHttpActionResult> GetMonitorLog(int id)
        {
            MonitorLog monitorLog = await db.Logging.FirstOrDefaultAsync(x => x.Id == id);
            if (monitorLog == null)
            {
                return NotFound();
            }

            return Ok(monitorLog);
        }


        // POST: api/MonitorLogs
        [ResponseType(typeof(MonitorLog))]
        public async Task<IHttpActionResult> PostMonitorLog(MonitorLog monitorLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Logging.Add(monitorLog);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = monitorLog.Id}, monitorLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool MonitorLogExists(int id)
        {
            return db.Logging.Count(e => e.Id == id) > 0;
        }
    }
}