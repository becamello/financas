<template>
  <v-main>
    <v-container fluid>
      <v-row>
        <v-col cols="12" md="10" class="pa-0">
          <v-row class="d-flex justify-space-between py-5 px-8 ml-4">
            <Breadcrumbs />

            <v-row class="cards">
              <v-col cols="12" sm="4" md="3">
                <v-card elevation="1" class="card-valor card-valor-gastos">
                  <v-icon>mdi-minus-circle-multiple-outline</v-icon>
                  <v-card-title>Gastos:</v-card-title>
                  <div class="card-conteudo">
                    {{ totalGastoExibicao }}
                  </div>
                </v-card>
              </v-col>

              <v-col cols="12" sm="4" md="3" >
                <v-card
                  elevation="1"
                  class="card-valor card-valor-recebimentos"
                >
                  <v-icon>mdi-hand-coin-outline</v-icon>
                  <v-card-title>Recebimentos:</v-card-title>
                  <div class="card-conteudo">
                    {{ totalRecebimentoExibicao }}
                  </div>
                </v-card>
              </v-col>

              <v-col cols="12" sm="4" md="3" >
                <v-card elevation="1" class="card-valor card-valor-saldo">
                  <v-icon>mdi-cash-register</v-icon>
                  <v-card-title>Saldo Final:</v-card-title>
                  <div class="card-conteudo">
                    {{ saldoFinalExibicao }}
                  </div>
                </v-card>
              </v-col>
            </v-row>
          </v-row>

          <v-col cols="12" class="px-10 mt-2">
            <Tabela
              :headers="headers"
              :items="exibirTitulos"
              :actions="actions"
              :height="400"
            />
          </v-col>
        </v-col>
        <v-col cols="12" md="2">
          <v-card
            elevation="0"
            style="background: transparent !important"
            class="d-flex flex-column py-2"
          >
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

      <v-dialog v-model="dialogDetalhes" width="50vw">
        <v-card>
          <v-card-title>
            <span class="headline">Detalhes do Título</span>
          </v-card-title>
          <v-card-text>
            <v-list dense>
              <v-row>
                <v-col cols="12" md="6">
                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-tag</v-icon>
                      <!-- Ícone para identificador -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong
                          >Identificador do Título:</strong
                        ></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.id
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-account-circle</v-icon>
                      <!-- Ícone para tipo -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong>Tipo de Título:</strong></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.tipoTituloDescricao
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-currency-usd</v-icon>
                      <!-- Ícone para valor -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong>Valor:</strong></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.valorOriginal
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-tag</v-icon>
                      <!-- Ícone para identificador -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong
                          >Identificador da Natureza de Lançamento:</strong
                        ></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.idNaturezaDeLancamento
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>
                </v-col>
                <v-col cols="12" md="6">
                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-text-box</v-icon>
                      <!-- Ícone para descrição -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong>Descrição:</strong></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.descricao
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-calendar</v-icon>
                      <!-- Ícone para data de cadastro -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong>Data de Cadastro:</strong></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.dataCadastro
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-calendar-check</v-icon>
                      <!-- Ícone para data de pagamento -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong>Data de Pagamento:</strong></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.dataPagamento
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>
                  <v-list-item class="pa-4">
                    <v-list-item-icon>
                      <v-icon>mdi-bookmark</v-icon>
                      <!-- Ícone para natureza -->
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong
                          >Natureza de Lançamento:</strong
                        ></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.naturezaDeLancamentoDescricao
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>
                </v-col>
                <v-col cols="12" md="12">
                  <v-list-item>
                    <v-list-item-icon>
                      <v-icon>mdi-comment</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                      <v-list-item-title
                        ><strong>Observações:</strong></v-list-item-title
                      >
                      <v-list-item-subtitle>{{
                        tituloDetalhes.observacao
                      }}</v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>
                </v-col>
              </v-row>
            </v-list>
          </v-card-text>
          <v-card-actions class="d-flex justify-end">
            <v-btn
              dark
              color="var(--primary-color)"
              @click="dialogDetalhes = false"
              >Fechar</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
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
      dialogDetalhes: false,
      tituloDetalhes: {
        id: null,
        descricao: "",
        valorOriginal: "",
        naturezaDeLancamentoDescricao: "",
        tipoTituloDescricao: "",
        dataCadastro: "",
        dataPagamento: "",
        observacao: "",
      },
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
        {
          icon: "mdi-eye",
          label: "Visualizar detalhes",
          handler: (titulo) => {
            this.visualizarDetalhes(titulo);
          },
        },
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
    visualizarDetalhes(titulo) {
      this.tituloDetalhes = titulo;
      this.dialogDetalhes = true;
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
  font-size: 12px;
}
.cards {
  display: flex;
  justify-content: end; 
  flex-wrap: wrap; 
}

.card-valor {
  width: 90%;
  height: auto;
  padding: 8px;
  border-radius: 12px;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.card-valor-gastos {
  border-top: 4px solid var(--gasto-color);
}
.card-valor-recebimentos {
  border-top: 4px solid var(--recebimento-color);
}
.card-valor-saldo {
  border-top: 4px solid var(--saldo-color);
}

.v-card__title {
  font-size: 16px;
  font-weight: 500;
  text-align: left;
  padding: 0px;
}
</style>
