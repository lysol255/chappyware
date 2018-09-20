var FantasyPoolApp;
(function (FantasyPoolApp) {
    var Analytics;
    (function (Analytics_1) {
        var Analytics = /** @class */ (function () {
            function Analytics($container, team) {
                var chartOptions = {
                    title: {
                        text: "Line Chart"
                    },
                    axisX: {
                        interval: 10
                    },
                    data: [{
                            type: "line",
                            dataPoints: [
                                { x: 10, y: 45 },
                                { x: 20, y: 14 },
                                { x: 30, y: 20 },
                                { x: 40, y: 60 },
                                { x: 50, y: 50 },
                                { x: 60, y: 80 },
                                { x: 70, y: 40 },
                                { x: 80, y: 60 },
                                { x: 90, y: 10 },
                                { x: 100, y: 50 },
                                { x: 110, y: 40 },
                                { x: 120, y: 14 },
                                { x: 130, y: 70 },
                                { x: 140, y: 40 },
                                { x: 150, y: 90 },
                            ]
                        }]
                };
                var chart = new CanvasJS.Chart('statchart', chartOptions);
                chart.render();
            }
            return Analytics;
        }());
        Analytics_1.Analytics = Analytics;
    })(Analytics = FantasyPoolApp.Analytics || (FantasyPoolApp.Analytics = {}));
})(FantasyPoolApp || (FantasyPoolApp = {}));
//# sourceMappingURL=Analytics.js.map