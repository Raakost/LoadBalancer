using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using LoadBalancer.LoadHelper;
using LoadBalancer.Models;

namespace LoadBalancer.Controllers
{
    public class SearchWordsController : ApiController
    {
        private HttpClient Client = new HttpClient();
        private RoundRobinHelper Helper = RoundRobinHelper.getInstance();
        public async Task<IHttpActionResult> GetSearchWords()
        {
            var url = Helper.GetNextUrl();

            MonitorLog monitorLog = new MonitorLog();
            SearchWordsWithLog searchwordLog = new SearchWordsWithLog();
            monitorLog.BeginTime = DateTime.Now;
            monitorLog.URL = url;

            HttpResponseMessage response = await Client.GetAsync(url + "api/SearchWords/");
            Thread.Sleep(5000);
            if (response.IsSuccessStatusCode)
            {
                List<SearchWords> searchWords = await response.Content.ReadAsAsync<List<SearchWords>>();

                if (searchWords == null)
                {
                    return NotFound();
                }

                monitorLog.EndTime = DateTime.Now;

                var logResult = await Client.PostAsJsonAsync(url + "api/monitorLogs/", monitorLog);

                searchwordLog.MonitorLog = await logResult.Content.ReadAsAsync<MonitorLog>();
                searchwordLog.SearchWords = searchWords;

                return Ok(searchwordLog);
            }
            return BadRequest();
        }

        // GET: api/SearchWords/5
        [ResponseType(typeof(SearchWords))]
        public async Task<IHttpActionResult> GetSearchWords(int id)
        {
            var url = Helper.GetNextUrl();

            HttpResponseMessage response = await Client.GetAsync(url + "api/SearchWords/" + id);

            if (response.IsSuccessStatusCode)
            {
                SearchWords searchWords = await response.Content.ReadAsAsync<SearchWords>();

                if (searchWords == null)
                {
                    return NotFound();
                }
                return Ok(searchWords);
            }
            return BadRequest();
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

            var url = Helper.GetNextUrl();

            HttpResponseMessage response = Client.PutAsJsonAsync(url + "api/searchWords/" + id, searchWords).Result;
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsAsync<SearchWords>());
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

            var url = Helper.GetNextUrl();

            HttpResponseMessage response = Client.PostAsJsonAsync(url + "api/searchWords/", searchWords).Result;
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsAsync<SearchWords>());
            }

            return BadRequest("haha");
        }

        // DELETE: api/SearchWords/5
        [ResponseType(typeof(SearchWords))]
        public async Task<IHttpActionResult> DeleteSearchWords(int id)
        {
            var url = Helper.GetNextUrl();

            HttpResponseMessage response = Client.DeleteAsync(url + "api/searchWords/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsAsync<SearchWords>());
            }

            return BadRequest("hehe");
        }
    }
}
