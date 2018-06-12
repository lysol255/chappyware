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
        banner: Banner;

        private $fantasyTeamTemplate: JQuery;

        public initializae() {
            this.FetchLeague(() => {                
                this.render();
            });
        }

        private render() {

            // grab the main content div
            var $mainContent = $('#mainContent');
            var $bannerContent = $('.banner');

            // initialize banner
            var bannerActions = this.CreateBannerActions();
            this.banner = new Banner($bannerContent, $mainContent, bannerActions);
            this.banner.ShowLoading();

            var $leagueSummary = $mainContent.find('.leagueSummary');
            var $analytics = $mainContent.find('.analytics');
            
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
                    columnDefs: [{
                        targets: 0,
                        render: (data: string, type, row) => {
                            return '<a href="#' + data.replace(" ","") + '">' + data + '</a>';
                        }
                    }],
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

            var gamesView = new GameEditorView();
            var $container = $('#mainContent').find('.gameslistview');
            gamesView.initialize($container);

            // control visibility
            this.banner.ShowLeagueSummary();
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

        private CreateBannerActions(): IBannerAction[] {

            var bannerActions = [];
            
            // update
            var updateAction: IBannerAction = {
                Name: 'Update',
                Selector: 'updatestats',
                Action: () => {
                    this.UpdateStats();
                }
            }
            bannerActions.push(updateAction);

            // games
            //var gamesAction: IBannerAction = {
            //    Name: 'Games',
            //    Selector: 'gameslistview',
            //    Action: () => {

            //        this.banner.ShowGamesEditorView();

            //    }
            //}
            //bannerActions.push(gamesAction);

            return bannerActions;
        }
    }
}

var league = new FantasyPoolApp.League();
league.initializae();