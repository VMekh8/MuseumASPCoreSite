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
            component: () => import('@/layouts/ControlPanel/MainControl.vue'),
            children: [
                    {
                        path: 'exhibitcontrol',
                        component: () => import('@/layouts/ControlPanel/ExhibitControl.vue')
                    },
                    {
                        path: 'exhibitioncontrol',
                        component: () => import('@/layouts/ControlPanel/ExhibitionControl.vue')
                    },
                    {
                        path: 'museumnewscontrol',
                        component: () => import('@/layouts/ControlPanel/NewsControl.vue')
                    },
                    {
                        path: 'usercontrol',
                        component: () => import('@/layouts/ControlPanel/UserControl.vue')
                    },
                    {
                        path: 'addmuseumnews',
                        component: () => import('@/layouts/AddPages/AddMuseumNews.vue')
                    },
                    {
                        path: 'addexhibit',
                        component: () => import('@/layouts/AddPages/AddExhibit.vue')
                    },
                    {
                        path: 'addexhibition',
                        component: () => import('@/layouts/AddPages/AddExhibition.vue')
                    }
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