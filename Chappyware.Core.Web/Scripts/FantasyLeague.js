var FantasyPoolApp;
(function (FantasyPoolApp) {
    var League = (function () {
        function League() {
            this.teamTemplate = "/Content/FantasyTeams.html";
        }
        League.prototype.initializae = function () {
            var _this = this;
            this.FetchLeague(function () {
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
            var teamTableArray = [];
            var $ownerTable = $('#hidden').find('.owners').clone();
            $leagueSummary.append($ownerTable);
            // iterate over the teams and build up the points table
            _.each(this.league.Teams, function (team) {
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
            var $controls = $('.controls');
            // initialize update control
            var $updateButton = $controls.find('.updatestats');
            $updateButton.click(function () {
                $updateButton.text("Updating...");
                _this.UpdateStats();
            });
            var $lastUpdated = $controls.find('.lastupdated');
        };
        League.prototype.RenderPlayerTable = function (team, $container) {
            var teamContent = new FantasyPoolApp.FantasyTeam(team);
            teamContent.render($container);
        };
        League.prototype.UpdateStats = function (callback) {
            var _this = this;
            $.ajax({
                url: "stats/update",
                type: "GET",
                success: function () {
                    $('.controls').find('.updatestats').text("Update");
                    $('#mainContent').find('.leagueSummary').empty();
                    _this.initializae();
                    if (callback) {
                        callback();
                    }
                }
            });
        };
        League.prototype.FetchLeague = function (callback) {
            var _this = this;
            $.ajax({
                url: "league",
                type: "GET",
                success: function (fetchedLeague) {
                    _this.league = JSON.parse(fetchedLeague);
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
