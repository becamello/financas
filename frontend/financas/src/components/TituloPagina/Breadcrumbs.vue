<template>
    <v-container style="width: 400px;" class="ma-0 pa-0">
      <v-breadcrumbs :items="breadcrumbs" class="ma-0 pa-0" ></v-breadcrumbs>
      <router-view></router-view>
    </v-container>
  </template>
  
  <script>
  export default {
    data() {
      return {
        breadcrumbs: []
      };
    },
    watch: {
      $route(to) {
        this.setBreadcrumbs(to);
      }
    },
    methods: {
      setBreadcrumbs(route) {
        const matchedRoutes = route.matched;
        this.breadcrumbs = matchedRoutes.map((record) => {
          return {
            text: record.meta.title || record.name,
            disabled: false,
            href: this.$router.resolve(record).href
          };
        });
      }
    },
    created() {
      this.setBreadcrumbs(this.$route);
    }
  };
  </script>
  
  <style>
  .v-breadcrumbs__item {
  font-size: 28px;
  color: var(--color-default) !important;;
  font-weight: bold;
}

.v-breadcrumbs__item--active {
  font-weight: bold;
  color: var(--color-default) !important;; 
}
  </style>