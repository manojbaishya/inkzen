import vue from 'eslint-plugin-vue';
import js from "@eslint/js";
import gitignore from 'eslint-config-flat-gitignore';
import withNuxt from './.nuxt/eslint.config.mjs';

export default withNuxt(
    gitignore(),
    {
        files: ["**/*.js", "**/*.jsx", "**/*.vue", "**/*.cjs", "**/*.mjs"]
    },
    js.configs.recommended,
    ...vue.configs['flat/recommended'],
    {
        languageOptions: {
            parserOptions: {
                ecmaVersion: 'latest',
                sourceType: 'module'
            }
        },
        rules: {
            semi: ["error", "always"],
            indent: ["error", 4],
            "vue/html-indent": ["error", 4, {
                "attribute": 1,
                "baseIndent": 1,
                "closeBracket": 0,
                "alignAttributesVertically": true,
                "ignores": []
            }],
            "vue/multi-word-component-names": ["error", {
                "ignores": ['index', 'projects', 'services', 'about', '[id]']
            }]
        }
    }
);
