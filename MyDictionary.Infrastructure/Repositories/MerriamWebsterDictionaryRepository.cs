using MyDictionary.Core.Domain;
using MyDictionary.Core.Domain.Interfaces;
using MyDictionary.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyDictionary.Infrastructure.Repositories
{
    public class MerriamWebsterDictionaryRepository : IApiWordRepository
    {
        private string _uri;
        private string _key;

        public MerriamWebsterDictionaryRepository(string uri, string key)
        {
            _uri = uri;
            _key = key;
        }

        public Task<string> GetSearchResult(string searchString)
        {
            throw new NotImplementedException();
        }

        public IApiWordResult GetWord(string word)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException(nameof(word));
            }

            using (var client = new HttpClient())
            {
                using (var result = client.GetAsync($"{_uri}{word}?key={_key}"))
                {
                    HttpResponseMessage response = result.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string strResponse = response.Content.ReadAsStringAsync().Result;
                        try
                        {
                            var objData = JsonConvert.DeserializeObject<List<RootObject>>(strResponse);

                            var wordApiResult = new ApiWordResult
                            {
                                Results = objData.Select(w =>
                                    new ApiWord
                                    {
                                        Id = w.Meta.Id,
                                        Syllables = w.Hwi.Hw,
                                        PartOfSpeech = w.Fl,
                                        Pronunciations = w.Hwi.Prs?.Select(p => p?.Mw),
                                        Definitions = w.ShortDef
                                    }).Where(w => w.Id.ToLower() == word.ToLower()).ToList()
                            };

                            return wordApiResult;
                        }
                        catch (JsonSerializationException ex)
                        {

                            var objData = JsonConvert.DeserializeObject<List<string>>(strResponse);
                            var wordApiResult = new ApiWordResult
                            {
                                NotFound = true,
                                Results = objData.Select(w =>
                                    new ApiWord
                                    {
                                        Id = w
                                    })
                            };

                            return wordApiResult;
                        }
                        
                    }

                    return new ApiWordResult { Error = response.ReasonPhrase };
                }
                
            }

        }
    }
}
