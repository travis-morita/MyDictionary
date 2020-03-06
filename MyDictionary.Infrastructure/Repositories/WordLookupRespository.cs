using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using Newtonsoft.Json;
//using RestSharp;
//using RestSharp.Authenticators;


namespace MyDictionary.Infrastructure.Respositories
{
    public class WordLookupRespository : IWordLookupRepository
    {
        private string _uri;

        public WordLookupRespository(string uri)
        {
            _uri = "https://wordsapiv1.p.rapidapi.com/words/";
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
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "wordsapiv1.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("x-rapidapi-key", "433ee81754mshd7eaa03edec82d5p1609e6jsn5b1a086d7b86");

            HttpResponseMessage response= await client.GetAsync($"{_uri}?letterPattern=^{searchString}.*");

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
                //var searchResults = JsonConvert.DeserializeObject<SearchResults>(response.Content.ReadAsStringAsync().Result);

                //return searchResults;
            }


            throw new System.Exception(response.ReasonPhrase);
        }

        public async Task<Word> GetWord(string word)
        {
            if (word is null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "wordsapiv1.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("x-rapidapi-key", "433ee81754mshd7eaa03edec82d5p1609e6jsn5b1a086d7b86");

            HttpResponseMessage response = await client.GetAsync($"{_uri}{word}");

            if (response.IsSuccessStatusCode)
            {
                var wordLookup = JsonConvert.DeserializeObject<Word>(response.Content.ReadAsStringAsync().Result);

                return wordLookup;
            }


            throw new System.Exception(response.ReasonPhrase);
        }


    }
}
