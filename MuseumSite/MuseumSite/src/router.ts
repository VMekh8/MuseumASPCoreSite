import { createRouter, createWebHistory } from "vue-router";

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'Home',
            component: () => import('./App.vue'),
        },
        {
            path: '/controlpanel',
            component: () => import('@/layouts/ControlPanel/AdminPanel.vue'),
            children: [
                    {
                        path: 'exhibitcontrol',
                        component: () => import('@/layouts/ControlPanel/ExhibitsControl.vue')
                    },
            ]
        },
        {
            path: '/login',
            name: 'Login',
            component: () => import('@/layouts/UserLayout/Login.vue'),
        },
        {
            path: '/register',
            component: () => import('@/layouts/UserLayout/Register.vue'),
        },
    ],
})