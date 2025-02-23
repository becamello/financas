<template>
  <v-container fluid class="d-flex align-center justify-center pa-0 container-login">
    <!-- Parte da esquerda -->
    <v-col cols="12" md="6" class="pa-0 d-flex justify-center align-center" style="height: 100vh">
      <div>
        <img src="../assets/logo.svg" alt="FINANÇAS, seu controle financeiro">
      </div>
    </v-col>

    <!-- Parte da direita -->
    <v-col cols="12" md="6" class="pa-10 d-flex align-center justify-center">
      <v-card elevation="5" class="card-form-login">
        <v-card-title class="text-center login-title pb-10 pa-0">BEM-VINDO!</v-card-title>
        <v-card-text class="pa-0">
          <Input
            label="E-mail"
            placeHolder="insira seu e-mail de acesso"
            v-model="usuario.email"
          />
          <Input
            label="Senha"
            placeHolder="123456"
            type="password"
            v-model="usuario.senha"
          />
        </v-card-text>
        <v-card-actions class="pa-0 d-flex align-center justify-center">
          <v-btn
            color="var(--primary-color)"
            dark
            class="btn-login"
            @click="login"
            >ENTRAR</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-col>
  </v-container>
</template>

<script>
import Input from "@/components/Input/Input.vue";
import usuarioService from "@/services/usuario-service";
import utilStorage from "@/utils/storage";

export default {
  name: "Login",
  components: {
    Input,
  },
  data() {
    return {
      usuario: {
        email: "",
        senha: "",
      },
    };
  },
  methods: {
    login() {
      if (!this.usuario.email || !this.usuario.senha) {
        alert("E-mail e senha do usuário são obrigatórios!")
        return;
      }

      usuarioService
        .login(this.usuario.email, this.usuario.senha)
        .then((response) => {
          
          utilStorage.salvarStorage(response.data.usuario);
          utilStorage.salvarTokenNaStorage(response.data.token);

          
          this.$router.push({ name: "Dashboard" });
        })
        .catch((error) => {
          
          console.error(error);
          alert("E-mail ou senha estão incorretos. Por favor, verifique as informações e tente novamente.")
        });
    },
  }
};
</script>

<style scoped>
.container-login {
  background-image: url("../assets/background-login.svg");
  background-size: cover; /* A imagem cobrirá toda a tela */
  background-position: center center; /* A imagem ficará centralizada */
  background-repeat: no-repeat; /* A imagem não será repetida */
  height: 100vh; /* A altura da tela será 100% */
  margin: 0; /* Remove a margem do body */
  height: 100vh;
  width: 100vw;
}

.card-form-login {
  width: 90%;
  height: 80vh;
  padding: 5rem;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border-radius: 2rem;
}

.login-title {
  font-size: 3.5rem;
  color: var(--primary-color);
  font-weight: 900;
  text-align: center;
}

.btn-login {
  width: 16vw;
  padding: 60px;
  font-size: 20px;
}

</style>
