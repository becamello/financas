<template>
    <v-menu
      v-model="menu"
      :close-on-content-click="false"
      offset-y
      nudge-bottom="0"
      
      max-width="290px"
      
    >
      <template v-slot:activator="{ on, attrs }">
        <v-text-field
        color="var(--terciary-color)"
          v-model="formattedDate"
          :label="label"
          prepend-inner-icon="mdi-calendar"
          readonly
          v-bind="attrs"
          v-on="on"
          outlined
          clearable
          @click:clear="clearDate"
          height="0px"
        ></v-text-field>
      </template>
      <v-date-picker
        v-model="internalDate"
        type="month"
        locale="pt-br"
        @input="selectDate"
        no-title
        :show-current="false"
        color="var(--primary-color)"
      ></v-date-picker>
    </v-menu>
  </template>
  
  <script>
  export default {
    props: {
      value: String,
      label: {
        type: String,
        default: 'Data de Cadastro'
      }
    },
    data() {
      return {
        menu: false,
        internalDate: this.value
      };
    },
    computed: {
      formattedDate() {
        if (!this.internalDate) return '';
        const [year, month] = this.internalDate.split('-');
        return `${month}/${year}`;
      }
    },
    watch: {
      value(newValue) {
        this.internalDate = newValue;
      }
    },
    methods: {
      selectDate(date) {
        this.menu = false;
        this.$emit('input', date);
      },
      clearDate() {
        this.internalDate = null;
        this.$emit('input', null);
      }
    }
  };
  </script>
  
  <style scoped>
  .v-input {
    flex: 0!important;
  }

  </style>