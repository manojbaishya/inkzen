import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

import inkline from '@inkline/plugin/vite';

const inklineConfig = {
    outputDir: 'src/css/variables'
};

// https://vitejs.dev/config/
export default defineConfig({
    plugins:
        [
            inkline(inklineConfig),
            vue()
        ],
    build: {
        outDir: '../wwwroot'
    },
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        port: 9000
    }
});