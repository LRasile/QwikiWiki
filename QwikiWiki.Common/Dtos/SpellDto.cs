using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QwikiWiki.Common.Dtos
{
    public class SpellDto
    {
        public string name{get;set;}
        public string sanitizedTooltip { get; set; }
        public ImageDto image { get; set; }
        public string costType { get; set; }
        public string costBurn { get; set; }
        public string cooldownBurn { get; set; }
        public string[] effectBurn { get; set; }
        public Variables[] vars { get; set; }
        public string rangeBurn { get; set; }
        //public LevelTip leveltip{get;set;}//": {"effect": ["{{ e1 }} -> {{ e1NL }}","{{ cooldown }} -> {{ cooldownnNL }}"],"label": ["Bonus Damage","Cooldown"]},
        //public int[] cost { get; set; }
        //public int[,] effect { get; set; }
        //public int[] cooldown { get; set; }
        //public int[] range { get; set; }
        public string key{get;set;}
        public string description { get; set; }
        public string sanitizedDescription { get; set; }
        public string tooltip { get; set; }
        public string resource { get; set; }
        public int maxrank { get; set; }

    }
    public class LevelTip
    {
        public string[] label { get; set; }
        public string[] effect { get; set; }
    }
}
