<template>
  <v-container>
    <v-row class="d-flex justify-center align-center pagina" >
      <v-col cols="12" md="4">
        <img src="../assets/AcessoNegado403.svg" />
      </v-col>
      <v-col cols="12" sm="8" md="8">
        <h1>Ops... Acesso Negado</h1>

        <p>Essa página é permitida somente para administradores.</p>

        <v-btn text color="var(--primary-color)" @click="redirecionarParaDashboard">
          Voltar para o Dashboard
        </v-btn>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "AcessoNegado",
  methods: {
    redirecionarParaDashboard() {
      this.$router.push({ name: "Dashboard" });
    },
  },
};
</script>

<style scoped>

.pagina {
  height: 70vh;
}
</style>
