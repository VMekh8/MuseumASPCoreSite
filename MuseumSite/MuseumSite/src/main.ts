import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
// @ts-ignore
import router from './Routes/routes.js'

<<<<<<< HEAD
const app = createApp(App)

const globalState = reactive({
  userRoles: []
})

app.provide('globalState', globalState)
app.use(router)

app.mount('#app')
=======
createApp(App).mount('#app')
>>>>>>> parent of d9d43a1 (Create a global array for user roles)
