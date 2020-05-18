import { optionalChain } from "/app/utils/object.utils.js";
import { SimplePieChart } from "/app/common/shared/components/charts/simple-pie-chart/simple-pie-chart.js";
import { dashboardService } from "/app/modules/core/services/dashboard.service.js";
import { Line } from "/app/config/vue-chartjs.js";
import { colorize, transparentize } from "/app/common/shared/components/charts/charts.utils.js";
import { logsService } from '../../services/logs.service.js';
import { RawCMS } from "/app/config/raw-cms.js";

const _LineLogsChart = async () => {
    return {
        computed: {
            chartData: function () {

                return this.context;
            }
        },
        extends: Line,
        mounted() {
            this.renderChart(this.chartData, this.options);
            RawCMS.eventBus.$on("update-chart", () => {
                this.$data._chart.update();
            });
        },
        props: {
            context: {
                type: Object,
                default: {
                    datasets: [],
                    labels: []
                }
            },
            options: {
                type: Object,
                default: {}
            }
        }
    };
};


const _LogsChartdDef = async () => {
    const lineLogsChart = await _LineLogsChart();
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/logs/components/logs-chart/logs-chart.tpl.html"
    );

    return {
        components: {
            LineChart: lineLogsChart
        },
        computed: {
            chartData: function () {
                return this.model;
            }
        },
        created: async function () {
            this.model.labels = optionalChain(() => {
                let time = new Date();
                let res = [];
                for (var i = 10; i > 0; i--) {
                    res.push(moment(new Date(time.getTime() + i * 3000)).format('HH:mm ss'));
                }
                return res;
            }, {
                    fallbackValue: []
            });

            this.model.datasets = [];

            window.setInterval(async () => {
                var data = await this.logsService.getStatistics();
                this.model.labels.push(optionalChain(() => {
                    return moment(new Date()).format('HH:mm ss');
                }, {
                    fallbackValue: moment(new Date()).format('HH:mm ss')
                }));

                let dataset = [];
                var randomColorGenerator = function () {
                    return '#' + (Math.random().toString(16) + '0000000').slice(2, 8);
                };
                data.forEach(app => {
                    
                    let dataApp = {};
                    if (!this.model.datasets.find(x => x.applicationId == app.applicationId)) {
                        dataApp.label = app.applicationName;
                        dataApp.applicationId = app.applicationId;
                        dataApp.fill = false;
                        var color = randomColorGenerator();
                        dataApp.backgroundColor = color;
                        dataApp.borderColor = color;
                        dataApp.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, app.count];
                        dataset.push(dataApp);
                    } else {
                        dataApp = this.model.datasets.find(x => x.applicationId == app.applicationId);
                        dataApp.data.push(app.count);
                        dataApp.data.splice(0, 1);
                        dataset.push(dataApp);
                    }    
                });

                this.model.datasets.forEach(app => {
                    if (!data.find(x => x.applicationId == app.applicationId)) {
                        app.data.push(0);
                        app.data.splice(0, 1);
                        dataset.push(app);
                    }
                });

                this.model.datasets = dataset;
                this.model.labels.splice(0, 1);
                RawCMS.eventBus.$emit("update-chart");
            }, 3000);
            this.isLoading = false;
        },
        data: function () {
            return {
                chartOptions: {
                    lowerIsBetter: true,
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                },
                logsService: logsService,
                isLoading: true,
                optionalChain: optionalChain,
                model: {},
                chartWidth: (screen.width * 0.8)
            };
        },
        template: tpl
    };
};

const _LogsChart = async (res, rej) => {
    const cmpDef = await _LogsChartdDef();
    res(cmpDef);
};

export const LogsChartdDef = _LogsChartdDef;
export const LogsChart = _LogsChart;
export default _LogsChart;