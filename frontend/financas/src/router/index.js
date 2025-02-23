import Vue from "vue";
import VueRouter from "vue-router";
import routes from "./routes";

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
  } else if (to.name === "Fluxo de Caixa") {
    if (!token || token === "undefined" || token === "") {
      next({
        path: "/login",
        query: { nextUrl: to.fullPath },
      });
    } else {
      next();
    }
  } else {
    next();
  }
});

export default router;
