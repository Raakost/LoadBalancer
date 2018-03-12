using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LoadBalancer.LoadHelper;

namespace LoadBalancer.Controllers
{
    public class TextsController : ApiController
    {
        private HttpClient Client = new HttpClient();
        private RoundRobinHelper Helper = RoundRobinHelper.getInstance();

        public async Task<IHttpActionResult> GetTexts()
        {
            var url = Helper.GetNextUrl();

            MonitorLog monitorLog = new MonitorLog();
            TextsWithLog textLog = new TextsWithLog();
            monitorLog.BeginTime = DateTime.Now;
            monitorLog.URL = url;

            HttpResponseMessage response = await Client.GetAsync(url + "api/texts/");
            Thread.Sleep(5000);
            if (response.IsSuccessStatusCode)
            {
                 List<Texts> texts = await response.Content.ReadAsAsync<List<Texts>>();

                if (texts == null)
                {
                    return NotFound();
                }

                monitorLog.EndTime = DateTime.Now;

                var logResult = await Client.PostAsJsonAsync(url + "api/monitorLogs/", monitorLog);

                textLog.MonitorLog = await logResult.Content.ReadAsAsync<MonitorLog>();
                textLog.Texts = texts;

                return Ok(textLog);
            }
            return BadRequest();
        }

        // GET: api/Texts/5
        [ResponseType(typeof(Texts))]
        public async Task<IHttpActionResult> GetTexts(int id)
        {
            var url = Helper.GetNextUrl();

            HttpResponseMessage response = await Client.GetAsync(url + "api/texts/" + id);

            if (response.IsSuccessStatusCode)
            {
                Texts texts = await response.Content.ReadAsAsync<Texts>();

                if (texts == null)
                {
                    return NotFound();
                }
                return Ok(texts);
            }
            return BadRequest();
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

            var url = Helper.GetNextUrl();

            HttpResponseMessage response = Client.PutAsJsonAsync(url + "api/texts/" + id, texts).Result;
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsAsync<Texts>());
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

            var url = Helper.GetNextUrl();

            HttpResponseMessage response = Client.PostAsJsonAsync(url + "api/texts/", texts).Result;
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsAsync<Texts>());
            }

            return BadRequest("haha");
        }

        // DELETE: api/Texts/5
        [ResponseType(typeof(Texts))]
        public async Task<IHttpActionResult> DeleteTexts(int id)
        {
            var url = Helper.GetNextUrl();

            HttpResponseMessage response = Client.DeleteAsync(url + "api/texts/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsAsync<Texts>());
            }

            return BadRequest("hehe");
        }
    }
}
