using Microsoft.AspNetCore.Mvc;
using moduit.test.be.Models;
using System.Net.Http;

namespace moduit.test.be.Controllers
{
    public class ExamController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger<ExamController> _logger;

        public ExamController(ILogger<ExamController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [Route("QuestionOne")]
        [HttpGet]
        public async Task<QuestionOne> QuestionOne()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "backend/question/one");
            var client = _clientFactory.CreateClient("moduitApi");

            var response = await client.SendAsync(request);

            QuestionOne resp = null;
            if (response.IsSuccessStatusCode)
            {
                resp = await response.Content.ReadFromJsonAsync<QuestionOne>();
            }

            return resp;
        }

        [Route("QuestionTwo")]
        [HttpGet]
        public async Task<IEnumerable<QuestionTwo>> QuestionTwo()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "backend/question/two");
            var client = _clientFactory.CreateClient("moduitApi");

            var response = await client.SendAsync(request);

            IEnumerable<QuestionTwo> resp = null;
            if (response.IsSuccessStatusCode)
            {
                resp = await response.Content.ReadFromJsonAsync<IEnumerable<QuestionTwo>>();

                resp = resp
                    .Where(m => m.description.Contains("Ergonomic") || m.title.Contains("Ergonomic"))
                    .Where(m => m.tags != null && m.tags.Contains("Sports"))
                    .OrderByDescending(m => m.id)
                    .Take(3);
            }

            return resp;
        }

        [Route("QuestionThree")]
        [HttpGet]
        public async Task<IEnumerable<QuestionThreeResult>> QuestionThree()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "backend/question/three");
            var client = _clientFactory.CreateClient("moduitApi");

            var response = await client.SendAsync(request);

            IEnumerable<QuestionThreeResult> resp = null;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<QuestionThreeGet> data = null;
                data = await response.Content.ReadFromJsonAsync<IEnumerable<QuestionThreeGet>>();

                resp = new List<QuestionThreeResult>();
                foreach (var d in data)
                {
                    var parseObj = new QuestionThreeResult()
                    {
                        id = d.id,
                        category = d.category,
                        createdAt = d.createdAt,
                        tags = d.tags,
                    };

                    // if items exists then mapped the items with header info
                    if (d.items != null)
                    {
                        var listObjItems = d.items.Select(m => new QuestionThreeResult()
                        {
                            id = parseObj.id,
                            category = parseObj.category,
                            createdAt = parseObj.createdAt,
                            tags = parseObj.tags,
                            title = m.title,
                            description = m.description,
                            footer = m.footer
                        });

                        resp = resp.Concat(listObjItems);
                    }
                    else
                    {
                        resp = resp.Append(parseObj);
                    }
                }
            }

            return resp;
        }
    }
}