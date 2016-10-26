using Chappyware.Business;
using Chappyware.Data;
using Chappyware.Data.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chappyware.Web.Controllers
{
    public class HomeController : Controller
    {

        private const string _GetTeamRoute = "~/teams";
        private const string _UpdateTeamRoute = "~/teams/update";

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Route(_GetTeamRoute, Name ="teams")]
        public ActionResult GetTeams()
        {
            FantasyTeamManager manager = new FantasyTeamManager();
            FantasyLeague league = manager.CreateLeague("Robs");
            manager.UpdateLeagueRoster(league, "TeamImport.txt");
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(league.Teams);
            return result;
        }

        [Route(_UpdateTeamRoute)]
        public ActionResult UpdateTeams()
        {
            // get the current stats and load them into memory
            IStatSource csvSource = new HockeyReferenceDotComStatSource();
            csvSource.Initialize();

            List<Player> currentPlayerStats = StorageFactory.Instance.LoadPersistedStatSource();
            StatisticManager.UpdatePlayerStatistics(currentPlayerStats, csvSource);

            // persist them into json
            StorageFactory.Instance.UpdatedPersistedStatSource(currentPlayerStats);

            return View();
        }
    }
}