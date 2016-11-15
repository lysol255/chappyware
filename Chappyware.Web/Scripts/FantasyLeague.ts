namespace FantasyPoolApp {

    export interface IFantasyPlayer {
        Name: string;
        Goals: number;
        Assists: number;
        Points: number;
        GamesPlayed: number;
        AvgTimeOnIce: string;
        PointsPerGame: number;
    }

    export interface IFantasyTeam {
        Players: IFantasyPlayer[];
        OwnerName: string;
        TotalGoals: number;
        TotalAssists: number;
        TotalPoints: number;
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
                this.render();
            });
        }

        private render() {
            // grab the main content div
            var $mainContent = $('#mainContent');

            var teamTableArray = [];

            var $ownerTable = $mainContent.find('.owners')

            // iterate over the teams and build up the points table
            _.each(this.teams, (team: IFantasyTeam) => {

                // render the player statistics
                this.RenderPlayerTable(team, $mainContent);
                
                // add a new row of team totals
                teamTableArray.push([team.OwnerName, team.TotalGoals, team.TotalAssists, team.TotalPoints]);
                
            });

            // create the team data table
            $ownerTable.DataTable(
                {
                    data: teamTableArray,
                    paging: false,
                    info: false,
                    searching: false,
                    columns: [
                        { title: "Name" },
                        { title: "Goals" },
                        { title: "Assits" },
                        { title: "Points" }
                    ]
                });
        }

        private RenderPlayerTable(team: IFantasyTeam, $container: JQuery) {

            // grab the team table template
            var $teamTableTemplate = $('#hidden').find('.team');

            var $teamTable = $teamTableTemplate.clone();

            // create player array
            var playerStatArray = [];

            _.each(team.Players, (player: IFantasyPlayer) => {

                // get the player row template
                var $playerRow = $teamTableTemplate.find('.player').clone();

                playerStatArray.push([player.Name, player.Goals, player.Assists, player.Points, player.AvgTimeOnIce, player.GamesPlayed, player.PointsPerGame.toFixed(2)]);

            });

            // create the data table
            $teamTable.DataTable(
                {
                    data: playerStatArray,
                    paging: false,
                    columns: [
                        { title: "Name" },
                        { title: "Goals" },
                        { title: "Assits" },
                        { title: "Points" },
                        { title: "Avg TOI " },
                        { title: "Games Played" },
                        { title: "PPG" }
                    ]
                });

            $container.append('<div>' + team.OwnerName + ',' + team.Players.length + '</div>');
            $container.append($teamTable);
        }

        private FetchTeams(callback: any) {

            $.ajax({
                url: "/teams",
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