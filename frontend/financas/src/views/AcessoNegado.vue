<template>
  <v-main>

    <v-container class="pagina d-flex align-center justify-center">
      <v-row class="text-center">
        <v-col cols="12" sm="4" md="5" class="d-flex justify-center">
          <img src="../assets/AcessoNegado403.svg" class="imagem-acesso-negado" />
        </v-col>
  
        <v-col cols="12" sm="8" md="7" class="d-flex flex-column align-center justify-center">
          <h1 class="titulo">Ops... Acesso Negado</h1>
          <p class="descricao">Essa página é permitida somente para administradores.</p>
  
          <v-btn color="var(--primary-color)" @click="redirecionarParaDashboard">
            Voltar para o Dashboard
          </v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
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
  min-height: 100vh; 
  padding: 2rem;
  text-align: center;
}

.imagem-acesso-negado {
  max-width: 100%;
}

@media (max-width: 600px) {
  .titulo {
    font-size: 1.8rem;
  }

  .descricao {
    font-size: 1rem;
    padding: 0 1rem;
  }

  .imagem-acesso-negado {
    max-width: 90%;
  }
}
</style>
