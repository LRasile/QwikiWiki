using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using QwikiWiki.Common.Dtos;
using QwikiWiki.Common.Models;
using QwikiWiki.DataLayer;

namespace QwikiWiki.BusinessLogic
{
    public class DataGatheringLogic
    {

        //private string apiKey = "ef5229a0-4b4a-459c-be40-7c393edbf64c";
        //= "https://global.api.pvp.net/api/lol/static-data/euw/v1.2/champion?champData=image,info,passive,spells,stats,tags&api_key=ef5229a0-4b4a-459c-be40-7c393edbf64c";
        private readonly string _urlForChampionData;

        public DataGatheringLogic(string apiKey)
        {
            _urlForChampionData = "https://global.api.pvp.net/api/lol/static-data/euw/v1.2/champion?champData=image,info,passive,spells,stats,tags&api_key=" + apiKey;
        }

        public string GetData()
        {
            ChampionData championData = GetChampionData();
            string version = championData.version;

            List<ChampionDto> championDtoList = GetChampionList(championData);
            List<ChampionModel> championModelList = ConvertChampionDtoListToChampionList(championDtoList, version);

            string result = SaveChampions(championModelList);
            DataLayerProvider.GetDataAccess().SaveVersion(version);

            return result;
        }

        private string SaveChampions(List<ChampionModel> championModelList)
        {
            string result = string.Empty;

            foreach (ChampionModel championModel in championModelList)
            {
                if (!DataLayerProvider.GetDataAccess().SaveChampion(championModel))
                {
                    result = string.Concat(result, ",", championModel.Name);
                }
            }
            return result;
        }

        private List<ChampionDto> GetChampionList(ChampionData championData)
        {
            string championListString = championData.data.ToString();
            string[] championStrings = championListString.Split(new string[] { "\"id\"" }, StringSplitOptions.RemoveEmptyEntries);

            List<ChampionDto> championDtoList = CreateChampionDtoList(championStrings);

            return championDtoList;
        }

        private List<ChampionModel> ConvertChampionDtoListToChampionList(List<ChampionDto> championDtoList, string version)
        {
            ChampionModelBuilder builder = new ChampionModelBuilder();
            List<ChampionModel> modelList = championDtoList.Select(championDto => builder.Build(championDto, version)).ToList();

            return modelList;
        }

        private ChampionData GetChampionData()
        {
            string dataResult;

            try
            {
                dataResult = GetWebClientData(_urlForChampionData);
            }
            catch (WebException)
            {
                //work around if we are behind a proxy or the api is down
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("QwikiWiki.BusinessLogic.DLL", "") + "DataResult.txt";
                path = path.Replace("file:///", "");
                dataResult = System.IO.File.ReadAllText(path);
            }

            return JsonSerialiser.FromJson<ChampionData>(dataResult);

        }

        private static List<ChampionDto> CreateChampionDtoList(string[] championStrings)
        {
            List<ChampionDto> championDtoList = new List<ChampionDto>();
            bool isFirst = true;

            foreach (string championString in championStrings)
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }
                string fullChampionJsonString = "{\"id\"" + championString;

                if (championString != championStrings[championStrings.Length - 1])
                {
                    int index = fullChampionJsonString.LastIndexOf(',');
                    fullChampionJsonString = fullChampionJsonString.Substring(0, index);
                }
                else
                {
                    fullChampionJsonString = fullChampionJsonString.Substring(0, fullChampionJsonString.Length - 1);
                }

                ChampionDto championDto = JsonSerialiser.FromJson<ChampionDto>(fullChampionJsonString);
                championDtoList.Add(championDto);
            }
            return championDtoList;
        }

        private string GetWebClientData(string completeUrl)
        {
            string dataResult;
            using (WebClient client = new WebClient())
            {
                client.UseDefaultCredentials = true;
                client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                dataResult = client.DownloadString(completeUrl);
            }
            return dataResult;
        }
    }
}
