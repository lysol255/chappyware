var FantasyPoolApp;
(function (FantasyPoolApp) {
    var League = (function () {
        function League() {
            this.teamTemplate = "/Content/FantasyTeams.html";
        }
        League.prototype.initializae = function () {
            var _this = this;
            this.FetchLeague(function () {
                _this.render();
            });
        };
        League.prototype.render = function () {
            var _this = this;
            // grab the main content div
            var $mainContent = $('#mainContent');
            var $bannerContent = $('.banner');
            // initialize banner
            var bannerActions = this.CreateBannerActions();
            this.banner = new FantasyPoolApp.Banner($bannerContent, $mainContent, bannerActions);
            this.banner.ShowLoading();
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
                            return '<a href="#' + data.replace(" ", "") + '">' + data + '</a>';
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
            var gamesView = new FantasyPoolApp.GameEditorView();
            var $container = $('#mainContent').find('.gameslistview');
            gamesView.initialize($container);
            // control visibility
            this.banner.ShowLeagueSummary();
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
        League.prototype.CreateBannerActions = function () {
            var _this = this;
            var bannerActions = [];
            // update
            var updateAction = {
                Name: 'Update',
                Selector: 'updatestats',
                Action: function () {
                    _this.UpdateStats();
                }
            };
            bannerActions.push(updateAction);
            // games
            var gamesAction = {
                Name: 'Games',
                Selector: 'gameslistview',
                Action: function () {
                    _this.banner.ShowGamesEditorView();
                }
            };
            bannerActions.push(gamesAction);
            return bannerActions;
        };
        return League;
    }());
    FantasyPoolApp.League = League;
})(FantasyPoolApp || (FantasyPoolApp = {}));
var league = new FantasyPoolApp.League();
league.initializae();
//# sourceMappingURL=FantasyLeague.js.map