<template>
  <v-container fluid class="container-login">
    <v-row class="fill-height d-flex align-center justify-center">
      <v-col
        cols="12"
        md="6"
        class="d-none d-md-flex justify-center align-center"
      >
        <img
          src="../assets/logo.svg"
          alt="FINANÇAS, seu controle financeiro"
          class="logo-login"
        />
      </v-col>

      <v-col cols="12" md="6" class="d-flex align-center justify-center">
        <v-card elevation="5" class="card-form-login">
          <v-card-title class="login-title">BEM-VINDO!</v-card-title>
          <v-card-text>
            <v-text-field
              outlined
              color="var(--color-default)"
              label="E-mail"
              placeholder="Insira seu e-mail de acesso"
              v-model="usuario.email"
            ></v-text-field>

            <v-text-field
                :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show ? 'text' : 'password'"
                outlined
                @click:append="show = !show"
                name="Senha"
                label="Senha"
                v-model="usuario.senha"
                color="var(--color-default)"
              ></v-text-field>
          </v-card-text>
          <v-card-actions class="d-flex align-center justify-center">
            <v-btn
              color="var(--primary-color)"
              dark
              class="btn-login"
              @click="login"
              :disabled="isLoading"
            >
              ENTRAR
            </v-btn>
          </v-card-actions>

          <v-overlay v-if="isLoading" absolute>
            <v-progress-circular
              indeterminate
              color="var(--primary-color)"
              size="30"
            ></v-progress-circular>
          </v-overlay>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import usuarioService from "@/services/usuario-service";
import utilStorage from "@/utils/storage";

export default {
  name: "Login",

  data() {
    return {
      show: false,
      usuario: {
        email: "",
        senha: "",
      },
      isLoading: false,
    };
  },
  methods: {
    login() {
      if (!this.usuario.email || !this.usuario.senha) {
        this.$toast.warning("E-mail e senha do usuário são obrigatórios!");
        return;
      }

      this.isLoading = true;

      usuarioService
        .login(this.usuario.email, this.usuario.senha)
        .then((response) => {
          this.isLoading = false;

          utilStorage.salvarStorage(response.data.usuario);
          utilStorage.salvarTokenNaStorage(response.data.token);

          this.$router.push({ name: "Fluxo de Caixa" });
        })
        .catch((error) => {
          console.error(error);
          this.isLoading = false;
          this.$toast.error("Erro ao realizar login! Verifique e-mail e senha");
        });
    },
  },
};
</script>

<style scoped>
.container-login {
  background-image: url("../assets/background-login.svg");
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  height: 100vh;
  width: 100vw;
  display: flex;
  justify-content: center;
  align-items: center;
}

.logo-login {
  width: 60%;
}

.card-form-login {
  width: 80%;
  padding: 3rem;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border-radius: 2rem;
  position: relative;
}

.login-title {
  font-size: 2.5rem;
  color: var(--primary-color);
  font-weight: 900;
  text-align: center;
  padding-bottom: 4rem;
}

.btn-login {
  width: 200px; 
  font-size: 18px;
}

@media (max-width: 768px) {
  .container-login {
    background-size: cover;
    background-position: center;
  }

  .login-title {
    font-size: 2rem;
  }

  .btn-login {
    width: 160px;
    font-size: 16px;
  }

  .card-form-login {
    padding: 2rem;
    width: 95%;
  }
}
</style>
