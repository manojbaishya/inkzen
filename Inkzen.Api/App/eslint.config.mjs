import vue from 'eslint-plugin-vue';
import js from "@eslint/js";

export default [
    ...vue.configs['flat/recommended'],
    'eslint:recommended',
    js.configs.recommended,
    {
        rules: {
            semi: ["error", "always"]
        }
    },
    {
        languageOptions: {
            parserOptions: {
                ecmaVersion: 'latest',
                sourceType: 'module'
            }
        }
    }
];
