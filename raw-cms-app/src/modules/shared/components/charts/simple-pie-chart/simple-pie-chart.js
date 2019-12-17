import { Pie } from '../../../../../config/vue-chartjs.js';
import { colorize, transparentize } from '../charts.utils.js';

const _SimplePieChart = {
  computed: {
    chartData: function() {
      const backColors = this.context.data.map(x => this.normalColorize(x));

      const res = {
        datasets: [
          {
            data: this.context.data,
            backgroundColor: backColors,
            hoverColor: backColors.map(x => this.hoverColorize(x)),
          },
        ],
        labels: this.context.labels,
      };

      return res;
    },
  },
  extends: Pie,
  methods: {
    normalColorize: function(value) {
      return colorize(true, false, value);
    },
    hoverColorize: function(color) {
      return transparentize(color);
    },
    refresh: function() {
      this.renderChart(this.chartData, {});
    },
  },
  mounted() {
    this.refresh();
  },
  props: {
    context: {
      type: Object,
      default: {
        data: [],
        labels: [],
      },
    },
  },
  watch: {
    context: function() {
      this.refresh();
    },
  },
};

export const SimplePieChart = _SimplePieChart;
export default _SimplePieChart;
