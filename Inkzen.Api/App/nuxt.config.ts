// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
    ssr: true,
    modules: [
        '@inkline/plugin/nuxt',
        '@vueuse/nuxt',
        "@nuxt/image",
        "@nuxt/eslint",
        "@nuxtjs/sitemap",
        "nuxt-viewport"
    ],
    inkline: {
        globals: {
            color: 'dark',                         // Default color variant
            colorMode: 'dark',               // Default color mode: 'system' | 'light' | 'dark' | string
            colorModeStrategy: 'localStorage', // Default color mode startegy: 'localStorage' | string
            componentOptions: {},              // Component specific global overrides
            locale: 'en',                      // Default translation
            renderMode: 'universal'
        }
    },
    viewport: {
        breakpoints: {
            xs: 0,
            sm: 576,
            md: 768,
            lg: 992,
            xl: 1200,
            '2xl': 1400,
        },

        defaultBreakpoints: {
            desktop: 'lg',
            mobile: 'xs',
            tablet: 'md',
        },

        fallbackBreakpoint: 'lg'
    },
    devtools: { enabled: true },
    nitro: {
        prerender: {
            crawlLinks: true
        },
    },
    runtimeConfig: {
        public: {
            baseUrl: process.env.VITE_BASEURL,
            service: process.env.VITE_SERVICE,
            api: {
                getAllProjects: process.env.VITE_API_GETALLPROJECTS,
                getProject: process.env.VITE_API_GETPROJECT,
                getAllTeam: process.env.VITE_API_GETALLTEAM
            }
        }
    },
    site: {
        url: 'https://test.inkzen.art',
    },
});