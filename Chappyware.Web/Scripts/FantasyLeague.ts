namespace FantasyPoolApp {

    export interface IFantasyPlayer {
        Name: string;
        Goals: number;
        Assists: number;
        Points: number;
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
            
            // iterate over the teams and build up the points table
            _.each(this.teams, (team: IFantasyTeam) => {

                // render the player statistics
                this.RenderPlayerTable(team, $mainContent);

                var $ownerTable = $mainContent.find('.owners');

                // render the team toals
                this.RenderTeamTotalTable(team, $ownerTable);
            });
        }

        private RenderTeamTotalTable(team: IFantasyTeam, $ownerTable: JQuery) {

            // clone an owner row
            var $ownerRow = $('#hidden').find('.owner').clone();

            // add the columns
            $ownerRow.append('<td>' + team.OwnerName + '</td>');
            $ownerRow.append('<td>' + team.TotalGoals + '</td>');
            $ownerRow.append('<td>' + team.TotalAssists + '</td>');
            $ownerRow.append('<td>' + team.TotalPoints + '</td>');

            // append to the container
            $ownerTable.append($ownerRow);
        }

        private RenderPlayerTable(team: IFantasyTeam, $container: JQuery) {

            // grab the team table template
            var $teamTableTemplate = $('#hidden').find('.team');

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
                $playerRow.append('<td>' + + '</td>');
                $playerRow.append('<td>' + + '</td>');
                $playerRow.append('<td>' + + '</td>');

                $teamTable.append($playerRow);
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