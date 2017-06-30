var FantasyPoolApp;
(function (FantasyPoolApp) {
    var FantasyPlayersView = (function () {
        function FantasyPlayersView($container) {
            var _this = this;
            this.FetchFantasyPlayers(function () {
                _this.Render($container);
            });
        }
        FantasyPlayersView.prototype.Render = function ($container) {
            if (this.Players) {
            }
        };
        FantasyPlayersView.prototype.FetchFantasyPlayers = function (callback) {
            var _this = this;
            $.ajax({
                url: "stats",
                type: "GET",
                success: function (players) {
                    _this.Players = JSON.parse(players);
                    if (callback) {
                        callback();
                    }
                }
            });
        };
        return FantasyPlayersView;
    }());
    FantasyPoolApp.FantasyPlayersView = FantasyPlayersView;
})(FantasyPoolApp || (FantasyPoolApp = {}));
//# sourceMappingURL=FantasyPlayersView.js.map