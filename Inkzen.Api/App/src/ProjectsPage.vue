<script setup>
import { useFetch } from '@vueuse/core';
import { useContextStore } from './context';
import { ref } from 'vue';


const ctx = useContextStore();
const getAllProjects = `${ctx.appconfig.baseUrl}/${ctx.appconfig.imagewidth.getAllProjects}`;

const { onFetchResponse, onFetchError } = useFetch(getAllProjects).get();

let projects = ref([]);

onFetchResponse(async response => {
    const projectsData = await response.json();

    projectsData.forEach(project => {
        project.primaryImage.media.publicUrl = project.primaryImage.media.publicUrl.replace('~', ctx.appconfig.service);
    });

    projects.value = projectsData;
});

onFetchError(error => {
    console.error(`ERROR: ${error.message}`);
})

</script>

<template>
    <div id="project-page" class="_margin-x:auto!">
        <div id="project-page-title">
            <div class="d2 font-weight_light _margin-top:4">Projects</div>
            <div class="d2 font-weight_light" style="align-self: flex-end;">⌾</div>
        </div>
        <div class="project-grid _margin-top:8">
            <div class="project-thumbnail-frame" v-for="project in projects" :key="project.id">
                <router-link :to="{ name: 'project-detail', params: { id: project.id } }">
                    <div class="thumbnail-container">
                        <span class="project-title">{{ project.title }}</span>
                        <img class="project-thumbnail" :src="project.primaryImage.media.publicUrl" :alt="project.title">
                    </div>
                </router-link>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
#project-page {
    max-width: 90%;

    #project-page-title {
        display: flex;
        justify-content: space-between
    }

    .project-grid {
        --width: 500px;
        display: grid;
        grid-template-rows: repeat(auto-fill, var(--width));
        grid-template-columns: repeat(auto-fill, var(--width));
        row-gap: 8rem;
        column-gap: 3rem;
        justify-items: center;
        align-items: center;
        align-content: space-evenly;
        justify-content: space-between;

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
                    width: var(--width);
                    height: var(--width);
                    cursor: pointer;
                    transition: all .5s ease;
                    transform-origin: center;
                    backface-visibility: hidden;
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