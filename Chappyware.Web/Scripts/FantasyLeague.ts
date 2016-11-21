namespace FantasyPoolApp {

    export interface IFantasyPlayer {
        Name: string;
        Goals: number;
        Assists: number;
        Points: number;
        GamesPlayed: number;
        AvgTimeOnIce: string;
        PointsPerGame: number;
        DraftRound: number;
    }

    export interface IFantasyTeam {
        Players: IFantasyPlayer[];
        OwnerName: string;
        TotalGoals: number;
        TotalAssists: number;
        TotalPoints: number;
        TotalGamesPlayed: number;
        TeamPointsPerGame: number;
        PointsBehindLeader: number;
    }

    export interface IFantasyLeague {
        teams: IFantasyTeam[];
        name: string;
    }

    export class League implements IFantasyLeague {

        private teamTemplate = "/Content/FantasyTeams.html";

        leagueJson: JQuery;
        teams: IFantasyTeam[];
        name: string;

        private $fantasyTeamTemplate: JQuery;

        public initializae() {


            this.FetchTeams(() => {
                // initialize controls
                this.InitializeControls();
                
                this.render();
            });
        }

        private render() {

            // grab the main content div
            var $mainContent = $('#mainContent');

            var $leagueSummary = $mainContent.find('.leagueSummary');

            // empty contents in prepartion of redraw
            $leagueSummary.empty();

            var teamTableArray = [];

            var $ownerTable = $('#hidden').find('.owners').clone();
            $leagueSummary.append($ownerTable);

            // iterate over the teams and build up the points table
            _.each(this.teams, (team: IFantasyTeam) => {

                // render the player statistics
                this.RenderPlayerTable(team, $leagueSummary);
                
                // add a new row of team totals
                teamTableArray.push([team.OwnerName,
                    team.TotalGoals,
                    team.TotalAssists,
                    team.TotalPoints,
                    team.TotalGamesPlayed,
                    team.TeamPointsPerGame.toFixed(2),
                    team.PointsBehindLeader
                ]);
                
            });

            // create the team data table
            $ownerTable.DataTable(
                {
                    data: teamTableArray,
                    paging: false,
                    info: false,
                    searching: false,
                    order: [[3, "desc"]],
                    columns: [
                        { title: "Name" },
                        { title: "Goals" },
                        { title: "Assists" },
                        { title: "Points" },
                        { title: "Games Played" },
                        { title: "Points Per Game" },
                        { title: "Points Behind Leader" }
                    ]
                });
        }

        private InitializeControls() {

            // initialize update control
            var $updateButton = $('.controls').find('.updatestats');

            $updateButton.click(() => {

                $updateButton.text("Updating...");

                this.UpdateStats();
            });
        }

        private RenderPlayerTable(team: IFantasyTeam, $container: JQuery) {

            // grab the team table template
            var $teamContent = $('#hidden').find('.teamContent').clone();

            var $teamTitle = $teamContent.find('.teamTitle');
            $teamTitle.append('<p>' + team.OwnerName + ',' + team.Players.length + '</p>');

            var $teamTable = $teamContent.find('.team');

            // create player array
            var playerStatArray = [];

            _.each(team.Players, (player: IFantasyPlayer) => {

                playerStatArray.push([
                    player.DraftRound,
                    player.Name,
                    player.Goals,
                    player.Assists,
                    player.Points,
                    player.AvgTimeOnIce,
                    player.GamesPlayed,
                    player.PointsPerGame.toFixed(2)]);

            });

            // create the data table
            $teamTable.DataTable(
                {
                    data: playerStatArray,
                    paging: false,
                    info: false,
                    searching: false,
                    order: [[4, "desc"]],
                    columns: [
                        { title: "Round" },
                        { title: "Name" },
                        { title: "Goals" },
                        { title: "Assists" },
                        { title: "Points" },
                        { title: "Avg TOI " },
                        { title: "Games Played" },
                        { title: "PPG" }
                    ]
                });
                        
            $container.append($teamContent);
        }

        private UpdateStats(callback?: any) {
            $.ajax({
                url: "stats/update",
                type: "GET",
                success: () => {
                    $('.controls').find('.updatestats').text("Update");
                    this.initializae();
                    if (callback) {
                        callback();
                    }
                }
            });
        }

        private FetchTeams(callback: any) {

            $.ajax({
                url: "teams",
                type: "GET",
                success: (fetchedTeams: any) => {
                    this.teams = <IFantasyTeam[]>JSON.parse(fetchedTeams);
                    callback();
                }
            });
        }
    }
        
    //document.body.innerHTML = league.render();
}

var league = new FantasyPoolApp.League();
league.initializae();