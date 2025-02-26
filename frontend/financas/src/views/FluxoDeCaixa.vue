<template>
  <v-main>
    <v-container fluid>
      <v-row>
        <v-col cols="12" md="10" class="pa-0">
          <v-row class="d-flex justify-space-between py-5 px-8 ml-4">
            <Breadcrumbs />

            <div class="cards">
              <v-card elevation="1" class="card-valor card-valor-gastos">
                <v-icon>mdi-minus-circle-multiple-outline</v-icon>
                <v-card-title>Gastos:</v-card-title>
                <div class="card-conteudo">
                  {{ totalGastoExibicao }}
                </div>
              </v-card>

              <v-card elevation="1" class="card-valor card-valor-recebimentos">
                <v-icon>mdi-hand-coin-outline</v-icon>
                <v-card-title>Recebimentos:</v-card-title>
                <div class="card-conteudo">
                  {{ totalRecebimentoExibicao }}
                </div>
              </v-card>

              <v-card elevation="1" class="card-valor card-valor-saldo">
                <v-icon>mdi-cash-register</v-icon>
                <v-card-title>Saldo Final:</v-card-title>
                <div class="card-conteudo">
                  {{ saldoFinalExibicao }}
                </div>
              </v-card>
            </div>
          </v-row>

          <v-col cols="12" class="px-10 mt-2">
            <Tabela
              :headers="headers"
              :items="exibirTitulos"
              :actions="actions"
              :height="460"
            />
          </v-col>
        </v-col>
        <v-col cols="12" md="2">
          <v-card elevation="0" height="60vh" class="d-flex flex-column py-2">
            <v-btn
              color="var(--primary-color)"
              dark
              @click="telaCadastro"
              class="mb-4"
            >
              <v-icon>mdi-plus</v-icon>
              Novo Título
            </v-btn>
            <v-btn
              elevation="1"
              @click="modalNaturezas"
              class="btn-natureza mb-4"
            >
              Natureza de Lançamento
            </v-btn>
            <v-divider></v-divider>
            <h3 style="text-align: center; padding-bottom: 20px" class="mt-4">
              FILTROS
            </h3>
            <DataPicker v-model="datacadastro" label="Data Cadastro" />
            <DataPicker v-model="datapagamento" label="Data Pagamento" />
            <v-divider></v-divider>
            <p style="padding-top: 20px; margin: 0">Tipo de Título:</p>
            <v-radio-group v-model="tipoTituloRadio" column>
              <v-radio
                label="Todos"
                value="Todos"
                color="var(--primary-color)"
              ></v-radio>
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
          </v-card>
        </v-col>
      </v-row>

      <ModalNatureza v-model="dialog" dialogWidth="50vw" />
    </v-container>
  </v-main>
</template>

<script>
import DataPicker from "@/components/Input/DataPicker.vue";
import Tabela from "@/components/Tabela/Tabela.vue";
import Breadcrumbs from "@/components/TituloPagina/Breadcrumbs.vue";
import tituloService from "@/services/titulo-service";
import Titulo from "@/models/Titulo";
import conversorValor from "@/utils/conversorValor";
import conversorData from "@/utils/conversorData";
import ModalNatureza from "@/components/Natureza/ModalNatureza.vue";

