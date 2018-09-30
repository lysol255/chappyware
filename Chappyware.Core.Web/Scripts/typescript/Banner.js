var FantasyPoolApp;
(function (FantasyPoolApp) {
    var Banner = /** @class */ (function () {
        function Banner($container, $contentContainer, actions) {
            this.$bannerContainer = $container;
            this.$contentContainer = $contentContainer;
            this.Actions = actions;
            this.render();
        }
        Banner.prototype.render = function () {
            var $header = this.$bannerContainer.find('.mdl-layout__header');
            var $title = $header.find('.mdl-layout-title');
            $title = $title.text('Last updated: ');
            var $hamburger = this.$bannerContainer.find('.mdl-layout__drawer');
            var $navList = $hamburger.find('.mdl-navigation');
            var $navButtonTemplate = $('.navButtonTemplate');
            _.each(this.Actions, function (bannerAction) {
                var $navButton = $navButtonTemplate.clone();
                $navButton.text(bannerAction.Name);
                $navButton.addClass(bannerAction.Selector);
                $navButton.click(bannerAction.Action);
                $navList.append($navButton);
            });
            // initialize update control
            var $updateButton = $hamburger.find('.updatestats');
            var $analyticsButton = $hamburger.find('.analyticsbutton');
            var $teamsButton = $hamburger.find('.teamsbutton');
            var $allPlayersView = $hamburger.find('.playerstatsbutton');
            //$analyticsButton.click(() => {
            //    var lineChart = new Analytics.Analytics($mainContent.find('.analytics'), this.league.Teams[0]);
            //    this.ShowAnalytics($mainContent);
            //});
            //$teamsButton.click(() => {
            //    this.ShowLeagueSummary();
            //});
            //$allPlayersView.click(() => {
            //    this.ShowAllPlayers();
            //});
        };
        Banner.prototype.ShowLoading = function () {
            this.HideAll();
            this.$contentContainer.find('.loading').removeClass('hidden');
        };
        Banner.prototype.HideAll = function () {
            this.$contentContainer.find('.leagueSummary').addClass('hidden');
            this.$contentContainer.find('.analytics').addClass('hidden');
            this.$contentContainer.find('.playersview').addClass('hidden');
            this.$contentContainer.find('.gameslistview').addClass('hidden');
        };
        Banner.prototype.ShowLeagueSummary = function () {
            this.HideAll();
            this.$contentContainer.find('.leagueSummary').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        };
        Banner.prototype.ShowAnalytics = function () {
            this.HideAll();
            this.$contentContainer.find('.analytics').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        };
        Banner.prototype.ShowGamesEditorView = function () {
            this.HideAll();
            this.$contentContainer.find('.gameslistview').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        };
        Banner.prototype.ShowAllPlayers = function () {
            this.HideAll();
            this.$contentContainer.find('.playersview').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        };
        return Banner;
    }());
    FantasyPoolApp.Banner = Banner;
})(FantasyPoolApp || (FantasyPoolApp = {}));
//# sourceMappingURL=Banner.js.map