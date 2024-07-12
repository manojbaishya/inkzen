<script setup>
import { extractFilename } from '../utils/images.mjs';

const config = useRuntimeConfig();

const getAllProjects = `${config.public.baseUrl}/${config.public.api.getAllProjects}`;

const { data: projects } = await useFetch(getAllProjects);

</script>

<template>
    <div>
        <div
            id="project-page"
            class="_margin-x:auto!"
        >
            <div id="project-page-title">
                <p class="_font-weight:light! _margin-top:4">
                    Projects
                </p>
                <p
                    class="_font-weight:light!"
                    style="align-self: flex-end;"
                >
                    ⌾
                </p>
            </div>
            <div
                id="project-grid"
                class="_margin-top:8"
            >
                <div
                    v-for="project in projects"
                    :key="project.id"
                    class="project-thumbnail-frame"
                >
                    <NuxtLink :to="{ name: 'project-id', params: { id: project.id } }">
                        <div class="thumbnail-container">
                            <span class="project-title">{{ project.title }}</span>
                            <NuxtImg
                                class="project-thumbnail"
                                :src="`/Projects/${extractFilename(project.primaryImage.media.publicUrl)}`"
                            />
                        </div>
                    </NuxtLink>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
@import '@inkline/inkline/css/mixins';

#project-page {
    max-width: 90%;

    #project-page-title {
        display: flex;
        justify-content: space-between;

        >p {
            font-size: calc(var(--font-size) * var(--scale-ratio-pow-5) * var(--scale-ratio-pow-4));

            @include breakpoint-down('sm') {
                font-size: calc(var(--font-size) * var(--scale-ratio-pow-5) * var(--scale-ratio-pow-3));
            }

            @include breakpoint-down('xs') {
                font-size: calc(var(--font-size) * var(--scale-ratio-pow-5) * var(--scale-ratio-pow-2));
            }

            @media screen and (max-width: 380px) {
                font-size: calc(var(--font-size) * var(--scale-ratio-pow-5) * var(--scale-ratio-pow-1));
            }
        }


    }

    #project-grid {
        --project-grid-width: 500px;

        @include breakpoint-down('lg') {
            --project-grid-width: 500px;
        }
        @include breakpoint-down('md') {
            --project-grid-width: 500px;
        }
        @include breakpoint-down('sm') {
            --project-grid-width: 400px;
        }
        @include breakpoint-down('xs') {
            --project-grid-width: 300px;
        }

        display: grid;
        grid-template-rows: repeat(auto-fill, var(--project-grid-width));
        grid-template-columns: repeat(auto-fill, var(--project-grid-width));
        row-gap: 8rem;
        column-gap: 3rem;
        justify-items: center;
        align-items: center;
        align-content: space-evenly;
        justify-content: space-evenly;

        .project-thumbnail-frame {
            overflow: hidden;

            .thumbnail-container {
                position: relative;

                .project-title {
                    position: absolute;
                    display: none;
                }

                .project-thumbnail {
                    object-fit: cover;
                    width: var(--project-grid-width);
                    height: var(--project-grid-width);
                    cursor: pointer;
                    transition: all .5s ease;
                    transform-origin: center;
                    backface-visibility: hidden;

                    border: thin solid #fc778a;
                }
            }

            &:hover {
                .thumbnail-container {
                    .project-title {
                        display: block;
                        position: absolute;
                        top: 0;
                        left: 0;
                        padding: 1rem;
                        font-size: 2rem;
                        font-weight: 300;
                        color: white;
                        backdrop-filter: blur(14px) brightness(80%);
                        z-index: 1;
                    }

                    .project-thumbnail {
                        filter: brightness(50%);
                        transform: scale(1.05);
                    }
                }

            }
        }
    }
}
</style>