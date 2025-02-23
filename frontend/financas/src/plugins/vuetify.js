import Vue from "vue";
import Vuetify from "vuetify/lib/framework";
import "../assets/styles/global.css";
import '@mdi/font/css/materialdesignicons.css';

Vue.use(Vuetify);

export default new Vuetify({
  icons: {
    iconfont: 'mdi',
  },
  lang: {
    locales: { 'pt-br': require('vuetify/es5/locale/pt').default },
    current: 'pt-br',
  },
});
