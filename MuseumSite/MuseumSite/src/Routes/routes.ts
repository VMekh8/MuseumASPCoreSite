import { createRouter, createWebHistory } from 'vue-router'
import Register from '../layouts/UserLayout/Register.vue'

const routes = [
  {
    path: '/register',
    name: 'Register',
    component: Register
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
