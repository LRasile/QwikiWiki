namespace QwikiWiki.Common.LocalDtos
{
    public class ChampionLocalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int PrimaryRole { get; set; }
        public int SecondaryRole { get; set; }
        public int IsFreeToPlay { get; set; }
        public int Passive { get; set; }
        public int QSpell { get; set; }
        public int WSpell { get; set; }
        public int ESpell { get; set; }
        public int RSpell { get; set; }
        public string ImgUrl { get; set; }
        public string Price { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int Difficulty { get; set; }
        public int Attack { get; set; }
    }
}

