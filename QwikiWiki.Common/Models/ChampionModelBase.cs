using System;
using System.ComponentModel;

namespace QwikiWiki.Common.Models
{
    public abstract class ChampionModelBase
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
