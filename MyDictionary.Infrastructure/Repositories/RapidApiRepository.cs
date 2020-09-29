using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MyDictionary.Core.Domain;
using MyDictionary.Core.Domain.Interfaces;
using MyDictionary.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyDictionary.Infrastructure.Respositories
{
    public class RapidApiRepository : IWordLookupRepository
    {
        private string _uri;
        private string _host;
        private string _key;

        public RapidApiRepository(string uri, string host, string key)
        {
            _uri = uri;
            _host = host;
            _key = key;
        }

        public async Task<string> GetSearchResult(string searchString)
        {
            if(searchString is null)
            {
                throw new ArgumentNullException(nameof(searchString));
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-rapidapi-host", _host);
            client.DefaultRequestHeaders.Add("x-rapidapi-key", _key);

            HttpResponseMessage response= await client.GetAsync($"{_uri}?letterPattern=^{searchString}.*");

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
                //var searchResults = JsonConvert.DeserializeObject<SearchResults>(response.Content.ReadAsStringAsync().Result);

                //return searchResults;
            }


            throw new System.Exception(response.ReasonPhrase);
        }

        public ApiWord GetWord(string word)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException(nameof(word));
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-rapidapi-host", _host);
                client.DefaultRequestHeaders.Add("x-rapidapi-key", _key);

                HttpResponseMessage response = client.GetAsync($"{_uri}{word}").Result;

                if (response.IsSuccessStatusCode)
                {
                    //var wordLookup = JsonConvert.DeserializeObject<Word>(response.Content.ReadAsStringAsync().Result);


                    var objData = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result); // Deserialize json data

                    var apiWord = new ApiWord();
                    apiWord.Spelling = objData.Value<string>("word");
                    apiWord.WordDetails = objData.Value<JArray>("results")?.ToObject<List<WordDetails>>();
                    apiWord.Syllables = objData.Value<JToken>("syllables")?.ToObject<Syllables>();
                    apiWord.Pronunciation = objData.Value<JToken>("pronunciation")?.ToObject<Pronunciation>();
                    return apiWord;
                }

                throw new System.Exception(response.ReasonPhrase);
            }
           
        }


    }
}
