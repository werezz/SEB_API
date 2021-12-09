using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SEB_API
{
    public class DogFactClient
    {
        private readonly HttpClient _client;

        public DogFactClient(HttpClient client)
        {
            string _ContentType = "application/json";

            _client = client;
            _client.Timeout = new TimeSpan(0, 0, 60);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
        }

        public async Task<DogFact> GetDogFactsFromClientAsync(int factNumber)
        {
            List<DogFact> dogFactList = new List<DogFact>();


            if (factNumber <= 0)
            {
                return new DogFact { fact = "" };
            }

            using var response = await _client.GetAsync($"https://dog-facts-api.herokuapp.com/api/v1/resources/dogs?number={ factNumber }", HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                dogFactList = await response.Content.ReadAsAsync<List<DogFact>>();

                if (dogFactList.Count >= 1)
                {
                    return dogFactList[factNumber - 1];
                }
                else
                {
                    return dogFactList[factNumber];
                }
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}