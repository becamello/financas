<template>
  <v-main>


    <v-container class="pt-0" >
      <Breadcrumbs class="pb-10"/>
      <v-row>
        <v-col cols="12" md="6" sm="12">
          <v-card class="pa-5">
            <GraficoBarra
              :categorias="categoriasGraficoBarra"
              :valores="valoresGraficoBarra"
              :cores="cores"
            />
          </v-card>
        </v-col>
  
        <v-col cols="12" md="6"  sm="12">
          <v-card class="pa-5">
            <GraficoPizza
              :dados="dadosGraficoPizza"
              :cores="cores"
              titulo="Títulos por Natureza"
              name="Títulos"
              pointFormat="Gasto: <b>{point.gasto}</b> | Recebimento: <b>{point.recebimento}</b>"
            />
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
  </template>
  
  <script>
  import Breadcrumbs from "@/components/TituloPagina/Breadcrumbs.vue";
  import tituloService from "@/services/titulo-service";  
  import Titulo from "@/models/Titulo"; 
  import GraficoPizza from "@/components/Graficos/GraficoPizza.vue";
  import GraficoBarra from "@/components/Graficos/GraficoBarra.vue";
  
  export default {
    components: {
      GraficoPizza, 
      GraficoBarra, 
      Breadcrumbs,
    },
    props: {
      cores: Array,
    },
    data() {
      return {
        dadosGraficoPizza: [], 
        categoriasGraficoBarra: [],
        valoresGraficoBarra: [], 
      };
    },
  
    mounted() {
      this.fetchData();
    },
  
    methods: {
      async fetchData() {
        try {
          const response = await tituloService.obterTodos(); 
          const titulos = response.data.map((titulo) => new Titulo(titulo)); 
  
          const titulosAtivos = titulos.filter(
            (titulo) => !titulo.naturezaDeLancamentoDataInativacao
          );
  
          const gastos = {};
          const recebimentos = {};
  
          titulosAtivos.forEach((titulo) => {
            const tipoTitulo = titulo.tipoTituloDescricao; 
            const descricaoNatureza = titulo.naturezaDeLancamentoDescricao;
  
            if (tipoTitulo === "Gasto") {
              gastos[descricaoNatureza] = (gastos[descricaoNatureza] || 0) + 1; 
            } else if (tipoTitulo === "Recebimento") {
              recebimentos[descricaoNatureza] = (recebimentos[descricaoNatureza] || 0) + 1; 
            }
          });
  
          this.dadosGraficoPizza = Object.keys({ ...gastos, ...recebimentos }).map(
            (natureza) => ({
              name: natureza,
              y: (gastos[natureza] || 0) + (recebimentos[natureza] || 0),
              gasto: gastos[natureza] || 0,
              recebimento: recebimentos[natureza] || 0,
            })
          );
  
          this.categoriasGraficoBarra = Object.keys({ ...gastos, ...recebimentos });
          this.valoresGraficoBarra = this.categoriasGraficoBarra.map((natureza) => [
            gastos[natureza] || 0,
            recebimentos[natureza] || 0,
          ]);
  
        } catch (error) {
          console.error("Erro ao buscar os títulos:", error);
        }
      },
    },
  };
  </script>
  