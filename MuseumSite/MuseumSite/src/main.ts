import { createApp, reactive } from 'vue'
import App from './App.vue'
import router from './Routes/routes'


const app = createApp(App)

const globalState = reactive({
  userRoles: []
})

app.provide('globalState', globalState)
app.use(router)

app.mount('#app')
