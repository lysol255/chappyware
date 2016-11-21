var FantasyPoolApp;
(function (FantasyPoolApp) {
    var League = (function () {
        function League() {
            this.teamTemplate = "/Content/FantasyTeams.html";
        }
        League.prototype.initializae = function () {
            var _this = this;
            this.FetchTeams(function () {
                // initialize controls
                _this.InitializeControls();
                _this.render();
            });
        };
        League.prototype.render = function () {
            var _this = this;
            // grab the main content div
            var $mainContent = $('#mainContent');
            var $leagueSummary = $mainContent.find('.leagueSummary');
            // empty contents in prepartion of redraw
            $leagueSummary.empty();
            var teamTableArray = [];
            var $ownerTable = $('#hidden').find('.owners').clone();
            $leagueSummary.append($ownerTable);
            // iterate over the teams and build up the points table
            _.each(this.teams, function (team) {
                // render the player statistics
                _this.RenderPlayerTable(team, $leagueSummary);
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
                    { title: "Points" },
                    { title: "Games Played" },
                    { title: "Points Per Game" },
                    { title: "Points Behind Leader" }
                ]
            });
        };
        League.prototype.InitializeControls = function () {
            var _this = this;
            // initialize update control
            var $updateButton = $('.controls').find('.updatestats');
            $updateButton.click(function () {
                $updateButton.text("Updating...");
                _this.UpdateStats();
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
            $teamTable.DataTable({
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
        };
        League.prototype.UpdateStats = function (callback) {
            var _this = this;
            $.ajax({
                url: "stats/update",
                type: "GET",
                success: function () {
                    $('.controls').find('.updatestats').text("Update");
                    _this.initializae();
                    if (callback) {
                        callback();
                    }
                }
            });
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