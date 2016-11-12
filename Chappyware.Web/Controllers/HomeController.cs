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

            List<TeamModel> teams = ConvertToModelObjects(league.Teams);
            
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(teams);
            return result;
        }

        private List<TeamModel> ConvertToModelObjects(List<FantasyTeam> teams)
        {
            List<TeamModel> teamModels = new List<TeamModel>();
            foreach(FantasyTeam team in teams)
            {
                TeamModel newTeam = new TeamModel();
                newTeam.OwnerName = team.Owner.Name;

                foreach (FantasyPlayer player in team.OwnedPlayers)
                {
                    PlayerStatsModel newPlayer = new PlayerStatsModel();
                    if (player.Player == null)
                    {
                        continue;
                    }
                    newPlayer.Name = player.Player.Name;

                    Statistics mostRecentStat = GetMostRecentOwnedStatistic(player);

                    newPlayer.Goals = mostRecentStat.Goals;
                    newPlayer.Assists = mostRecentStat.Assists;
                    newPlayer.Points = mostRecentStat.Goals + mostRecentStat.Assists;
                    newPlayer.GamesPlayed = mostRecentStat.GamesPlayed;
                    newPlayer.AvgTimeOnIce = mostRecentStat.AvgTOI;

                    // handle zero games played
                    newPlayer.PointsPerGame = 0;
                    if (newPlayer.GamesPlayed > 0)
                    {
                        newPlayer.PointsPerGame = (double)newPlayer.Points / (double)newPlayer.GamesPlayed;
                    }

                    newTeam.Players.Add(newPlayer);
                }

                teamModels.Add(newTeam);
            }
            return teamModels;
        }

        private Statistics GetMostRecentOwnedStatistic(FantasyPlayer player)
        {
            Statistics mostRecentStat = null;
            var currentStat = from statistic in player.Player.Stats
                              where statistic.RecordDate >= player.OwnedStartDate && statistic.RecordDate < player.OwnedEndDate
                              select statistic;
            if (currentStat.Count() > 0)
            {
                DateTime mostRecent = currentStat.Select(c => c.RecordDate).Max();
                mostRecentStat = currentStat.SingleOrDefault(c => c.RecordDate == mostRecent);
            }
            else
            {
                mostRecentStat = new Statistics();
            }

            return mostRecentStat;
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