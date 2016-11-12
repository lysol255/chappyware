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
            // iterate over the teams and build up the points table
            _.each(this.teams, function (team) {
                // render the player statistics
                _this.RenderPlayerTable(team, $mainContent);
                var $ownerTable = $mainContent.find('.owners');
                // render the team toals
                _this.RenderTeamTotalTable(team, $ownerTable);
            });
        };
        League.prototype.RenderTeamTotalTable = function (team, $ownerTable) {
            // clone an owner row
            var $ownerRow = $('#hidden').find('.owner').clone();
            // add the columns
            $ownerRow.append('<td>' + team.OwnerName + '</td>');
            $ownerRow.append('<td>' + team.TotalGoals + '</td>');
            $ownerRow.append('<td>' + team.TotalAssists + '</td>');
            $ownerRow.append('<td>' + team.TotalPoints + '</td>');
            // append to the container
            $ownerTable.append($ownerRow);
        };
        League.prototype.RenderPlayerTable = function (team, $container) {
            // grab the team table template
            var $teamTableTemplate = $('#hidden').find('.team');
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
                $playerRow.append('<td>' + player.AvgTimeOnIce + '</td>');
                $playerRow.append('<td>' + player.GamesPlayed + '</td>');
                $playerRow.append('<td>' + player.PointsPerGame.toFixed(2) + '</td>');
                $teamTable.append($playerRow);
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
