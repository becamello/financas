import Login from '@/views/Login.vue';
import FluxoDeCaixa from '@/views/FluxoDeCaixa.vue';
import Titulos from '@/views/Titulos.vue';
import Dashboard from '@/views/Dashboard.vue';

const routes = [
    {
      path: "/login",
      name: "Login",
      component: Login,
      title: "Login",
      meta: { requiredAuth: false },
    },
    {
      path: "/",
      name: "Dashboard",
      component: Dashboard,
      title: "Dashboard",
      meta: { requiredAuth: false },
    },
    {
      path: "/fluxo-de-caixa",
      name: "Fluxo de Caixa",
      component: FluxoDeCaixa,
      title: "Fluxo de Caixa",
      meta: { requiredAuth: false },
    },
    {
      path: "/fluxo-de-caixa/novo-titulo",
      name: "Novo Titulo",
      component: Titulos,
      title: "Novo Título",
      meta: { requiredAuth: false },
    },
    {
      path: "/fluxo-de-caixa/editar-titulo/:id",
      name: "Editar Título",
      component: Titulos,
      title: "Editar Título",
      meta: { requiredAuth: false },
    },
];

export default routes;