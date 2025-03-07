<template>
  <v-data-table
    :headers="headers"
    :items="items"
    :items-per-page="-1"
    class="elevation-0 custom-scroll"
    no-title
    fixed-header
    :height="height"
    :hide-default-footer="hideFooter"
  >
    <template v-slot:[`item.acoes`]="{ item }">
      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon v-bind="attrs" v-on="on">
            <v-icon small>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item
            v-for="(acao, index) in actions"
            :key="index"
            @click="acao.handler(item)"
            :disabled="acao.disabled && acao.disabled(item)" 
          >
            <v-list-item-icon>
              <v-tooltip top>
                <template v-slot:activator="{ on: tooltipOn, attrs: tooltipAttrs }">
                  <v-icon
                    small
                    v-bind="tooltipAttrs"
                    v-on="tooltipOn"
                    class="icon-table"
                  >
                    {{ acao.icon }}
                  </v-icon>
                </template>
                <span>{{ acao.label }}</span>
              </v-tooltip>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>{{ acao.label }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-menu>
    </template>

    <template v-slot:[`item.status`]="{ item }">
      <v-chip :color="statusChipColor(item.status)" dark>
        {{ item.status }}
      </v-chip>
    </template>

  </v-data-table>
</template>

<script>
export default {
  name: "Tabela",
  props: {
    headers: {
      type: Array,
      required: true,
    },
    items: {
      type: Array,
      required: true,
    },
    actions: {
      type: Array,
      default: () => [],
    },
    height: {
      type: Number,
    },
    width: {
      type: Number,
    },
    hideFooter: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    statusChipColor(status) {
      return status === "Ativo" ? "green" : "red";
    },
  },
};
</script>

<style scoped>
.icon-table {
  cursor: pointer;
}
</style>
