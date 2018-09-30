namespace FantasyPoolApp {

    export interface IBannerAction {
        Name: string;
        Selector: string;
        Action: () => any;
    }

    export class Banner {

        private Teams: IFantasyTeam[];
        private $bannerContainer: JQuery;
        private $contentContainer: JQuery;
        private Actions: IBannerAction[];

        public constructor($container: JQuery, $contentContainer: JQuery, actions: IBannerAction[]) {
            this.$bannerContainer = $container;
            this.$contentContainer = $contentContainer;
            this.Actions = actions;
            this.render();
        }

        private render() {

            var $header = this.$bannerContainer.find('.mdl-layout__header');
            var $title = $header.find('.mdl-layout-title');
            $title = $title.text('Last updated: ');

            var $hamburger = this.$bannerContainer.find('.mdl-layout__drawer');

            var $navList = $hamburger.find('.mdl-navigation');
            var $navButtonTemplate = $('.navButtonTemplate');

            _.each(this.Actions, (bannerAction: IBannerAction) => {

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
        }

        public ShowLoading() {
            this.HideAll();
            this.$contentContainer.find('.loading').removeClass('hidden');
        }

        private HideAll() {
            this.$contentContainer.find('.leagueSummary').addClass('hidden');
            this.$contentContainer.find('.analytics').addClass('hidden');
            this.$contentContainer.find('.playersview').addClass('hidden');
            this.$contentContainer.find('.gameslistview').addClass('hidden');
        }

        public ShowLeagueSummary() {
            this.HideAll();
            this.$contentContainer.find('.leagueSummary').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        }

        private ShowAnalytics() {
            this.HideAll();
            this.$contentContainer.find('.analytics').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        }

        public ShowGamesEditorView() {
            this.HideAll();
            this.$contentContainer.find('.gameslistview').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        }

        private ShowAllPlayers() {
            this.HideAll();
            this.$contentContainer.find('.playersview').removeClass('hidden');
            this.$contentContainer.find('.loading').addClass('hidden');
        }

    }
    
}
