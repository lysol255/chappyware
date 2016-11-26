using Chappyware.Business;
using Chappyware.Data;
using Chappyware.Data.Storage;
using Chappyware.Web.Models;
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
        private const string _UpdateStatsRoute = "~/stats/update";
        private const string _GetStatsRoute = "~/stats";
        private const string _GetLeagueRoute = "~/league";

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Route(_GetLeagueRoute, Name = "league")]
        [HttpGet]
        public ActionResult GetLeague()
        {
            FantasyTeamManager manager = new FantasyTeamManager();
            FantasyLeague league = manager.CreateLeague("Robs");
            manager.UpdateLeagueRoster(league, DataFileUtilities.GetLeagueFileName());

            LeagueModel leagueModel = new LeagueModel(league);

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(leagueModel);
            return result;
        }


        [Route(_GetTeamRoute, Name ="teams")]
        [HttpGet]
        public ActionResult GetTeams()
        {
            FantasyTeamManager manager = new FantasyTeamManager();
            FantasyLeague league = manager.CreateLeague("Robs");
            manager.UpdateLeagueRoster(league, DataFileUtilities.GetLeagueFileName());

            //List<TeamModel> teams = ConvertToModelObjects(league.Teams);
            
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(null);
            return result;
        }

        //[Route(_GetStatsRoute, Name = "stats")]
        //[HttpGet]
        //public ActionResult GetStats()
        //{
        //    StatisticManager statManager = new StatisticManager();
        //    List<Player> currentPlayers = statManager.GetPlayerStatistics();

        //    List<TeamModel> teams = ConvertToPlayerStatsModels(currentPlayers);

        //    JsonResult result = new JsonResult();
        //    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    result.Data = JsonConvert.SerializeObject(teams);
        //    return result;
        //}

        //private List<HistoricalStatisticModel> ConvertToPlayerStatsModels(List<Player> players)
        //{
        //    foreach(Player player in players)
        //    {

        //    }
        //}      

        

        [Route(_UpdateStatsRoute)]
        [HttpGet]
        public ActionResult UpdateTeams()
        {
            // get the current stats and load them into memory
            IStatSource csvSource = new HockeyReferenceDotComStatSource();
            csvSource.Initialize();

            List<Player> currentPlayerStats = StorageFactory.Instance.LoadPersistedStatSource();
            StatisticManager.UpdatePlayerStatistics(currentPlayerStats, csvSource);

            // persist them into json
            StorageFactory.Instance.UpdatedPersistedStatSource(currentPlayerStats);

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(currentPlayerStats);
            return result;
        }
    }
}