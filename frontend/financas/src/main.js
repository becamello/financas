import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import "@/assets/styles/global.css";
import '@mdi/font/css/materialdesignicons.css';
import router from './router';
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-bootstrap.css';

Vue.use(VueToast, {
  position: 'top-right',
  duration: 3000,
});
Vue.config.productionTip = false

new Vue({
  vuetify,
  render: h => h(App),
  router
}).$mount('#app')
