using System.Data;
using QwikiWiki.Common.Dtos;
using QwikiWiki.Common.LocalDtos;

namespace QwikiWiki.Common.Models
{
    public class ChampionModelBuilder
    {
        public ChampionModel Build(ChampionDto championDto,string version)
        {
            ChampionModel model = new ChampionModel();
            model.Id = int.Parse(championDto.id);
            model.Name = championDto.name;
            model.ImgUrl = "//ddragon.leagueoflegends.com/cdn/" + version + "/img/champion/" + championDto.image.full;
            model.Title = championDto.title;
            model.PrimaryRole = championDto.tags[0];

            if(championDto.tags.Length > 1){
                model.SecondaryRole = championDto.tags[1];

                //Damn it riot Alistar is suppppppppppport is he?
                if (model.SecondaryRole.ToLower() == "suppport")
                {
                    model.SecondaryRole = "Support";
                }

            }

            model.Info = championDto.info;

            SpellModelBuilder spellBuilder = new SpellModelBuilder();
            model.Passive = spellBuilder.BuildFromPassiveDto(championDto.passive);
            model.QSpell = spellBuilder.BuildFromSpellDto(championDto.spells[0]);
            model.WSpell = spellBuilder.BuildFromSpellDto(championDto.spells[1]);
            model.ESpell = spellBuilder.BuildFromSpellDto(championDto.spells[2]);
            model.RSpell = spellBuilder.BuildFromSpellDto(championDto.spells[3]);

            return model;
        }

        public ChampionModel Build(ChampionLocalDto championLocalDto, SpellModel[] spellModels, RoleLocalDto[] roleLocalDtos)
        {
            ChampionModel result = new ChampionModel();
            result.Id = championLocalDto.Id;
            result.Name = championLocalDto.Name;
            result.ImgUrl = championLocalDto.ImgUrl;
            result.Title = championLocalDto.Title;

            result.PrimaryRole = roleLocalDtos[0].Role;
            if (roleLocalDtos[1] != null)
                result.SecondaryRole = roleLocalDtos[1].Role;

            result.Info = new InfoDto
            {
                Attack = championLocalDto.Attack,
                Defense = championLocalDto.Defense,
                Difficulty = championLocalDto.Difficulty,
                Magic = championLocalDto.Magic
            };
            
            result.Passive = spellModels[0];
            result.QSpell = spellModels[1];
            result.WSpell = spellModels[2];
            result.ESpell = spellModels[3];
            result.RSpell = spellModels[4];

            return result;
        }
    }
}
