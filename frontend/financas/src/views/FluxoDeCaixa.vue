<template>
  <v-main>
    <!-- Cargo - Converte ID para Nome -->
    <!-- <template v-slot:[`item.cargo`]="{ item }">
              {{ item.cargoNome }}
            </template>
       -->
    <!-- Ações 
    </v-container> -->
    <v-container fluid>
      <v-row>
        <v-col cols="12" md="10" class="pa-0">
          <v-row class="d-flex justify-space-between py-5 px-8">
            <Breadcrumbs/>

            <v-btn color="var(--primary-color)" dark
              ><v-icon>mdi-plus</v-icon>Adicionar Título</v-btn
            >
          </v-row>
          <Tabela :headers="headers" :items="items" :actions="actions" />
        </v-col>

        <v-divider vertical></v-divider>

        <v-col cols="12" md="2">
          <v-card
            elevation="0"
            height="90vh"
            width=""
            class="d-flex flex-column py-4"
          >
            <h3 style="text-align: center; padding-bottom: 50px">FILTROS</h3>
            <DataPicker v-model="date" label="Data Cadastro" />
            <DataPicker v-model="data" label="Data Pagamento" />
            <v-divider></v-divider>
            <p style="padding-top: 20px; margin: 0">Tipo de Título:</p>
            <v-radio-group v-model="column" column>
              <v-radio
                label="Gasto"
                value="Gasto"
                color="var(--primary-color)"
              ></v-radio>
              <v-radio
                label="Recebimento"
                value="Recebimento"
                color="var(--primary-color)"
              ></v-radio>
            </v-radio-group>
            <v-divider></v-divider>
            <p style="padding-top: 20px">Natureza de Lançamento:</p>
            <v-select
              :items="items"
              label="Outlined style"
              outlined
              color="var(--primary-color)"
            ></v-select>
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
      itemsPerPage: 5,
      headers: [
        { text: "Descrição", value: "descricao" },
        { text: "Data de Cadastro", value: "dataCadastro" },
        { text: "Valor", value: "valor" },
        { text: "Natureza de Lançamento", value: "naturezaDeLancamento" },
        { text: "Data de Pagamento", value: "dataPagamento" },
        { text: " ", value: "acoes" },
      ],
      items: [
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
        {
          descricao: "Uber",
          dataCadastro: "12/04/2024",
          valor: "R$25,00",
          naturezaDeLancamento: "Transporte",
          dataPagamento: "15/04/2024",
        },
      ],
      actions: [
        {
          label: 'Editar Título',
          icon: 'mdi-pencil',
          // handler: this.editarFuncionario,
        },
        {
          label: 'Novo Parecer',
          icon: 'mdi-comment-plus', // Ícone de novo parecer
          handler: this.novoParecer,
        },
        {
          label: 'Visualizar Pareceres',
          icon: 'mdi-eye', // Ícone de visualização
          handler: this.visualizarPareceres,
        },
      ],
    };
  },
  computed: {
    // filteredFuncionarios() {
    //   if (!this.search) {
    //     return this.funcionarios;
    //   }

    //   const searchLower = this.search.toLowerCase();

    //   return this.funcionarios.filter((f) => {
    //     return (
    //       (f.codigo && f.codigo.toString().includes(searchLower)) ||
    //       (f.login && f.login.toLowerCase().includes(searchLower)) ||
    //       (f.nome && f.nome.toLowerCase().includes(searchLower)) ||
    //       (f.cargoNome && f.cargoNome.toLowerCase().includes(searchLower)) ||
    //       (f.admissao && f.admissao.toLowerCase().includes(searchLower)) ||
    //       (f.desligamento && f.desligamento.toLowerCase().includes(searchLower))
    //     );
    //   });
    // },
    totalPages() {
      return Math.ceil(this.filteredFuncionarios.length / this.itemsPerPage);
    },
  },
  //   mounted() {
  //     const codigo = this.$route.params.codigo;
  //     if (codigo) {
  //       this.modoEdicao = true;
  //       this.carregarFuncionario(codigo);
  //     }
  //   },
  //   methods: {
  //     obterTodosOsFuncionarios() {
  //       funcionarioService
  //         .obterTodos()
  //         .then((response) => {
  //           this.funcionarios = response.data.map((f) => new Funcionario(f));
  //         })
  //         .catch(console.log);
  //     },
  //     adicionarFuncionario() {
  //       this.$router.push({ name: "NovoFuncionario" });
  //     },
  //     editarFuncionario(funcionario) {
  //       this.$router.push({
  //         name: "EditarFuncionario",
  //         params: { codigo: funcionario.codigo },
  //       });
  //     },
  //   },
  //   created() {
  //     this.obterTodosOsFuncionarios();
  //   },
};
</script>
