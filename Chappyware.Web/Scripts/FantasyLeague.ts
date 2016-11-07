namespace FantasyPoolApp {

    export interface IFantasyPlayer {
        Name: string;
        Goals: number;
        Assists: number;
        Points: number;
    }

    export interface IFantasyTeam {
        Players: IFantasyPlayer[];
        Owner: string;
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

            // grab the team table template
            var $teamTableTemplate = $('#hidden').find('.team');

            // iterate over the teams and build up the points table
            _.each(this.teams, (team: IFantasyTeam) => {
                var $teamTable = $teamTableTemplate.clone();

                var $headingRow = $teamTable.find('.heading');

                _.each(team.Players, (player: IFantasyPlayer) => {

                    // get the player row template
                    var $playerRow = $teamTableTemplate.find('.player').clone();

                    // append columns
                    $playerRow.append('<td>' + player.Name + '</td>');
                    $playerRow.append('<td>' + player.Goals + '</td>');
                    $playerRow.append('<td>' + player.Assists + '</td>');
                    $playerRow.append('<td>' + player.Points + '</td>');
                    $playerRow.append('<td>' +  + '</td>');
                    $playerRow.append('<td>' +  + '</td>');
                    $playerRow.append('<td>' + + '</td>');

                    $teamTable.append($playerRow);
                });


                $mainContent.append('<div>' + team.Owner + '</div>');
                $mainContent.append($teamTable);
            });
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