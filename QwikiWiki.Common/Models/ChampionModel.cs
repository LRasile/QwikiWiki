using System;
using System.ComponentModel;
using System.Collections.Generic;
using QwikiWiki.Common.Dtos;

namespace QwikiWiki.Common.Models
{
    public class ChampionModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("PrimaryRole")]
        public string PrimaryRole { get; set; }
        [DisplayName("SecondaryRole")]
        public string SecondaryRole { get; set; }
        [DisplayName("ImgUrl")]
        public string ImgUrl { get; set; }
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

        public string RoleText
        {
            get
            {
                string roleText = PrimaryRole + "," + SecondaryRole;
                return roleText;
            }
        }

        public string RoleTextAbbr
        {
            get
            {
                string roleTextAbbr = PrimaryRole + "," + SecondaryRole;
                if (roleTextAbbr.ToLower().IndexOf("marksman", StringComparison.Ordinal) > -1)
                {
                    roleTextAbbr = roleTextAbbr.Replace("Marksman", "ADC");
                }
                if (roleTextAbbr.ToLower().IndexOf("mage", StringComparison.Ordinal) > -1)
                {
                    roleTextAbbr = roleTextAbbr.Replace("Mage", "APC");
                }
                return roleTextAbbr;
            }
        }
    }
}