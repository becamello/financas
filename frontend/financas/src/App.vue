<template>
  <v-app>
    <router-view></router-view>
  </v-app>
</template>

<script>
export default {
  name: 'App',

  components: {
    //
  },

  data: () => ({
    //
  }),
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

</style>
