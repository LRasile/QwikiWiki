using System.ComponentModel;
using QwikiWiki.Common.Dtos;

namespace QwikiWiki.Common.Models
{
    public class ChampionModel : ChampionModelBase
    {
        [DisplayName("Price")]
        public string Price { get; set; }
        [DisplayName("IsFreeToPlay")]
        public bool IsFreeToPlay { get; set; }
        [DisplayName("Passive")]
        public SpellModel Passive { get; set; }
        [DisplayName("QSpell")]
        public SpellModel QSpell { get; set; }
        [DisplayName("WSpell")]
        public SpellModel WSpell { get; set; }
        [DisplayName("ESpell")]
        public SpellModel ESpell { get; set; }
        [DisplayName("RSpell")]
        public SpellModel RSpell { get; set; }

        public InfoDto Info { get; set; }
        
    }
}