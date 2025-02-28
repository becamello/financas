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
    isDarkMode() {
    return this.$vuetify.theme.dark;
  },
    chartOptions() {
      return {
        chart: {
          type: "column",
          backgroundColor: "transparent"
        },
        title: {
          text: "Relação Gasto/Recebimento por Natureza",
          style: { color: this.isDarkMode ? "var(--secondary-darkmode-color)" : "var(--secondary-lightmode-color)" }
        },
        credits: {
          enabled: false,
        },
        xAxis: {
          categories: this.categorias, 
          labels: { style: { color: this.isDarkMode ? "var(--secondary-darkmode-color)" : "var(--secondary-lightmode-color)" } }
        },
        yAxis: {
          title: {
            text: " ",
            labels: { style: { color: this.isDarkMode ? "var(--secondary-darkmode-color)" : "var(--secondary-lightmode-color)" } }
          },
        },
        legend: {
          itemStyle: { color: this.isDarkMode ? "var(--secondary-darkmode-color)" : "var(--secondary-lightmode-color)" }, 
        itemHoverStyle: { color: this.isDarkMode ? "var(--text-hover-dark-color)" : "var(--text-hover-light-color)" }, 
        itemHiddenStyle: { color: this.isDarkMode ? "var(--secondary-darkmode-color)" : "var(--secondary-lightmode-color)"} 
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