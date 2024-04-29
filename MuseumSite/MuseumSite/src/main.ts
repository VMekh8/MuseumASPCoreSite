import { createApp, reactive } from 'vue'
import App from './App.vue'

const app = createApp(App)

const globalState = reactive({
  userRoles: []
})

app.provide('globalState', globalState)

app.mount('#app')
