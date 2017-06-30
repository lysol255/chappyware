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
            this.ShowLoading($mainContent);
            var $leagueSummary = $mainContent.find('.leagueSummary');
            var $analytics = $mainContent.find('.analytics');
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
                columnDefs: [{
                        targets: 0,
                        render: function (data, type, row) {
                            return '<a href="#' + data + '">' + data + '</a>';
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
            // control visibility
            this.ShowLeagueSummary($mainContent);
        };
        League.prototype.InitializeControls = function () {
            var _this = this;
            var $controls = $('.controls');
            var $mainContent = $('#mainContent');
            // initialize update control
            var $updateButton = $controls.find('.updatestats');
            var $analyticsButton = $controls.find('.analyticsbutton');
            var $teamsButton = $controls.find('.teamsbutton');
            var $allPlayersView = $controls.find('.playerstatsbutton');
            $updateButton.click(function () {
                _this.ShowLoading($mainContent);
                $updateButton.text("Updating...");
                _this.UpdateStats();
            });
            $analyticsButton.click(function () {
                var lineChart = new FantasyPoolApp.Analytics.Analytics($mainContent.find('.analytics'), _this.league.Teams[0]);
                _this.ShowAnalytics($mainContent);
            });
            $teamsButton.click(function () {
                _this.ShowLeagueSummary($mainContent);
            });
            $allPlayersView.click(function () {
                _this.ShowAllPlayers($mainContent);
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
        League.prototype.ShowLoading = function ($container) {
            this.HideAll($container);
            $container.find('.loading').removeClass('hidden');
        };
        League.prototype.HideAll = function ($container) {
            $container.find('.leagueSummary').addClass('hidden');
            $container.find('.analytics').addClass('hidden');
            $container.find('.playersview').addClass('hidden');
        };
        League.prototype.ShowLeagueSummary = function ($container) {
            this.HideAll($container);
            $container.find('.leagueSummary').removeClass('hidden');
            $container.find('.loading').addClass('hidden');
        };
        League.prototype.ShowAnalytics = function ($container) {
            this.HideAll($container);
            $container.find('.analytics').removeClass('hidden');
            $container.find('.loading').addClass('hidden');
        };
        League.prototype.ShowAllPlayers = function ($container) {
            this.HideAll($container);
            $container.find('.playersview').removeClass('hidden');
            $container.find('.loading').addClass('hidden');
        };
        return League;
    }());
    FantasyPoolApp.League = League;
})(FantasyPoolApp || (FantasyPoolApp = {}));
var league = new FantasyPoolApp.League();
league.initializae();
//# sourceMappingURL=FantasyLeague.js.map