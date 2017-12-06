namespace FantasyPoolApp {

    export interface IGameModel {
        HomeTeam: string;
        AwayTeam: string;
        HomeGoals: number;
        AwayGoals: number;
        RecordDate: Date;
    }

    export class GameEditorView {

        private _Games: IGameModel[];

        private $container: JQuery;

        public initialize($container: JQuery) {
            this.FetchGames(() => {
                this.render($container);
            });
        }

        public render($container: JQuery) {

            var $gameContent = $container.find('.games');

            $gameContent.empty();

            var gameArray = [];

            _.each(this._Games, (game: IGameModel) => {

                gameArray.push([
                    game.RecordDate,
                    game.HomeTeam,
                    game.HomeGoals,
                    game.AwayTeam,
                    game.AwayGoals]);

            });

            // create the data table
            var teamDataTable = $gameContent.DataTable(
                {
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
        }

        private FetchGames(callback: any) {

            $.ajax({
                url: "games",
                type: "GET",
                success: (gamesList: any) => {
                    this._Games = <IGameModel[]>JSON.parse(gamesList);
                    callback();
                }
            });
        }

    }

}