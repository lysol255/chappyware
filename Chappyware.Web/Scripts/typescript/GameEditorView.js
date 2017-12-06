var FantasyPoolApp;
(function (FantasyPoolApp) {
    var GameEditorView = (function () {
        function GameEditorView() {
        }
        GameEditorView.prototype.initialize = function ($container) {
            var _this = this;
            this.FetchGames(function () {
                _this.render($container);
            });
        };
        GameEditorView.prototype.render = function ($container) {
            var $gameContent = $container.find('.games');
            $gameContent.empty();
            var gameArray = [];
            _.each(this._Games, function (game) {
                gameArray.push([
                    game.RecordDate,
                    game.HomeTeam,
                    game.HomeGoals,
                    game.AwayTeam,
                    game.AwayGoals]);
            });
            // create the data table
            var teamDataTable = $gameContent.DataTable({
                data: gameArray,
                paging: false,
                info: false,
                searching: false,
                order: [[0, "desc"]],
                columns: [
                    { title: "Date" },
                    { title: "Home Team" },
                    { title: "Home Goals" },
                    { title: "Away Team" },
                    { title: "Away Goals" },
                ]
            });
            $container.append($gameContent);
            $container.removeClass('.hidden');
        };
        GameEditorView.prototype.FetchGames = function (callback) {
            var _this = this;
            $.ajax({
                url: "games",
                type: "GET",
                success: function (gamesList) {
                    _this._Games = JSON.parse(gamesList);
                    callback();
                }
            });
        };
        return GameEditorView;
    }());
    FantasyPoolApp.GameEditorView = GameEditorView;
})(FantasyPoolApp || (FantasyPoolApp = {}));
//# sourceMappingURL=GameEditorView.js.map