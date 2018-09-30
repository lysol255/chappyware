namespace FantasyPoolApp {


    export class FantasyPlayersView {

        private Players: IFantasyPlayer[];

        public constructor($container: JQuery) {
            this.FetchFantasyPlayers(() => {
                this.Render($container);        
            });


        }

        private Render($container: JQuery) {
            if (this.Players) {

            }
        }

        private FetchFantasyPlayers(callback?: any) {
            $.ajax({
                url: "stats",
                type: "GET",
                success: (players: any) => {
                    this.Players = <IFantasyPlayer[]>JSON.parse(players);
                    if (callback) {
                        callback();
                    }
                }
            });
        }


    }

}