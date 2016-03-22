using System.Web.Mvc;
using QwikiWiki.BusinessLogic;
using QwikiWiki.Common.Models;

namespace QwikiWiki.Controllers
{
    public class ChampionController : Controller
    {
        //
        // GET: /Champion/

        public ActionResult Index(int championId)
        {
            ChampionModel championViewModel = new ChampionLogic().GetChampion(championId);
            return View("ChampionView", championViewModel);
        }

    }
}
