import Login from '@/views/Login.vue';

const routes = [
    {
      path: "/login",
      name: "Login",
      component: Login,
      title: "Login",
      meta: { requiredAuth: false },
    },
];

export default routes;