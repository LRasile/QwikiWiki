using QwikiWiki.Common.Models;
using System.Web.Mvc;
using QwikiWiki.BusinessLogic;

namespace QwikiWiki.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ChampionList");
        }

        [HttpGet]
        public ActionResult ChampionList()
        {
            ChampionLogic logic = new ChampionLogic();

            ChampionListViewModel viewModel = new ChampionListViewModel()
            {
                Version = logic.GetVersionNumber(),
                ChampionList = logic.GetChampionModels()
            };

            return View("ChampionList", viewModel);
        }

        [HttpGet]
        public ActionResult ReloadChampionData()
        {
            return View("ReloadChampionData", new ReloadChampionDataViewModel());
        }

        [HttpPost]
        public ActionResult ReloadChampionData(ReloadChampionDataViewModel viewModel)
        {
            //if (viewModel.Username == "rasile.leonardo@gmail.com" && viewModel.Password == "reloadMyData")
            //{
            //     ef5229a0-4b4a-459c-be40-7c393edbf64c

            DataGatheringLogic logic = new DataGatheringLogic(viewModel.ApiKey);
            string failedChampions = logic.GetData();

            string message = "Data loaded successfully.";
            viewModel = new ReloadChampionDataViewModel();
            if (!string.IsNullOrEmpty(failedChampions))
            {
                message = failedChampions;
            }
            viewModel.Message = message;
            
            //}
            //else
            //{
            //    viewModel = new ReloadChampionDataViewModel {Message = "Failed to load the data."};
            //}

            return View("ReloadChampionData", viewModel);
        }

    }
}
