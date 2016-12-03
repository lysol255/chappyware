using Chappyware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chappyware.Web.Models
{
    public class LeagueModel
    {
        public DateTime LastUpdated { get; set; }

        public List<TeamModel> Teams { get; set; }

        public LeagueModel(FantasyLeague league)
        {
            Teams = ConvertToModelObjects(league.Teams);

            //todo set last updaed
            SetLastUpdated();
        }

        private void SetLastUpdated()
        {
            //throw new NotImplementedException();
        }

        private List<TeamModel> ConvertToModelObjects(List<FantasyTeam> teams)
        {
            List<TeamModel> teamModels = new List<TeamModel>();

            int pointLeader = 0;

            foreach (FantasyTeam team in teams)
            {
                TeamModel newTeam = new TeamModel();
                newTeam.OwnerName = team.Owner.Name;

                foreach (FantasyPlayer fantasyPlayer in team.OwnedPlayers)
                {
                    PlayerModel playerModel = new PlayerModel();
                    if (fantasyPlayer.Player == null)
                    {
                        continue;
                    }
                    playerModel.Name = fantasyPlayer.Player.Name;

                    SetMostRecentStatsForPlayer(fantasyPlayer, playerModel);
 //                   SetHistoricalStatisticsForPlayer(fantasyPlayer, playerModel);

                    playerModel.DraftRound = fantasyPlayer.DraftRound;

                    newTeam.Players.Add(playerModel);
                }

                // make a point leader
                if (newTeam.TotalPoints > pointLeader)
                {
                    pointLeader = newTeam.TotalPoints;
                }

                teamModels.Add(newTeam);
            }

            // calculate a how far a team is behind the leader
            foreach (TeamModel teamModel in teamModels)
            {
                if (teamModel.TotalPoints < pointLeader)
                {
                    teamModel.PointsBehindLeader = pointLeader - teamModel.TotalPoints;
                }
            }

            return teamModels;
        }

        private void SetHistoricalStatisticsForPlayer(FantasyPlayer fantasyPlayer, PlayerModel playerModel)
        {
            foreach (Statistic stat in fantasyPlayer.Player.Stats)
            {
                HistoricalStatisticModel historicalStat = new HistoricalStatisticModel(stat);
                playerModel.HistoricalStats.Add(historicalStat);
            }
        }

        private void SetMostRecentStatsForPlayer(FantasyPlayer player, PlayerModel newPlayer)
        {
            Statistic mostRecentStat = player.GetCurrentStats();
            
            newPlayer.Goals = mostRecentStat.Goals;
            newPlayer.Assists = mostRecentStat.Assists;
            newPlayer.Points = (mostRecentStat.Goals + mostRecentStat.Assists);
            newPlayer.GamesPlayed = mostRecentStat.GamesPlayed;
            newPlayer.AverageTimeOnIce = mostRecentStat.AvgTOI;
            
            // handle zero games played
            newPlayer.PointsPerGame = 0;
            if (newPlayer.GamesPlayed > 0)
            {
                newPlayer.PointsPerGame = (double)newPlayer.Points / (double)newPlayer.GamesPlayed;
            }
        }

        

        
    }
}