import Vue from "vue";
import VueRouter from "vue-router";
import routes from "./routes";
import { jwtDecode } from "jwt-decode";

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  routes,
});

router.beforeEach((to, from, next) => {
  let token = localStorage.getItem("token");

  // Se a rota requer login, verificamos se o token está presente
  if (to.name === "Login") {
    if (token) {
      next({ name: "Fluxo de Caixa" }); // Redireciona para a página principal se o usuário já estiver logado
    } else {
      next(); // Caso contrário, permite o acesso ao login
    }
  } else if (to.meta.requiredAuth) {
    // Verifica se a rota requer autenticação
    if (!token || token === "undefined" || token === "") {
      next({
        path: "/login",
        query: { nextUrl: to.fullPath }, // Mantém a URL para redirecionamento após o login
      });
    } else {
      // Decodifica o token para obter as informações do payload (incluindo o 'role')
      try {
        const decodedToken = jwtDecode(token); // Decodificando o token
        const role = decodedToken.role; // Acessando a propriedade 'role' do payload

        // Verifica se a rota requer um usuário Admin
        if (to.meta.isAdmin && role !== "Admin") {
          next({ name: "AcessoNegado" }); // Redireciona para a página de acesso negado
        } else {
          next(); // Caso contrário, permite o acesso
        }
      } catch (error) {
        console.error("Erro ao decodificar o token:", error);
        next({ name: "Login" }); // Redireciona para a página de login se o token estiver corrompido ou inválido
      }
    }
  } else {
    next(); // Se a rota não exigir autenticação ou papel específico, permite o acesso
  }
});


export default router;
