<template>
  <v-main>
    <v-container fluid class="pa-6">
      <Breadcrumbs />

      <v-form ref="form" lazy-validation class="py-5">
        <v-col cols="12">
          <v-row class="d-flex align-center">
            <v-col cols="9">
              <v-text-field
                elevation="0"
                v-model="titulo.descricao"
                :rules="[(v) => !!v || 'A descrição é obrigatória']"
                label="Descrição"
                required
                outlined
                color="var(--color-default)"
                class="py-6"
              />
            </v-col>
            <v-col cols="3">
              <v-select
                label="Tipo de título"
                v-model="titulo.tipoTitulo"
                :items="tipoTitulos"
                item-text="nome"
                item-value="id"
                color="var(--color-default)"
                :rules="[
                  (v) =>
                    (v !== undefined && v !== null) ||
                    'O tipo de título é obrigatório',
                ]"
                required
                outlined
              />
            </v-col>
          </v-row>
          <v-row class="d-flex align-center">
            <v-col cols="6">
              <v-select
                label="Natureza de Lançamento"
                v-model="titulo.naturezaDeLancamentoDescricao"
                :items="naturezaDisponiveis"
                item-text="descricao"
                item-value="id"
                color="var(--color-default)"
                :rules="[
                  (v) =>
                    (v !== undefined && v !== null) || 'A natureza de lançamento é obrigatória',
                ]"
                required
                outlined
              />
            </v-col>
            <v-col cols="3">
              <v-text-field
                v-model="titulo.valorOriginal"
                :rules="[(v) => !!v || 'O valor é obrigatório']"
                label="Valor"
                type="number"
                color="var(--color-default)"
                required
                prefix="R$"
                outlined
              />
            </v-col>
            <v-col cols="3">
              <v-text-field
                v-model="titulo.dataPagamento"
                :rules="[(v) => !!v || 'A data de pagamento é obrigatória']"
                label="Data de Pagamento"
                color="var(--color-default)"
                required
                type="date"
                outlined
              />
            </v-col>
          </v-row>
          <v-row class="d-flex align-center">
            <v-col cols="12">
                <v-textarea
                  outlined
                  name="Observação"
                  label="Observação"
                  v-model="titulo.observacao"
                  color="var(--color-default)"
                ></v-textarea>
            </v-col>
          </v-row>
        </v-col>
      </v-form>

      <v-divider class="pb-6"></v-divider>

      <v-row class="rodape pt-10">
        <v-btn @click="cancelarAcao">Cancelar</v-btn>
        <v-btn
          class="ml-4"
          color="var(--primary-color)"
          dark
          @click="salvarTitulo"
          >Salvar</v-btn
        >
      </v-row>
    </v-container>
  </v-main>
</template>

<script>
import Titulo from "@/models/Titulo";
import tituloService from "@/services/titulo-service";
import conversorData from "@/utils/conversorData";
import Breadcrumbs from "@/components/TituloPagina/Breadcrumbs.vue";
import naturezaService from "@/services/natureza-service";

