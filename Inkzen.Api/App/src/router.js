import { createRouter, createWebHistory } from 'vue-router';
import HomePage from './HomePage.vue';

export default createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'homepage',
            component: HomePage
        },
        {
            path: '/projects',
            name: 'projects',
            component: () => import('./ProjectsPage.vue')
        },
        {
            path: '/projects/:id',
            name: 'project-detail',
            component: () => import('./ProjectDetailPage.vue'),
            props: route => ({
                project: route.meta.data.project,
                categories: route.meta.data.categories,
                images: route.meta.data.images
            })
        },
        {
            path: '/services',
            name: 'services',
            component: () => import('./ServicesPage.vue')
        },
        {
            path: '/about',
            name: 'about',
            component: () => import('./AboutPage.vue')
        }
    ]
});
