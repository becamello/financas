<template>
    <v-main>
      <v-container fluid class="pa-6">
        <Breadcrumbs />
      </v-container>

    </v-main>
  </template>
  
  <script>
import Breadcrumbs from '@/components/TituloPagina/Breadcrumbs.vue';

  export default {
    name: "Usuario",
    components: {
      Breadcrumbs
    }
  };
  </script>
  
  <style scoped>
  /* Estilos customizados para a página de acesso negado */
  </style>
  