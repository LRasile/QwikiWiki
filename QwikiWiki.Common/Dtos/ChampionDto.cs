using QwikiWiki.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QwikiWiki.Common.Dtos
{
    public class ChampionDto
    {
        public string[] tags { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public InfoDto info { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public ImageDto image { get; set; }
        public StatsDto stats { get; set; }
        public SpellDto[] spells { get; set; }
        public PassiveDto passive { get; set; }
    }

}
