using MyDictionary.Core.Domain;
using MyDictionary.Core.Domain.Interfaces;
using MyDictionary.Infrastructure.ApiModels;
using MyDictionary.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.Infrastructure.Repositories
{
    public class MerriamWebsterDictionaryRepository : IWordRepository
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

        public IEnumerable<IWord> GetWord(string word)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException(nameof(word));
            }



            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                using (var result = client.GetAsync($"{_uri}{word}?key={_key}"))
                {
                    HttpResponseMessage response = result.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //var wordLookup = JsonConvert.DeserializeObject<Word>(response.Content.ReadAsStringAsync().Result);


                        //var objData = (List<JObject>)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result); // Deserialize json data
                        string strResponse = response.Content.ReadAsStringAsync().Result;
                        var temp = JsonConvert.DeserializeObject(strResponse);
                
                        var objData = JsonConvert.DeserializeObject<List<MyArray>>(response.Content.ReadAsStringAsync().Result); // Deserialize json data
                        //((Newtonsoft.Json.Linq.JArray)temp).ToList().Select(w => w.Meta)

                        var apiWordInfo = objData.Select(w =>
                            new ApiWord
                            {
                                Id = (w.Meta.Id.IndexOf(":") > 0 ? w.Meta.Id.Substring(0, w.Meta.Id.IndexOf(":")) : w.Meta.Id),
                                Syllables = w.Hwi.Hw,
                                PartOfSpeech = w.Fl,
                                Pronunciations = w.Hwi.Prs?.Select(p => new Pronunciation { Text = p.Mw }),
                                Definitions = w.Shortdef
                            }).Where(w => w.Id == word).ToList();


                        //apiWord.WordDetails = objData.Select(d => new WordDetails { Definition = string.Join(",", d.Shortdef) }).ToList();
                        ////apiWord.Spelling = objData[0].Value<string>("word");
                        //apiWord.WordDetails = objData[0].Value<JArray>("results")?.ToObject<List<WordDetails>>();
                        //apiWord.Syllables = objData[0].Value<JToken>("syllables")?.ToObject<Syllables>();
                        //apiWord.Pronunciation = objData.Value<JToken>("pronunciation")?.ToObject<Pronunciation>();
                        return apiWordInfo;
                    }

                    throw new System.Exception(response.ReasonPhrase);
                }
                
            }
        //    using (var client = new HttpClient())
        //    {
        //        //client.DefaultRequestHeaders.Accept.Clear();
        //        //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        using (var response = client.GetAsync($"{_uri}{word}?key={_key}"))
        //        {
                   
        //            response.Wait();

        //            var result = response.Result;
        //            if (result.IsSuccessStatusCode)
        //            {
        //                //var wordLookup = JsonConvert.DeserializeObject<Word>(response.Content.ReadAsStringAsync().Result);

        //                //var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
        //                //readTask.Wait();

        //                //students = readTask.Result;

        //                var objData = JsonConvert.DeserializeObject<List<object>>(result.Content.ReadAsStringAsync().Result); // Deserialize json data

        //                var apiWord = new ApiWord();
        //                //apiWord.Spelling = objData.Value<string>("word");
        //                //apiWord.WordDetails = objData.Value<JArray>("results")?.ToObject<List<WordDetails>>();
        //                //apiWord.Syllables = objData.Value<JToken>("syllables")?.ToObject<Syllables>();
        //                //apiWord.Pronunciation = objData.Value<JToken>("pronunciation")?.ToObject<Pronunciation>();
        //                return apiWord;

        //            }
                    
        //            //throw new System.Exception(response.Status);
                    

        //        }
                
        //    }

        //    return null;

        }

        private string Decode(string pronunciation)
        {
            try
            {

                // Create two different encodings.
                Encoding ascii = Encoding.ASCII;
                Encoding unicode = Encoding.UTF32;

                // Convert the string into a byte array.
                byte[] unicodeBytes = unicode.GetBytes(pronunciation);

                // Perform the conversion from one encoding to the other.
                byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

                // Convert the new byte[] into a char[] and then into a string.
                char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                return new string(asciiChars);

            }
            catch (Exception ex)
            {

                
            }


            return "";
        }
    }
}
