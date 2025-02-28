<template>
  <highcharts :options="chartOptions"></highcharts>
</template>

<script>
import { Chart } from "highcharts-vue";

export default {
  name: "GraficoBarra",
  components: {
    highcharts: Chart,
  },
  props: {
    categorias: Array, 
    valores: Array,
    cores: Array, 
  },
  computed: {
    chartOptions() {
      return {
        chart: {
          type: "column",
        },
        title: {
          text: "Relação Gasto/Recebimento por Natureza",
        },
        credits: {
          enabled: false,
        },
        xAxis: {
          categories: this.categorias, 
        },
        yAxis: {
          title: {
            text: " ",
          },
        },
        series: [
          {
            name: "Gasto",
            data: this.valores.map((item) => item[0]), 
            color: "var(--gasto-color)", 
          },
          {
            name: "Recebimento",
            data: this.valores.map((item) => item[1]), 
            color: "var(--recebimento-color)", 
          },
        ],
        tooltip: {
          shared: true,
          valueSuffix: " títulos",
        },
      };
    },
  },
};
</script>
