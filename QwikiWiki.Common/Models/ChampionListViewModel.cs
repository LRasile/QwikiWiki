using System.Collections.Generic;

namespace QwikiWiki.Common.Models
{
    public class ChampionListViewModel
    {
        //would be nice to configure details depending on patch
        //public List<string> PatchList { get; set; }
        public List<ChampionModel> ChampionList { get; set; }

        public string Version { get; set; }
    }
}