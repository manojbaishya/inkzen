import { createApp } from 'vue';
import { Inkline, components } from '@inkline/inkline';
import { createPinia } from 'pinia';
import router from './router';
import App from './App.vue';

import './css/variables/index.scss';
import '@inkline/inkline/css/index.scss';
import '@inkline/inkline/css/utilities.scss';

createApp(App)
    .use(router)
    .use(createPinia())
    .use(Inkline, { components })
    .mount('#app');