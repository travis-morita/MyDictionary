using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyDictionary.Infrastructure.ApiModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Meta
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("uuid")]
        public string Uuid;

        [JsonProperty("sort")]
        public string Sort;

        [JsonProperty("src")]
        public string Src;

        [JsonProperty("section")]
        public string Section;

        [JsonProperty("stems")]
        public List<string> Stems;

        [JsonProperty("offensive")]
        public bool Offensive;
    }

    public class Sound
    {
        [JsonProperty("audio")]
        public string Audio;

        [JsonProperty("ref")]
        public string Ref;

        [JsonProperty("stat")]
        public string Stat;
    }

    public class Pr
    {
        [JsonProperty("mw")]
        public string Mw;

        [JsonProperty("sound")]
        public Sound Sound;
    }

    public class Hwi
    {
        [JsonProperty("hw")]
        public string Hw;

        [JsonProperty("prs")]
        public List<Pr> Prs;
    }

    public class Def
    {
        [JsonProperty("sseq")]
        public List<List<List<object>>> Sseq;
    }

    public class Sound2
    {
        [JsonProperty("audio")]
        public string Audio;

        [JsonProperty("ref")]
        public string Ref;

        [JsonProperty("stat")]
        public string Stat;
    }

    public class Pr2
    {
        [JsonProperty("mw")]
        public string Mw;

        [JsonProperty("sound")]
        public Sound2 Sound;
    }

    public class Uro
    {
        [JsonProperty("ure")]
        public string Ure;

        [JsonProperty("prs")]
        public List<Pr2> Prs;

        [JsonProperty("fl")]
        public string Fl;
    }

    public class Syn
    {
        [JsonProperty("pl")]
        public string Pl;

        [JsonProperty("pt")]
        public List<List<object>> Pt;
    }

    public class Table
    {
        [JsonProperty("tableid")]
        public string Tableid;

        [JsonProperty("displayname")]
        public string Displayname;
    }

    public class MyArray
    {
        [JsonProperty("meta")]
        public Meta Meta;

        [JsonProperty("hom")]
        public int Hom;

        [JsonProperty("hwi")]
        public Hwi Hwi;

        [JsonProperty("fl")]
        public string Fl;

        [JsonProperty("def")]
        public List<Def> Def;

        [JsonProperty("uros")]
        public List<Uro> Uros;

        [JsonProperty("syns")]
        public List<Syn> Syns;

        [JsonProperty("table")]
        public Table Table;

        [JsonProperty("et")]
        public List<List<string>> Et;

        [JsonProperty("date")]
        public string Date;

        [JsonProperty("shortdef")]
        public List<string> Shortdef;
    }

    public class Root
    {
        public List<MyArray> MyArray;
    }


}
