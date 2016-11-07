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
            // grab the main content div
            var $mainContent = $('#mainContent');
            // grab the team table template
            var $teamTableTemplate = $('#hidden').find('.team');
            // iterate over the teams and build up the points table
            _.each(this.teams, function (team) {
                var $teamTable = $teamTableTemplate.clone();
                var $headingRow = $teamTable.find('.heading');
                _.each(team.Players, function (player) {
                    // get the player row template
                    var $playerRow = $teamTableTemplate.find('.player').clone();
                    // append columns
                    $playerRow.append('<td>' + player.Name + '</td>');
                    $playerRow.append('<td>' + player.Goals + '</td>');
                    $playerRow.append('<td>' + player.Assists + '</td>');
                    $playerRow.append('<td>' + player.Points + '</td>');
                    $playerRow.append('<td>' + +'</td>');
                    $playerRow.append('<td>' + +'</td>');
                    $playerRow.append('<td>' + +'</td>');
                    $teamTable.append($playerRow);
                });
                $mainContent.append('<div>' + team.Owner + '</div>');
                $mainContent.append($teamTable);
            });
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
