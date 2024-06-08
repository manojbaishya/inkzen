import { defineStore } from 'pinia';
import { computed } from 'vue';

export const useContextStore = defineStore('context', () => {
    const appconfig = computed(() => {
        return {
            service: import.meta.env.VITE_SERVICE,
            baseUrl: import.meta.env.VITE_BASEURL,
            imagewidth: {
                getAllProjects: import.meta.env.VITE_API_GETALLPROJECTS,
                getProject: import.meta.env.VITE_API_GETPROJECT
            }
        }
    });

    return { appconfig };
});