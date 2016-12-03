var FantasyPoolApp;
(function (FantasyPoolApp) {
    var FantasyTeam = (function () {
        function FantasyTeam(team) {
            this.team = team;
        }
        FantasyTeam.prototype.render = function ($container) {
            // grab the team table template
            var $teamContent = $('#hidden').find('.teamContent').clone();
            var $teamTitle = $teamContent.find('.teamTitle');
            $teamTitle.append('<a id=' + this.team.OwnerName + '>' + this.team.OwnerName + '</>');
            var $teamTable = $teamContent.find('.team');
            // create player array
            var playerStatArray = [];
            _.each(this.team.Players, function (player) {
                playerStatArray.push([
                    player.DraftRound,
                    player.Name,
                    player.Goals,
                    player.Assists,
                    player.Points,
                    player.AverageTimeOnIce,
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
        return FantasyTeam;
    }());
    FantasyPoolApp.FantasyTeam = FantasyTeam;
})(FantasyPoolApp || (FantasyPoolApp = {}));
//# sourceMappingURL=FantasyTeam.js.map