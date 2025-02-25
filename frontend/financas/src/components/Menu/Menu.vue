<template>
  <v-container>
    <v-navigation-drawer
      v-model="drawer"
      :mini-variant="mini"
      permanent
      dark
      app
      class="nav-bar"
      width="12vw"
    >
      <v-container>
        <v-row>
          <v-col>
            <v-btn icon @click.stop="toggleMini">
              <v-icon>mdi-menu</v-icon>
            </v-btn>
          </v-col>
        </v-row>
      </v-container>

      <v-list dense>
        <!-- <v-list-item router to="/">
          <v-list-item-action>
            <v-icon>mdi-home</v-icon>
          </v-list-item-action>
          <v-list-item-content >
            <v-list-item-title class="nav-title">Tela inicial</v-list-item-title>
          </v-list-item-content>
        </v-list-item> -->
        <v-list-item router to="/fluxo-de-caixa">
          <v-list-item-action class="mr-2">
            <v-icon>mdi-finance</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title class="nav-title"
              >Fluxo de Caixa</v-list-item-title
            >
          </v-list-item-content>
        </v-list-item>
        <!-- <v-list-item router to="/usuarios">
          <v-list-item-action>
            <v-icon>mdi-account-multiple</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title class="nav-title"
              >Usuários</v-list-item-title
            >
          </v-list-item-content>
        </v-list-item> -->
      </v-list>

      <v-container class="nav-footer">
        <v-list dense>
          <!-- <v-list-item>
            <v-list-item-action>
              <v-icon>mdi-theme-light-dark</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title class="nav-title">Alterar tema</v-list-item-title>
            </v-list-item-content>
          </v-list-item> -->
          <v-list-item @click="logout">
            <v-list-item-action class="mr-2">
              <v-icon>mdi-logout</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title class="nav-title">Deslogar</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-container>
    </v-navigation-drawer>

    
  </v-container>
</template>

<script>
import utilStorage from "@/utils/storage";

export default {
  name: "Menu",
  data: () => ({
    drawer: true,
    interval: {},
    value: 0,
    mini: true,
  }),

  methods: {
    toggleMini() {
      this.mini = !this.mini;
    },
    logout() {
      utilStorage.removerStorage();
      utilStorage.removerTokenNaStorage();

      this.$router.replace({ path: "/login" });
    },
  },
};
</script>
<style scoped>
.nav-bar {
  background-color: var(--primary-color);
}

.nav-title {
  margin-left: 10px;
}

.nav-footer {
  position: absolute;
  bottom: 0;
  padding: 0;
}
</style>
