namespace FantasyPoolApp {

    export interface IHistoricalStatistic{
        Goals: number;
        Assists: number;
        Points: number;
        GamesPlayed: number;
        AverageTimeOnIce: string;
        RecordDate: Date;
    }


    export interface IFantasyPlayer {
        Name: string;
        Goals: number;
        Assists: number;
        Points: number;
        GamesPlayed: number;
        AverageTimeOnIce: string;
        PointsPerGame: number;
        DraftRound: number;
        HistoricalStatistics: IHistoricalStatistic[]
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
        Teams: IFantasyTeam[];
        Name: string;
    }

    export class League {

        private teamTemplate = "/Content/FantasyTeams.html";

        leagueJson: JQuery;
        league: IFantasyLeague;
        name: string;

        private $fantasyTeamTemplate: JQuery;

        public initializae() {
            this.FetchLeague(() => {
                // initialize controls
                this.InitializeControls();
                
                this.render();
            });
        }

        private render() {

            // grab the main content div
            var $mainContent = $('#mainContent');

            this.ShowLoading($mainContent);

            var $leagueSummary = $mainContent.find('.leagueSummary');

            var $leagueSummaryLoading = $mainContent.find('.leagueSummary-loading');
            
            var teamTableArray = [];

            var $ownerTable = $('#hidden').find('.owners').clone();
            $leagueSummary.append($ownerTable);

            // iterate over the teams and build up the points table
            _.each(this.league.Teams, (team: IFantasyTeam) => {

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

            // control visibility
            this.ShowLeagueSummary($mainContent);

        }

        private InitializeControls() {

            var $controls = $('.controls');

            // initialize update control
            var $updateButton = $controls.find('.updatestats');
            
            $updateButton.click(() => {

                this.ShowLoading($('#mainContent'));

                $updateButton.text("Updating...");

                this.UpdateStats();
            });

            var $lastUpdated = $controls.find('.lastupdated');
        }

        private RenderPlayerTable(team: IFantasyTeam, $container: JQuery) {
            var teamContent = new FantasyTeam(team);
            teamContent.render($container);
        }

        private UpdateStats(callback?: any) {
            $.ajax({
                url: "stats/update",
                type: "GET",
                success: () => {
                    $('.controls').find('.updatestats').text("Update");
                    $('#mainContent').find('.leagueSummary').empty();
                    this.initializae();
                    if (callback) {
                        callback();
                    }
                }
            });
        }

        private FetchLeague(callback: any) {

            $.ajax({
                url: "league",
                type: "GET",
                success: (fetchedLeague: any) => {
                    this.league = <IFantasyLeague>JSON.parse(fetchedLeague);
                    callback();
                }
            });
        }

        private ShowLoading($container: JQuery) {
            $container.find('.leagueSummary').addClass('hidden');
            $container.find('.leagueSummary-loading').removeClass('hidden');
        }

        private ShowLeagueSummary($container: JQuery) {
            $container.find('.leagueSummary').removeClass('hidden');
            $container.find('.leagueSummary-loading').addClass('hidden');
        }
    }
}

var league = new FantasyPoolApp.League();
league.initializae();