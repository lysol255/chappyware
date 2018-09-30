using Chappyware.Business;
using Chappyware.Data;
using Chappyware.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Chappyware.Web.Controllers
{
    public class HomeController : Controller
    {

        private const string _GetTeamRoute = "~/teams";
        private const string _UpdateStatsRoute = "~/stats/update";
        private const string _GetStatsRoute = "~/stats";
        private const string _GetLeagueRoute = "~/league";
        private const string _GetPlayerGameRoute = "~/games/{id?}";


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Route(_GetLeagueRoute, Name = "league")]
        [HttpGet]
        public ActionResult GetLeague()
        {
            FantasyLeague league = LoadLeague();

            LeagueModel leagueModel = new LeagueModel(league);

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(leagueModel);
            return result;
        }

        [Route(_GetStatsRoute, Name ="stats")]
        [HttpGet]
        public ActionResult GetStats()
        {
            StatisticManager manager = new StatisticManager();
            List<Player> playerStats = manager.GetHistorialPlayerStatistics();

            List<PlayerModel> playerModels = new List<PlayerModel>();
            foreach(Player player in playerStats)
            {
                PlayerModel newPlayerModel = new PlayerModel();
                newPlayerModel.Name = player.Name;
                playerModels.Add(newPlayerModel);
            }

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(playerModels);
            return result;
        }        

        [Route(_UpdateStatsRoute)]
        [HttpGet]
        public ActionResult UpdateTeams()
        {
            FantasyTeamManager manager = FantasyTeamManager.Insance;
            manager.GetLeague("Robs").UpdateLeague();
            manager.ResetLeagueCache("Robs");

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject("");
            return result;
        }

        private static FantasyLeague LoadLeague()
        {
            FantasyTeamManager manager = FantasyTeamManager.Insance;
            FantasyLeague league = manager.GetLeague("Robs");
            return league;
        }
    }
}