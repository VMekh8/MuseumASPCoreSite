import { createApp, reactive } from 'vue'
import App from './App.vue'
import router from './router'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'


const app = createApp(App)

const globalState = reactive({
  userRoles: []
})

app.provide('globalState', globalState)

app.use(router).mount('#app')
