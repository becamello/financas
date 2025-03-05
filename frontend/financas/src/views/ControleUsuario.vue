<template>
  <v-main>
    <v-container fluid class="pa-6 pt-0">
      <Breadcrumbs />

      
      <v-row class="d-flex justify-end">
        <v-btn
          color="var(--primary-color)"
          dark
          @click="abrirModalUsuario"
          class="ma-3"
        >
          Controle dos Usuários
        </v-btn>
      </v-row>
      <v-row class="d-flex justify-space-between">
        <v-col cols="12" md="6" sm="12">
          <v-card class="pa-5">
            <GraficoPizza
              :dados="dadosGraficoPizzaRole"
              :cores="cores"
              titulo="Usuários x Perfil"
              name="Usuários"
              pointFormat="Total: <b>{point.y}</b>"
            />
          </v-card>
        </v-col>

        <v-col cols="12" md="6" sm="12">
          <v-card class="pa-5">
            <GraficoPizza
              :dados="dadosGraficoPizzaStatus"
              :cores="cores"
              titulo="Usuários x Status"
              name="Usuários"
              pointFormat="Total: <b>{point.y}</b>"
            />
          </v-card>
        </v-col>
      </v-row>

      <ModalUsuario v-model="dialog" dialogWidth="70vw" />
    </v-container>
  </v-main>
</template>

<script>
import Breadcrumbs from "@/components/TituloPagina/Breadcrumbs.vue";
import usuarioService from "@/services/usuario-service";
import Usuario from "@/models/Usuario";
import ModalUsuario from "@/components/Usuario/ModalUsuario.vue";
import GraficoPizza from "@/components/Graficos/GraficoPizza.vue";

export default {
  name: "ControleUsuarios",
  components: {
    Breadcrumbs,
    ModalUsuario,
    GraficoPizza
  },
  data() {
    return {
      dialog: false,
      usuarios: [],
      dadosGraficoPizzaRole: [],
      dadosGraficoPizzaStatus: [],
    };
  },
  methods: {
    async fetchData() {
      try {
        const response = await usuarioService.obterTodos();
        const usuarios = response.data.map((usuario) => new Usuario(usuario));

        const usuariosPorRole = { Admin: 0, Usuario: 0 };
        const usuariosPorStatus = { Ativo: 0, Inativo: 0 };

        usuarios.forEach((usuario) => {
          const role = usuario.roleDescricao;
          if (role === "Admin") {
            usuariosPorRole.Admin++;
          } else if (role === "User") {
            usuariosPorRole.Usuario++;
          }

          const status = usuario.dataInativacao ? "Inativo" : "Ativo";
          if (status === "Ativo") {
            usuariosPorStatus.Ativo++;
          } else {
            usuariosPorStatus.Inativo++;
          }
        });

        this.dadosGraficoPizzaRole = [
          { name: "Admin", y: usuariosPorRole.Admin },
          { name: "User", y: usuariosPorRole.Usuario },
        ];

        this.dadosGraficoPizzaStatus = [
          { name: "Ativo", y: usuariosPorStatus.Ativo, color: '#4CAF50' },
          { name: "Inativo", y: usuariosPorStatus.Inativo, color: '#F44336' },
        ];
      } catch (error) {
        console.error("Erro ao buscar os usuários:", error);
      }
    },
    abrirModalUsuario() {
      this.modoCadastro = true;  
      this.usuarioAtual = new Usuario(); 
      this.dialog = true;
    },
  },
  watch: {
    dialog(novoValor) {
      if (!novoValor) {
        this.fetchData();
      }
    },
  },
  mounted() {
    this.fetchData(); 
  },
};
</script>
