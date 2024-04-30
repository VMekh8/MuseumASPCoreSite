import { createRouter, createWebHistory } from "vue-router";

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/login',
            component: () => import('@/layouts/UserLayout/Login.vue'),
        },
        {
            path: '/register',
            component: () => import('@/layouts/UserLayout/Register.vue'),
        },
    ],
})