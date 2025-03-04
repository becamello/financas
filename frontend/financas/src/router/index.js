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

  if (to.name === "Login") {
    if (token) {
      next({ name: "Fluxo de Caixa" }); 
    } else {
      next();
    }
  } else if (to.meta.requiredAuth) {
    if (!token || token === "undefined" || token === "") {
      next({
        path: "/login",
        query: { nextUrl: to.fullPath }, 
      });
    } else {
      try {
        const decodedToken = jwtDecode(token);
        const role = decodedToken.role;


        if (to.meta.isAdmin && role !== "Admin") {
          next({ name: "AcessoNegado" });
        } else {
          next();
        }
      } catch (error) {
        console.error("Erro ao decodificar o token:", error);
        next({ name: "Login" }); 
      }
    }
  } else {
    next(); 
  }
});


export default router;
