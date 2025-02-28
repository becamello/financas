<template>
  <v-app>
    <Menu v-if="this.$router.currentRoute.name != 'Login'" />
    <router-view />
  </v-app>
</template>

<script>
import Menu from "./components/Menu/Menu.vue";

export default {
  name: "App",

  components: {
    Menu,
  },

  data() {
    return {
      // 
    };
  },
};
</script>

<style>
* {
  box-sizing: border-box;
}

html {
  height: 100%;
  margin: 0;
  overflow-y: auto !important;
}
#app {
  min-height: 100vh; /* Garante que o contêiner principal ocupe toda a altura da tela */
}

.v-toast__item {
  font-family: "Roboto", sans-serif !important;
}
</style>
