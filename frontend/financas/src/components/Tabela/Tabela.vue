<!-- components/Tabela.vue -->
<template>
    <v-data-table
      :headers="headers"
      :items="items"
      :items-per-page="-1"
      class="elevation-0 mx-8 my-4 custom-scroll"
      no-title
      fixed-header
      height="520px"
    >
      <template v-slot:[`item.acoes`]="{ item }">
        <v-menu offset-y>
          <template v-slot:activator="{ on, attrs }">
            <v-icon
              small
              v-bind="attrs"
              v-on="on"
            >
              mdi-dots-vertical
            </v-icon>
          </template>
          <v-list>
            <!-- Renderizando as ações dinamicamente com ícones -->
            <v-list-item
              v-for="(acao, index) in actions"
              :key="index"
              @click="acao.handler(item)"
            >
              <v-icon left>{{ acao.icon }}</v-icon> <!-- Exibindo o ícone da ação -->
              <v-list-item-title>{{ acao.label }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </template>
    </v-data-table>
  </template>
  
  <script>
  export default {
    name: 'Tabela',
    props: {
      headers: {
        type: Array,
        required: true,
      },
      items: {
        type: Array,
        required: true,
      },
      actions: {
        type: Array,
        default: () => [], // Ações que serão passadas para o componente
      },
    },
  };
  </script>
  
  <style scoped>
  .custom-scroll .v-data-table__wrapper {
    scrollbar-width: thin; /* Padrão para Firefox */
    scrollbar-color: #888 #f5f5f5; /* Cor do scroll e fundo */
  }
  
  /* Estiliza a barra de rolagem para Chrome, Edge e Safari */
  .custom-scroll .v-data-table__wrapper::-webkit-scrollbar {
    width: 8px; /* Largura da barra de rolagem */
  }
  
  .custom-scroll .v-data-table__wrapper::-webkit-scrollbar-track {
    background: #f5f5f5; /* Cor do fundo da rolagem */
    border-radius: 10px;
  }
  
  .custom-scroll .v-data-table__wrapper::-webkit-scrollbar-thumb {
    background: #888; /* Cor do scroll */
    border-radius: 10px;
    transition: background 0.3s ease;
  }
  
  .custom-scroll .v-data-table__wrapper::-webkit-scrollbar-thumb:hover {
    background: #555; /* Cor mais escura ao passar o mouse */
  }
  </style>
  