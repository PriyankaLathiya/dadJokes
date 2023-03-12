using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DadJokesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DadJokesController : ControllerBase
    {
        private HttpClient _client;
        private const string APIKEY = "X-RapidAPI-Key", APIValue = "a4790af60cmshc5bd233302d5975p10e423jsn671070f7b75f";

        public DadJokesController(HttpClient client)
        {
            _client = client;
        }
            
        [HttpGet]
        [Route("randomjoke/{count:int?}")]
        public async Task<string> RandomJoke(int? count = 1)
        {
            string APIURL = $"https://dad-jokes.p.rapidapi.com/random/joke?count={count}";
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(APIURL);
            request.Method = HttpMethod.Get;
            request.Headers.Add(APIKEY, APIValue);

            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(result))
                    return "Success";
                else
                    return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        [HttpGet]
        [Route("jokecount")]
        public async Task<string> JokeCount()
            {
            string APIURL = $"https://dad-jokes.p.rapidapi.com/joke/count";
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(APIURL);
            request.Method = HttpMethod.Get;
            request.Headers.Add(APIKEY, APIValue);
            
            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(result))
                    return "Success";
                else
                    return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}

