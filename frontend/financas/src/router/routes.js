import Login from '@/views/Login.vue';
import FluxoDeCaixa from '@/views/FluxoDeCaixa.vue';
import Titulos from '@/views/Titulos.vue';
import Dashboard from '@/views/Dashboard.vue';
import AcessoNegado from '@/views/AcessoNegado.vue';
import ControleUsuario from '@/views/ControleUsuario.vue';

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
    {
      path: "/acesso-negado",
      name: "AcessoNegado",
      component: AcessoNegado,
      title: "Acesso Negado",
      meta: { requiredAuth: false }, 
    },
    {
      path: "/usuarios",
      name: "Usuários",
      component: ControleUsuario,
      title: "Usuários",
      meta: { requiredAuth: true, isAdmin: true }, 
    },

];

export default routes;