import { Pie } from '../../../../../config/vue-chartjs.js';
import { optionalChain } from '../../../../../utils/object.utils.js';
import { colorize, transparentize } from '../charts.utils.js';

const _defaultChartOptions = {
  lowerIsBetter: false,
};

const _SimplePieChart = {
  computed: {
    chartData: function() {
      const data = this.sortedData;
      const backColors = data.map(x => this.normalColorize(x));

      const res = {
        datasets: [
          {
            data: data,
            backgroundColor: backColors,
            hoverColor: backColors.map(x => this.hoverColorize(x)),
          },
        ],
        labels: this.context.labels,
      };

      return res;
    },
    sortedData: function() {
      let data = optionalChain(() => [...this.context.data], { fallbackValue: [] }).sort(
        (a, b) => a - b
      );

      if (this.options.lowerIsBetter) {
        data = data.reverse();
      }

      return data;
    },
  },
  extends: Pie,
  methods: {
    normalColorize: function(value) {
      const data = this.sortedData;
      const min = Math.min(...data) || 0;
      const max = Math.max(...data) || 100;
      return colorize(value, { range: [min, max], lowerIsBetter: this.options.lowerIsBetter });
    },
    hoverColorize: function(color) {
      return transparentize(color);
    },
    refresh: function() {
      this.renderChart(this.chartData, {
        maintainAspectRatio: false,
      });
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
    options: {
      type: Object,
      default: _defaultChartOptions,
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
