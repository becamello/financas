import Login from '@/views/Login.vue';
import FluxoDeCaixa from '@/views/FluxoDeCaixa.vue';

const routes = [
    {
      path: "/login",
      name: "Login",
      component: Login,
      title: "Login",
      meta: { requiredAuth: false },
    },
    {
      path: "/fluxo-de-caixa",
      name: "Fluxo de Caixa",
      component: FluxoDeCaixa,
      title: "Fluxo de Caixa",
      meta: { requiredAuth: false },
    },
];

export default routes;