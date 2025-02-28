<template>
  <highcharts :options="chartOptions"></highcharts>
</template>

<script>
import { Chart } from "highcharts-vue";

export default {
  name: "GraficoPizza",
  components: {
    highcharts: Chart,
  },
  props: {
    dados: Array, 
    cores: Array, 
    titulo: {
      type: String,
      required: true,
    },
    name: {
      type: String,
      default: "Títulos", 
    },
    pointFormat: {
      type: String,
      default: "Gasto: <b>{point.gasto}</b> | Recebimento: <b>{point.recebimento}</b>",
    },
  },
  computed: {
    chartOptions() {
      return {
        chart: { type: "pie" },
        title: { text: this.titulo }, 
        credits: {
          enabled: false,
        },
        series: [
          {
            name: this.name,
            data: this.dados,
            colorByPoint: true,
            dataLabels: {
              enabled: true,
              format: "{point.name} <br>{point.percentage:.1f}%<br>",
              distance: 10,
              style: {
                fontSize: "12px",
                fontWeight: "normal",
              },
            },
          },
        ],
        tooltip: {
          pointFormat: this.pointFormat,
        },
      };
    },
  },
};
</script>
