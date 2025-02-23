<template>
  <v-main>
    <v-container fluid>
      <v-row>
        <v-col cols="12" md="10" class="pa-0">
          <v-row class="d-flex justify-space-between py-5 px-8">
            <Breadcrumbs/>
            <v-btn color="var(--primary-color)" dark>
              <v-icon>mdi-plus</v-icon>Adicionar Título
            </v-btn>
          </v-row>
          <Tabela :headers="headers" :items="exibirTitulos" :actions="actions" />
        </v-col>

        <v-divider vertical></v-divider>

        <v-col cols="12" md="2">
          <v-card elevation="0" height="90vh" class="d-flex flex-column py-4">
            <h3 style="text-align: center; padding-bottom: 50px">FILTROS</h3>
            <DataPicker v-model="date" label="Data Cadastro" />
            <DataPicker v-model="data" label="Data Pagamento" />
            <v-divider></v-divider>
            <p style="padding-top: 20px; margin: 0">Tipo de Título:</p>
            <v-radio-group v-model="column" column>
              <v-radio label="Gasto" value="Gasto" color="var(--primary-color)"></v-radio>
              <v-radio label="Recebimento" value="Recebimento" color="var(--primary-color)"></v-radio>
            </v-radio-group>
            <v-divider></v-divider>
            <p style="padding-top: 20px">Natureza de Lançamento:</p>
            <v-select :items="items" label="Outlined style" outlined color="var(--primary-color)"></v-select>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
</template>

<script>
import DataPicker from "@/components/Input/DataPicker.vue";
import Tabela from "@/components/Tabela/Tabela.vue";
import Breadcrumbs from "@/components/TituloPagina/Breadcrumbs.vue";
import tituloService from "@/services/titulo-service";
import Titulo from "@/models/Titulo";

export default {
  name: "FluxoDeCaixa",
  components: {
    DataPicker,
    Tabela,
    Breadcrumbs
  },
  data() {
    return {
      search: "",
      page: 1,
      itemsPerPage: 10,
      titulos: [],
      headers: [
        { text: "Código", value: "id" },
        { text: "Descrição", value: "descricao" },
        { text: "Valor", value: "valorOriginal" },
        { text: "Natureza de Lançamento", value: "naturezaDeLancamentoDescricao" }, 
        { text: "Tipo do Título", value: "tipoTituloDescricao" },
        { text: "Data de Cadastro", value: "dataCadastro" },
        { text: "Data de Pagamento", value: "dataPagamento" },
        { text: " ", value: "acoes" },
      ],
      actions: [
        {
          icon: 'mdi-pencil',
          label: "Editar título"
        },
        {
          icon: 'mdi-eye',
          label: "Visualizar detalhes"
        },
      ],
    };
  },
  computed: {
    exibirTitulos() {
      return this.titulos;
    },
    totalPages() {
      return Math.ceil(this.titulos.length / this.itemsPerPage);
    },
  },
  methods: {
    obterTodosOsTitulos() { 
      tituloService
        .obterTodos()
        .then((response) => {
          this.titulos = response.data.map((t) => new Titulo(t));
        })
        .catch(error => {
          console.error("Erro ao obter títulos:", error);
        });
    },
  },
  created() {
    this.obterTodosOsTitulos();
  }
}
</script>
