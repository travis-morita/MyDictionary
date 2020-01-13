using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyDictionary.Core.Domain
{
    public class SearchResults
    {
        public Query Query { get; set; }
        public Results Results { get; set; }
    }

    public class Query
    {

        [JsonProperty("limit")]
        public int limit { get; set; }

        [JsonProperty("page")]
        public int page { get; set; }
    }

    public class Results
    {

        [JsonProperty("total")]
        public int total { get; set; }

        [JsonProperty("data")]
        public IList<string> data { get; set; }
    }

    public class Example
    {

        [JsonProperty("query")]
        public Query query { get; set; }

        [JsonProperty("results")]
        public Results results { get; set; }
    }
}
