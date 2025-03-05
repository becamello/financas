<template>
    <v-dialog
      v-model="dialog"
      :max-width="dialogWidth"
      :max-height="dialogHeight"
      persistent
    >
      <v-card class="card-dialog">
        <v-card-title class="titulo-dialog">Usuários</v-card-title>
  
        <div class="d-flex justify-end" v-if="!mostrarFormulario">
          <v-btn color="var(--primary-color)" text @click="formularioUsuario">
            <v-icon>mdi-plus</v-icon>Novo Usuário
          </v-btn>
        </div>
  
        <div class="tabela-usuario" v-if="!mostrarFormulario">
          <Tabela
            :headers="headers"
            :items="usuariosComRoleDescricao"
            :actions="actions"
            :height="320"
            :hideFooter="true"
            class="pa-5 mb-10"
          ></Tabela>
        </div>
  
        <v-card-actions v-if="!mostrarFormulario" class="d-flex justify-end">
          <v-btn color="var(--color-default)" text @click="dialog = false">
            Fechar
          </v-btn>
        </v-card-actions>
  
        <v-form v-if="mostrarFormulario" ref="form" lazy-validation class="pa-5">
            <v-col cols="12">
              <v-radio-group v-model="usuario.role" row>
                  <v-radio label="Usuário" :value="1" color="var(--primary-color)"></v-radio>
                <v-radio label="Administrador" :value="0" color="var(--primary-color)"></v-radio>
              </v-radio-group>
            </v-col>
          <v-col cols="12">
            <v-row class="d-flex align-center">
              <v-col cols="8">
                <v-text-field
                  elevation="0"
                  v-model="usuario.email"
                  :rules="[(v) => !!v || 'O e-mail é obrigatório']"
                  label="E-mail"
                  required
                  outlined
                  color="var(--color-default)"
                />
              </v-col>
              <v-col cols="4">
                <v-text-field
                :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show ? 'text' : 'password'"
                outlined
                @click:append="show = !show"
                name="Senha"
                label="Senha"
                v-model="usuario.senha"
                color="var(--color-default)"
                :rules="[(v) => !!v || 'A senha é obrigatória']"
              ></v-text-field>
              </v-col>
            </v-row>
          </v-col>
          <v-row>
            <v-col cols="12" md="12" class="d-flex justify-end pt-10">
                <v-btn @click="cancelarFormulario">Cancelar</v-btn>
                <v-btn
                  class="ml-4"
                  color="var(--primary-color)"
                  dark
                  @click="salvarUsuario"
                >
                  Salvar
                </v-btn>
            </v-col>
          </v-row>
        </v-form>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  import usuarioService from "@/services/usuario-service";
  import Tabela from "../Tabela/Tabela.vue";
  import Usuario from "@/models/Usuario";
  
  export default {
    name: "ModalUsuario",
    components: {
      Tabela,
    },
    data() {
      return {
        show: false,
        mostrarFormulario: false,
        modoCadastro: true, 
        itemsPerPage: 10,
        headers: [
        { text: "Id", value: "id", width: "100px" },
        { text: "E-mail", value: "email", width: "600px" },
        { text: "Função", value: "roleDescricao" },
        { text: "Status", value: "status" },
        {
          text: " ",
          value: "acoes",
          sortable: false,
          fixed: "right",
          width: "10px",
        },
      ],
        actions: [
          {
            icon: "mdi-pencil",
            label: "Editar usuario",
            handler: (item) => {
              usuarioService
                .obterPorId(item.id)  
                .then((response) => {
                  this.usuario = new Usuario(response.data);
                  this.mostrarFormulario = true;
                  this.modoCadastro = false;  
                  this.usuario.senha = "";
                })
                .catch((error) => {
                  console.error("Erro ao obter usuario:", error);
                  this.$toast.error("Erro ao carregar usuario para edição");
                });
            },
            disabled: (item) => item.status === "Inativo"
          },
          {
            icon: "mdi-account-off",
            label: "Inativar usuario",
            handler: (item) => {
              this.usuario = item;
              this.inativarUsuario();
            },
            disabled: (item) => item.status === "Inativo"
          },
          
        ],
        usuarios: [], 
        usuario: new Usuario(),
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
      usuariosComRoleDescricao() {
      return this.usuarios.map((usuario) => ({
        ...usuario,
        roleDescricao: new Usuario(usuario).roleDescricao,
        status: this.verificarStatus(usuario),
      }));
    },
    },
    methods: {
      exibirUsuarios() {
        usuarioService
          .obterTodos()
          .then((response) => {
            this.usuarios = response.data;
          })
          .catch((error) => {
            console.error("Erro ao obter usuarios:", error);
          });
      },
      verificarStatus(usuario) {
      return usuario.dataInativacao ? "Inativo" : "Ativo";
    },
      formularioUsuario() {
        this.mostrarFormulario = true;
        this.modoCadastro = true;
        this.usuario = new Usuario({ role: 1 });
      },
      cancelarFormulario() {
        this.mostrarFormulario = false;
      },
      cadastrarUsuario() {
        if (!this.usuario.modeloValidoParaCadastro()) {
          this.$toast.warning("Verifique os campos obrigatórios!");
          return;
        }
  
        usuarioService
          .cadastrar(this.usuario)
          .then(() => {
            this.$toast.success("Usuário cadastrado com sucesso!");
            this.exibirUsuarios();
            this.usuario = new Usuario(); 
            this.mostrarFormulario = false;
          })
          .catch((error) => {
            console.error("Erro ao cadastrar usuario:", error);
            this.$toast.error("Erro ao cadastrar usuario");
          });
      },
      atualizarUsuario() {
        if (!this.usuario.modeloValidoParaAtualizar()) {
    this.$toast.warning("Verifique os campos obrigatórios!");
    return;
  }

  if (!this.modoCadastro && !this.usuario.senha) {
    delete this.usuario.senha; 
  }

  usuarioService
    .atualizar(this.usuario)
    .then(() => {
      this.$toast.success("Usuário atualizado com sucesso!");
      this.exibirUsuarios();
      this.mostrarFormulario = false;
    })
    .catch((error) => {
      console.error("Erro ao atualizar usuario:", error);
      this.$toast.error("Erro ao atualizar usuario.");
    });
      },
      salvarUsuario() {
        this.$refs.form.validate();
        if (this.modoCadastro) {
          this.cadastrarUsuario();
        } else {
          this.atualizarUsuario(); 
        }
      },
      inativarUsuario() {
        usuarioService
          .inativar(this.usuario)
          .then(() => {
            this.$toast.success("Usuário inativado com sucesso!");
            this.exibirUsuarios();
          })
          .catch((error) => {
            console.error("Erro ao inativar usuário:", error);
            this.$toast.error("Erro ao inativar usuário.");
          });
      },
    },
    created() {
      this.exibirUsuarios();
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
  