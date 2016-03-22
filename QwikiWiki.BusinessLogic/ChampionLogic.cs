using System.Collections.Generic;
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

        public List<SimpleChampionModel> GetSimpleChampionModels()
        {
            List<SimpleChampionModel> simpleChampionModels = DataLayerProvider.GetDataAccess().GetSimpleChampions();
            return simpleChampionModels; 
        }

        public ChampionModel GetChampion(int championId)
        {
            ChampionLocalDto championDto = new DataAccess().GetChampion(championId);
            return GenerateChampionModel(championDto);
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
