<template>
    <v-container>
      <v-text-field
        outlined
        color="var(--terciary-color)"
        :label="label"
        :placeholder="placeHolder"
        :type="type"
        v-model="valor"
      ></v-text-field>
    </v-container>
  </template>
  
  <script>
  export default {
    name: "Input",
    props: {
      label: { type: String, required: true },
      placeHolder: { type: String },
      type: { type: String, default: "text" },
      value: { type: String },
    },
    data() {
      return {
        valor: this.value
      };
    },
    watch: {
      valor(newValue) {
        this.$emit("input", newValue);
      },
    },
  };
  </script>
  