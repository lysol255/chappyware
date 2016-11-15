var FantasyPoolApp;
(function (FantasyPoolApp) {
    var League = (function () {
        function League() {
            this.teamTemplate = "/Content/FantasyTeams.html";
        }
        League.prototype.initializae = function () {
            var _this = this;
            this.FetchTeams(function () {
                _this.render();
            });
        };
        League.prototype.render = function () {
            var _this = this;
            // grab the main content div
            var $mainContent = $('#mainContent');
            var teamTableArray = [];
            var $ownerTable = $mainContent.find('.owners');
            // iterate over the teams and build up the points table
            _.each(this.teams, function (team) {
                // render the player statistics
                _this.RenderPlayerTable(team, $mainContent);
                // add a new row of team totals
                teamTableArray.push([team.OwnerName, team.TotalGoals, team.TotalAssists, team.TotalPoints]);
            });
            // create the team data table
            $ownerTable.DataTable({
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
        };
        League.prototype.RenderPlayerTable = function (team, $container) {
            // grab the team table template
            var $teamTableTemplate = $('#hidden').find('.team');
            var $teamTable = $teamTableTemplate.clone();
            // create player array
            var playerStatArray = [];
            _.each(team.Players, function (player) {
                // get the player row template
                var $playerRow = $teamTableTemplate.find('.player').clone();
                playerStatArray.push([player.Name, player.Goals, player.Assists, player.Points, player.AvgTimeOnIce, player.GamesPlayed, player.PointsPerGame.toFixed(2)]);
            });
            // create the data table
            $teamTable.DataTable({
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
        };
        League.prototype.FetchTeams = function (callback) {
            var _this = this;
            $.ajax({
                url: "/teams",
                type: "GET",
                success: function (fetchedTeams) {
                    _this.teams = JSON.parse(fetchedTeams);
                    callback();
                }
            });
        };
        return League;
    }());
    FantasyPoolApp.League = League;
})(FantasyPoolApp || (FantasyPoolApp = {}));
var league = new FantasyPoolApp.League();
league.initializae();
//# sourceMappingURL=FantasyLeague.js.map