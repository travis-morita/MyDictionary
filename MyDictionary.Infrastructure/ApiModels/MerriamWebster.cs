using System.Collections.Generic;

namespace MyDictionary.Infrastructure
{
    public class Meta
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = (value.IndexOf(":") > 0 ? value.Substring(0, value.IndexOf(":")) : value); }
        }
        public string Uuid { get; set; }
        public string Sort { get; set; }
        public string Src { get; set; }
        public string Section { get; set; }
        public List<string> Stems { get; set; }
        public bool Offensive { get; set; }
    }

    public class Sound
    {
        public string Audio { get; set; }
        public string @ref { get; set; }
        public string Stat { get; set; }
    }

    public class Pr
    {
        public string Mw { get; set; }
        public Sound Sound { get; set; }
    }

    public class Hwi
    {
        public string Hw { get; set; }
        public List<Pr> Prs { get; set; }
    }

    public class In
    {
        public string @if { get; set; }
        public List<Pr> Prs { get; set; }
    }

    public class Def
    {
        public string Vd { get; set; }
        public List<List<List<object>>> Sseq { get; set; }
    }

    public class Dro
    {
        public string Drp { get; set; }
        public List<Def> Def { get; set; }
    }

    public class Uro
    {
        public string Ure { get; set; }
        public List<Pr> Prs { get; set; }
        public string Fl { get; set; }
    }

    public class Art
    {
        public string Artid { get; set; }
        public string Capt { get; set; }
    }

    public class RootObject
    {
        public Meta Meta { get; set; }
        public int Hom { get; set; }
        public Hwi Hwi { get; set; }
        public string Fl { get; set; }
        public List<In> Ins { get; set; }
        public List<Def> Def { get; set; }
        public List<Dro> Dros { get; set; }
        public List<List<object>> Et { get; set; }
        public string Date { get; set; }
        public List<string> ShortDef { get; set; }
        public List<Uro> Uros { get; set; }
        public Art Art { get; set; }
    }


}
