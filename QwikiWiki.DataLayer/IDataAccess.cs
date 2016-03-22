using System.Collections.Generic;
using QwikiWiki.Common.LocalDtos;
using QwikiWiki.Common.Models;

namespace QwikiWiki.DataLayer
{
    public interface IDataAccess
    {
        List<SimpleChampionModel> GetSimpleChampions();

        SpellModel[] GetSpells(ChampionLocalDto championLocalDto);

        RoleLocalDto[] GetRoles(int primaryRole, int secondaryRole);

        bool SaveChampion(ChampionModel championModel);

        string SaveVersion(string version);

        string GetVersionNumber();

    }
}
