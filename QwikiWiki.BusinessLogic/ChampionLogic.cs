using System.Collections.Generic;
using System.Linq;
using QwikiWiki.Common.LocalDtos;
using QwikiWiki.Common.Models;
using QwikiWiki.DataLayer;

namespace QwikiWiki.BusinessLogic
{
    public class ChampionLogic
    {
        public string GetVersionNumber()
        {
            return DataLayerProvider.GetDataAccess().GetVersionNumber();
        }

        public List<ChampionModel> GetChampionModels()
        {
            List<ChampionLocalDto> championLocalDtos = DataLayerProvider.GetDataAccess().GetChampions();

            List<ChampionModel> result = championLocalDtos.Select(GenerateChampionModel).ToList();

            return result; 
        }

        private ChampionModel GenerateChampionModel(ChampionLocalDto championLocalDto)
        {
            SpellModel[] spellModel = DataLayerProvider.GetDataAccess().GetSpells(championLocalDto);
            RoleLocalDto[] roleLocalDtos = DataLayerProvider.GetDataAccess().GetRoles(championLocalDto.PrimaryRole, championLocalDto.SecondaryRole);

            ChampionModel championModel = new ChampionModelBuilder().Build(championLocalDto, spellModel, roleLocalDtos);

            return championModel;
        }
    }
}
