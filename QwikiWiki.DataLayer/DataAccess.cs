using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Qwikiwiki.DataLayer;
using QwikiWiki.Common.LocalDtos;
using QwikiWiki.Common.Models;

namespace QwikiWiki.DataLayer
{
    public class DataAccess : IDataAccess
    {
        //Public Methods
        public List<ChampionLocalDto> GetChampions()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "GetChampions",
                CommandType = CommandType.StoredProcedure
            };

            List<ChampionLocalDto> result = SQLHelper.GetEntityList<ChampionLocalDto>(ref command);

            return result;
        }

        public SpellModel[] GetSpells(ChampionLocalDto championLocalDto)
        {
            SpellModel passive = GetSpell(championLocalDto.Passive);
            SpellModel qSpell = GetSpell(championLocalDto.QSpell);
            SpellModel wSpell = GetSpell(championLocalDto.WSpell);
            SpellModel eSpell = GetSpell(championLocalDto.ESpell);
            SpellModel rSpell = GetSpell(championLocalDto.RSpell);

            return new [] {passive,qSpell,wSpell,eSpell,rSpell};
        }

        public RoleLocalDto[] GetRoles(int primaryRoleId, int secondaryRoleId)
        {
            RoleLocalDto primaryRole = GetRole(primaryRoleId);

            RoleLocalDto secondaryRole = null;
            if (secondaryRoleId > 0)
                secondaryRole = GetRole(secondaryRoleId);
            
            return new [] {primaryRole, secondaryRole};

        }

        public string GetVersionNumber()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "GetVersion",
                CommandType = CommandType.StoredProcedure
            };

            string result = string.Empty;
            object obj = SQLHelper.GetScalar<object>(ref command);
            if (obj != null)
                result = (string)obj;
            return result;
        }

        public string SaveVersion(string version)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "InsertVersion",
                CommandType = CommandType.StoredProcedure
            };
            string dateString = DateTime.Now.ToString("yyyyMMdd"); ;
            command.Parameters.AddWithValue("@DateUpdated", dateString);
            command.Parameters.AddWithValue("@Version", version);

            int rows = SQLHelper.ExecuteNonQuery(ref command);
            return rows.ToString();
        }

        public bool SaveChampion(ChampionModel championModel)
        {
            int primaryRoleId = GetRoleId(championModel.PrimaryRole);
            int secondaryRoleId = 0;
            if (!string.IsNullOrEmpty(championModel.SecondaryRole))
                secondaryRoleId = GetRoleId(championModel.SecondaryRole);

            int passiveId = SaveSpell(championModel.Passive);
            int qSpellId = SaveSpell(championModel.QSpell);
            int wSpellId = SaveSpell(championModel.WSpell);
            int eSpellId = SaveSpell(championModel.ESpell);
            int rSpellId = SaveSpell(championModel.RSpell);

            SqlCommand command = new SqlCommand
            {
                CommandText = "SaveChampion",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", championModel.Id);
            command.Parameters.AddWithValue("@Name", championModel.Name);
            command.Parameters.AddWithValue("@Title", championModel.Title);
            command.Parameters.AddWithValue("@PrimaryRole", primaryRoleId);
            if (secondaryRoleId > 0)
                command.Parameters.AddWithValue("@SecondaryRole", secondaryRoleId);
            command.Parameters.AddWithValue("@IsFreeToPlay", championModel.IsFreeToPlay);
            command.Parameters.AddWithValue("@Passive", passiveId);
            command.Parameters.AddWithValue("@QSpell", qSpellId);
            command.Parameters.AddWithValue("@WSpell", wSpellId);
            command.Parameters.AddWithValue("@ESpell", eSpellId);
            command.Parameters.AddWithValue("@RSpell", rSpellId);
            command.Parameters.AddWithValue("@ImgUrl", championModel.ImgUrl);
            command.Parameters.AddWithValue("@Price", championModel.Price);
            command.Parameters.AddWithValue("@Difficulty", championModel.Info.Difficulty);
            command.Parameters.AddWithValue("@Attack", championModel.Info.Attack);
            command.Parameters.AddWithValue("@Defense", championModel.Info.Defense);
            command.Parameters.AddWithValue("@Magic", championModel.Info.Magic);

            int rows = SQLHelper.ExecuteNonQuery(ref command);

            return (rows == 1);
        }

        //Private Methods

        private int SaveSpell(SpellModel spellModel)
        {
            int id = GetSpellId(spellModel.Name);

            if (id > 0)
                UpdateSpell(id, spellModel);
            else
                id = InsertSpell(spellModel);

            return id;
        }

        private void UpdateSpell(int id, SpellModel spellModel)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "UpdateSpell",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", spellModel.Name);
            command.Parameters.AddWithValue("@ImgUrl", spellModel.ImgUrl);
            command.Parameters.AddWithValue("@Description", spellModel.Description);
            command.Parameters.AddWithValue("@Cost", spellModel.Cost);
            command.Parameters.AddWithValue("@Cooldown", spellModel.Cooldown);
            command.Parameters.AddWithValue("@Range", spellModel.Range);
            command.Parameters.AddWithValue("@IsAoe", spellModel.IsAoe);
            command.Parameters.AddWithValue("@AimType", (int)spellModel.AimType);
            command.Parameters.AddWithValue("@CrowdControl", (int)spellModel.CrowdControl);
            command.Parameters.AddWithValue("@DamageType", (int)spellModel.DamageType);

            SQLHelper.ExecuteNonQuery(ref command);
        }

        private static int InsertSpell(SpellModel spellModel)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "InsertSpell",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Name", spellModel.Name);
            command.Parameters.AddWithValue("@ImgUrl", spellModel.ImgUrl);
            command.Parameters.AddWithValue("@Description", spellModel.Description);
            command.Parameters.AddWithValue("@Cost", spellModel.Cost);
            command.Parameters.AddWithValue("@Cooldown", spellModel.Cooldown);
            command.Parameters.AddWithValue("@Range", spellModel.Range);
            command.Parameters.AddWithValue("@IsAoe", spellModel.IsAoe);
            command.Parameters.AddWithValue("@AimType", (int)spellModel.AimType);
            command.Parameters.AddWithValue("@CrowdControl", (int)spellModel.CrowdControl);
            command.Parameters.AddWithValue("@DamageType", (int)spellModel.DamageType);

            int id = SQLHelper.GetScalar<int>(ref command);
            return id;
        }

        private SpellModel GetSpell(int id)
        {
            SqlCommand comm = new SqlCommand
            {
                CommandText = "GetSpell",
                CommandType = CommandType.StoredProcedure
            };
            comm.Parameters.AddWithValue("@id", id);

            SpellModel spellModel = SQLHelper.GetEntity<SpellModel>(ref comm);
            return spellModel;
        }

        private RoleLocalDto GetRole(int id)
        {
            SqlCommand comm = new SqlCommand
            {
                CommandText = "GetRole",
                CommandType = CommandType.StoredProcedure
            };
            comm.Parameters.AddWithValue("@id", id);

            RoleLocalDto roleLocalDto = SQLHelper.GetEntity<RoleLocalDto>(ref comm);
            return roleLocalDto;
        }

        private int GetSpellId(string name)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "GetSpellId",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@name", name);

            int result = 0;
            object obj = SQLHelper.GetScalar<object>(ref command);
            if (obj != null)
                result = (int)obj;
            return result;
        }

        private int GetRoleId(string role)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "GetRoleId",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Role", role);

            int result = 0;
            object obj = SQLHelper.GetScalar<object>(ref command);
            if (obj != null)
                result = (int)obj;
            return result;
        }

    }
}