export default {
  name: "Titulo",
  components: {
    Breadcrumbs,
  },
  data() {
    return {
      valid: true,
      titulo: new Titulo(),
      modoCadastro: true,
      tipoTitulos: [
        { id: 0, nome: "Gasto" },
        { id: 1, nome: "Recebimento" },
      ],
      naturezaDisponiveis: [],
    };
  },
  mounted() {
    this.carregarNaturezas();

    const id = this.$route.params.id || this.$route.query.id;
    if (!id) return;

    this.modoCadastro = false;
    this.obterTituloPorId(id);
  },

  watch: {
    "titulo.cargo"(newCargo) {
      this.titulo.cargo = Number(newCargo);
    },
    "$route.params.codigo": {
      immediate: true,
      handler(novoCodigo) {
        if (novoCodigo) {
          this.obterTituloPorId(novoCodigo);
        }
      },
    },
  },
  methods: {
    carregarNaturezas() {
      naturezaService
        .obterTodos()
        .then((response) => {
          this.naturezaDisponiveis = response.data.filter((natureza) => !natureza.dataInativacao);
        })
        .catch((error) => {
          console.error("Erro ao buscar naturezas de lançamento:", error);
        });
    },
    obterTituloPorId(id) {
      tituloService
        .obterPorId(id)
        .then((response) => {
          const dados = response.data;

          const dataFormatada = dados.dataPagamento
            ? conversorData.aplicarMascaraIsoEmFormatoAmericanoSemHorario(
                dados.dataPagamento
              )
            : null;

          this.titulo = new Titulo({
            ...dados,
            dataPagamento: dataFormatada,
          });

          const naturezaSelecionada = this.naturezaDisponiveis.find(
            (natureza) => natureza.id === dados.idNaturezaDeLancamento
          );

          if (naturezaSelecionada) {
            this.titulo.naturezaDeLancamentoDescricao = naturezaSelecionada.id;
          }
        })
        .catch((error) => {
          console.error("Erro ao obter título:", error);
        });
    },
    cancelarAcao() {
      this.$router.replace({ name: "Fluxo de Caixa" });
    },
    cadastrarTitulo() {
      if (!this.titulo.modeloValidoParaCadastro()) {
        this.$toast.warning("Verifique os campos obrigatórios!");
        return;
      }

      const dadosTitulo = {
        descricao: this.titulo.descricao,
        tipoTitulo: this.titulo.tipoTitulo,
        valorOriginal: this.titulo.valorOriginal,
        idNaturezaDeLancamento: this.titulo.naturezaDeLancamentoDescricao,
        observacao: this.titulo.observacao || "",
        dataPagamento: this.titulo.dataPagamento || null,
      };

      tituloService
        .cadastrar(dadosTitulo)
        .then(() => {
          this.$toast.success("Título cadastrado com sucesso!");
          this.titulo = new Titulo();
          this.$router.push({ name: "Fluxo de Caixa" });
        })
        .catch((error) => {
          console.error(
            "Erro ao cadastrar título:",
            error.response?.data || error.message
          );
          this.$toast.error("Erro ao cadastrar título");
        });
    },
    atualizarTitulo() {
      if (!this.titulo.modeloValidoParaAtualizar()) {
        console.warn("Validação falhou no modelo de título:", this.titulo);
        this.$toast.warning("Verifique os campos obrigatórios!");
        return;
      }

      const id = this.$route.params.id || this.$route.query.id;

      const dadosTituloAtualizado = {
        id: id,
        descricao: this.titulo.descricao,
        tipoTitulo: this.titulo.tipoTitulo,
        valorOriginal: this.titulo.valorOriginal,
        idNaturezaDeLancamento: this.titulo.naturezaDeLancamentoDescricao,
        observacao: this.titulo.observacao || "",
        dataPagamento: this.titulo.dataPagamento || null,
      };

      dadosTituloAtualizado.dataPagamento =
        conversorData.aplicarMascaraIsoEmFormatoAmericano(
          dadosTituloAtualizado.dataPagamento
        );

      tituloService
        .atualizar(dadosTituloAtualizado)
        .then(() => {
          this.$toast.success("Título cadastrado com sucesso!");
          this.$router.push({ name: "Fluxo de Caixa" });
        })
        .catch((error) => {
          console.error("Erro ao atualizar título:", error);
          this.$toast.error("Erro ao atualizar título.");
        });
    },

    salvarTitulo() {
      this.$refs.form.validate();
      if (this.valid) {
        this.modoCadastro ? this.cadastrarTitulo() : this.atualizarTitulo();
      } else {
        this.$toast.warning("Verifique os campos obrigatórios!");
      }
    },
  },
};
</script>

<style scoped>
.rodape {
  display: flex;
  align-items: center;
  justify-content: end;
  padding: 20px 0px 10px 0px !important;
}
</style>