export default {
  name: "FluxoDeCaixa",
  components: {
    DataPicker,
    Tabela,
    Breadcrumbs,
    ModalNatureza,
  },
  data() {
    return {
      dialog: false,
      itemsPerPage: 10,
      titulos: [],
      tipoTituloRadio: "Todos",
      datacadastro: null,
      datapagamento: null,
      naturezaDeLancamento: null,
      headers: [
        { text: "Descrição", value: "descricao" },
        { text: "Valor", value: "valorOriginal" },
        {
          text: "Natureza de Lançamento",
          value: "naturezaDeLancamentoDescricao",
        },
        { text: "Tipo do Título", value: "tipoTituloDescricao" },
        { text: "Data de Cadastro", value: "dataCadastro" },
        { text: "Data de Pagamento", value: "dataPagamento" },
        { text: " ", value: "acoes", sortable: false },
      ],
      actions: [
        {
          icon: "mdi-pencil",
          label: "Editar título",
          handler: (titulo) => {
            this.$router.push({
              name: "Editar Título",
              params: { id: titulo.id },
            });
          },
        },
        { icon: "mdi-eye", label: "Visualizar detalhes" },
        {
          icon: "mdi-delete",
          label: "Excluir título",
          handler: (titulo) => {
            this.titulo = titulo;
            this.inativarTitulo();
          },
        },
      ],
    };
  },
  computed: {
    exibirTitulos() {
      return this.filtrarTitulos().map((titulo) => ({
        ...titulo,
        valorOriginal: conversorValor.aplicarMascaraParaRealComPrefixo(
          titulo.valorOriginal
        ),
        tipoTituloDescricao: titulo.tipoTituloDescricao,
      }));
    },
    totalPages() {
      return Math.ceil(this.titulos.length / this.itemsPerPage);
    },
    totalGastos() {
      return this.exibirTitulos
        .filter((titulo) => titulo.tipoTitulo === 0)
        .reduce(
          (acumulador, titulo) =>
            acumulador +
            conversorValor.removerMascaraDeReal(titulo.valorOriginal),
          0
        );
    },
    totalRecebimentos() {
      return this.exibirTitulos
        .filter((titulo) => titulo.tipoTitulo === 1)
        .reduce(
          (acumulador, titulo) =>
            acumulador +
            conversorValor.removerMascaraDeReal(titulo.valorOriginal),
          0
        );
    },
    saldoFinal() {
      return this.totalRecebimentos - this.totalGastos;
    },
    totalGastoExibicao() {
      return conversorValor.aplicarMascaraParaRealComPrefixo(this.totalGastos);
    },
    totalRecebimentoExibicao() {
      return conversorValor.aplicarMascaraParaRealComPrefixo(
        this.totalRecebimentos
      );
    },
    saldoFinalExibicao() {
      return conversorValor.aplicarMascaraParaRealComPrefixo(this.saldoFinal);
    },
  },
  methods: {
    telaCadastro() {
      this.$router.replace({ name: "Novo Titulo" });
    },
    obterTodosOsTitulos() {
      tituloService
        .obterTodos()
        .then((response) => {
          this.titulos = response.data.map(
            (t) =>
              new Titulo({
                ...t,
                dataPagamento: t.dataPagamento
                  ? conversorData.aplicarMascaraEmDataIso(t.dataPagamento)
                  : null,
              })
          );
        })
        .catch((error) => {
          console.error("Erro ao obter títulos:", error);
        });
    },
    filtrarTitulos() {
      return this.titulos.filter((titulo) => {
        if (titulo.dataInativacao) {
          return false;
        }

        if (
          this.tipoTituloRadio !== "Todos" &&
          titulo.tipoTituloDescricao !== this.tipoTituloRadio
        ) {
          return false;
        }

        if (
          this.datacadastro &&
          !this.compararDatas(titulo.dataCadastro, this.datacadastro)
        ) {
          return false;
        }

        if (
          this.datapagamento &&
          !this.compararDatas(titulo.dataPagamento, this.datapagamento)
        ) {
          return false;
        }

        return true;
      });
    },
    compararDatas(dataTitulo, dataFiltro) {
      const dataFiltroFormatada =
        conversorData.formatarDataParaMesAno(dataFiltro);
      const dataTituloFormatada =
        conversorData.formatarDataParaMesAno(dataTitulo);

      if (!dataFiltroFormatada) {
        return true;
      }
      return dataTituloFormatada === dataFiltroFormatada;
    },
    inativarTitulo() {
      tituloService
        .inativar(this.titulo)
        .then(() => {
          this.$toast.success("Título excluído com sucesso!");
          this.obterTodosOsTitulos();
        })
        .catch((error) => {
          console.error("Erro ao inativar funcionário:", error);
          this.$toast.error("Erro ao excluir titulo.");
        });
    },
    modalNaturezas() {
      this.dialog = !this.dialog;
    },
  },
  created() {
    this.obterTodosOsTitulos();
  },
};
</script>

<style scoped>
.btn-natureza {
  background-color: transparent !important;
  border: 1px solid var(--secondary-color);
  font-size: 13px;
}

.cards {
  display: flex;
  justify-content: space-between;
  margin-right: 1.2em;
}

.card-valor {
  width: 13vw;
  height: 13vh;
  border-radius: 12px;
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  margin-left: 20px;
}

.card-valor-gastos {
  border-top: 4px solid #f1948a;
}
.card-valor-recebimentos {
  border-top: 4px solid #85c1e9;
}
.card-valor-saldo {
  border-top: 4px solid #2ecc71;
}

.v-card__title {
  font-size: 16px;
  font-weight: 500;
  text-align: left;
  padding: 0px;
}
</style>
