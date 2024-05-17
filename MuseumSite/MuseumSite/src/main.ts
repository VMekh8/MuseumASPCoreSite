import { createApp, reactive } from 'vue'
import App from './App.vue'
import router from './router'
import { BootstrapVue3 } from 'bootstrap-vue-3'
import { BootstrapIconsPlugin } from 'bootstrap-icons-vue'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

const app = createApp(App)

const globalState = reactive({
  userRoles: []
})

app.provide('globalState', globalState)

app.use(router).use(BootstrapVue3).use(BootstrapIconsPlugin).mount('#app')
