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
                order: [[3, "desc"]],
                columns: [
                    { title: "Name" },
                    { title: "Goals" },
                    { title: "Assists" },
                    { title: "Points" }
                ]
            });
        };
        League.prototype.RenderPlayerTable = function (team, $container) {
            // grab the team table template
            var $teamContent = $('#hidden').find('.teamContent').clone();
            var $teamTitle = $teamContent.find('.teamTitle');
            $teamTitle.append('<p>' + team.OwnerName + ',' + team.Players.length + '</p>');
            var $teamTable = $teamContent.find('.team');
            // create player array
            var playerStatArray = [];
            _.each(team.Players, function (player) {
                playerStatArray.push([player.Name, player.Goals, player.Assists, player.Points, player.AvgTimeOnIce, player.GamesPlayed, player.PointsPerGame.toFixed(2)]);
            });
            // create the data table
            $teamTable.DataTable({
                data: playerStatArray,
                paging: false,
                info: false,
                searching: false,
                order: [[3, "desc"]],
                columns: [
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
        };
        League.prototype.FetchTeams = function (callback) {
            var _this = this;
            $.ajax({
                url: "teams",
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