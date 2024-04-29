import { createApp, reactive } from 'vue'
import App from './App.vue'
// @ts-ignore
import router from './Routes/routes.js'

const app = createApp(App)

const globalState = reactive({
  userRoles: []
})

app.provide('globalState', globalState)
app.use(router)

app.mount('#app')
