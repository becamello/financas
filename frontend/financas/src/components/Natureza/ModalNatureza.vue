<template>
  <v-dialog
    v-model="dialog"
    :max-width="dialogWidth"
    :max-height="dialogHeight"
    persistent
  >
    <v-card class="card-dialog">
      <v-card-title class="titulo-dialog">Natureza de Lançamento</v-card-title>

      <div class="d-flex justify-end" v-if="!mostrarFormulario">
        <v-btn color="var(--primary-color)" text @click="formularioNatureza">
          <v-icon>mdi-plus</v-icon>Nova Natureza
        </v-btn>
      </div>

      <div class="tabela-natureza" v-if="!mostrarFormulario">
        <Tabela
          :headers="headers"
          :items="naturezas"
          :actions="actions"
          :height="320"
          :hideFooter="true"
          class="pa-5 mb-10"
        ></Tabela>
      </div>

      <v-card-actions v-if="!mostrarFormulario" class="d-flex justify-end">
        <v-btn color="var(--terciary-color)" text @click="dialog = false">
          Fechar
        </v-btn>
      </v-card-actions>

      <v-form v-if="mostrarFormulario" ref="form" lazy-validation class="pa-5">
        <v-col cols="12">
          <v-row class="d-flex align-center">
            <v-col cols="12">
              <v-text-field
                elevation="0"
                v-model="natureza.descricao"
                :rules="[(v) => !!v || 'A descrição é obrigatória']"
                label="Descrição"
                required
                outlined
                color="var(--cor-primaria)"
              />
            </v-col>
          </v-row>
          <v-row class="d-flex align-center">
            <v-col cols="12">
              <v-textarea
                outlined
                name="Observação"
                label="Observação"
                v-model="natureza.observacao"
                color="var(--cor-primaria)"
              ></v-textarea>
            </v-col>
          </v-row>
        </v-col>
        <v-row class="rodape pt-10">
          <v-btn @click="cancelarFormulario">Cancelar</v-btn>
          <v-btn
            class="ml-4"
            color="var(--primary-color)"
            dark
            @click="salvarNatureza"
          >
            Salvar
          </v-btn>
        </v-row>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>
import naturezaService from "@/services/natureza-service";
import Tabela from "../Tabela/Tabela.vue";
import NaturezaDeLancamento from "@/models/NaturezaDeLancamento";

export default {
  name: "ModalNatureza",
  components: {
    Tabela,
  },
  data() {
    return {
      mostrarFormulario: false,
      modoCadastro: true, 
      itemsPerPage: 10,
      headers: [
        { text: "Descrição", value: "descricao", width: "250px" },
        { text: "Observação", value: "observacao" },
        { text: " ", value: "acoes", sortable: false, fixed: "right", width: "10px" },
      ],
      actions: [
        {
          icon: "mdi-pencil",
          label: "Editar natureza",
          handler: (item) => {
            naturezaService
              .obterPorId(item.id)  
              .then((response) => {
                this.natureza = new NaturezaDeLancamento(response.data);
                this.mostrarFormulario = true;
                this.modoCadastro = false;  
              })
              .catch((error) => {
                console.error("Erro ao obter natureza:", error);
                this.$toast.error("Erro ao carregar natureza para edição");
              });
          },
        },
        {
          icon: "mdi-delete",
          label: "Excluir natureza",
          handler: (item) => {
            this.natureza = item;
            this.inativarNatureza();
          },
        },
      ],
      naturezas: [], 
      natureza: new NaturezaDeLancamento(),
    };
  },
  props: {
    value: { type: Boolean },
    dialogWidth: { type: [String, Number] },
    dialogHeight: { type: [String, Number] },
    TituloDialog: { type: String },
  },
  computed: {
    dialog: {
      get() {
        return this.value;
      },
      set(val) {
        this.$emit("input", val);
      },
    },
  },
  methods: {
    exibirNaturezas() {
      naturezaService
        .obterTodos()
        .then((response) => {
          this.naturezas = response.data.filter((natureza) => !natureza.dataInativacao);
        })
        .catch((error) => {
          console.error("Erro ao obter naturezas:", error);
        });
    },
    formularioNatureza() {
      this.mostrarFormulario = true;
      this.modoCadastro = true;
      this.natureza = new NaturezaDeLancamento(); 
    },
    cancelarFormulario() {
      this.mostrarFormulario = false;
    },
    cadastrarNatureza() {
      if (!this.natureza.modeloValidoParaCadastro()) {
        this.$toast.warning("Verifique os campos obrigatórios!");
        return;
      }

      naturezaService
        .cadastrar(this.natureza)
        .then(() => {
          this.$toast.success("Natureza cadastrada com sucesso!");
          this.exibirNaturezas();
          this.natureza = new NaturezaDeLancamento(); 
          this.mostrarFormulario = false;
        })
        .catch((error) => {
          console.error("Erro ao cadastrar natureza:", error);
          this.$toast.error("Erro ao cadastrar natureza");
        });
    },
    atualizarNatureza() {
      if (!this.natureza.modeloValidoParaAtualizar()) {
        this.$toast.warning("Verifique os campos obrigatórios!");
        return;
      }

      naturezaService
        .atualizar(this.natureza)
        .then(() => {
          this.$toast.success("Natureza atualizada com sucesso!");
          this.exibirNaturezas();
          this.mostrarFormulario = false;
        })
        .catch((error) => {
          console.error("Erro ao atualizar natureza:", error);
          this.$toast.error("Erro ao atualizar natureza.");
        });
    },
    salvarNatureza() {
      this.$refs.form.validate();
      if (this.modoCadastro) {
        this.cadastrarNatureza();
      } else {
        this.atualizarNatureza(); 
      }
    },
    inativarNatureza() {
      naturezaService
        .inativar(this.natureza)
        .then(() => {
          this.$toast.success("Natureza de lançamento excluída com sucesso!");
          this.exibirNaturezas();
        })
        .catch((error) => {
          console.error("Erro ao excluir natureza de lançamento:", error);
          this.$toast.error("Erro ao excluir natureza de lançamento.");
        });
    },
  },
  created() {
    this.exibirNaturezas();
  },
};
</script>

<style scoped>
.rodape {
  display: flex;
  align-items: center;
  justify-content: end;
  padding: 20px 0px 10px 0px !important;
  max-width: 50vw;
}
</style>
