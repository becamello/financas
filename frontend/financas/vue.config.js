const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  devServer: {
    port: 8089
  },
  transpileDependencies: [
    'vuetify'
  ]
})
