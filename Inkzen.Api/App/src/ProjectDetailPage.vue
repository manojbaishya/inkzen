<script>
import { useFetch } from '@vueuse/core';
import { useContextStore } from './context';
import { replaceNewlines } from './utils';

export default {
    beforeRouteEnter: async function (route) {
        const ctx = useContextStore();

        const imageWidth = 800; //px
        const getProjectDetails = `${ctx.appconfig.baseUrl}/${ctx.appconfig.imagewidth.getProject}/${route.params.id}?imageSize=${imageWidth}`;

        const { response } = await useFetch(getProjectDetails).get();

        const projectData = await response.value.json();
        route.meta.data = {
            project: projectData.project,
            categories: projectData.project.tags,
            images: projectData.gallery.map(img => ({
                id: img.id,
                src: img.url.replace('~', ctx.appconfig.service),
                alt: img.altText
            }))
        }
    }
}
</script>

<script setup>
import { ref, reactive } from 'vue';

import { IContainer } from '@inkline/inkline/components/IContainer';
import { IColumn } from '@inkline/inkline/components/IColumn';
import { IRow } from '@inkline/inkline/components/IRow';

import { MasonryGrid } from "@egjs/vue-grid";

import VueEasyLightbox from 'vue-easy-lightbox';

const props = defineProps({
    project: {
        type: Object,
        required: true
    },
    categories: {
        type: Array,
        required: true
    },
    images: {
        type: Array,
        required: true
    }
});

const videoProviders = {
    YOUTUBE: 'youtube',
    VIMEO: 'vimeo'
};

const videoType = ref(videoProviders.YOUTUBE);

videoType.value = props.project.regions.Video.value.includes(videoProviders.VIMEO) ? videoProviders.VIMEO : videoProviders.YOUTUBE;

const vimeoId = ref("");
if (videoType.value === videoProviders.VIMEO)
    vimeoId.value = props.project.regions.Video.value.split('/').at(-1);

const masonrygrid = reactive({
    gap: 50,
    align: "justify",
    defaultDirection: "end",
    column: 2,
    columnSize: 0,
    columnSizeRatio: 0
});

const lightboxVisible = ref(false);
const lightboxImage = ref("");

function showLightbox(url) {
    lightboxImage.value = url;
    toggleLightboxVisibility();
}

function toggleLightboxVisibility() {
    lightboxVisible.value = !lightboxVisible.value;
}

</script>

<template>

    <vue-easy-lightbox :visible="lightboxVisible" :imgs="lightboxImage" :dblclick-disabled="true" :move-disabled="true"
        @hide="toggleLightboxVisibility">
        <template v-slot:toolbar=""></template>
    </vue-easy-lightbox>

    <div class="_width:75%! _margin-x:auto!">
        <IContainer fluid>
            <IRow id="project-title">
                <IColumn md="10">
                    <h1 class="_margin-top:6! font-weight_light d3">{{ project.title }}</h1>
                </IColumn>
                <IColumn md="2">
                </IColumn>
            </IRow>
            <IRow id="project-description">
                <IColumn md="6">
                </IColumn>
                <IColumn md="6">
                    <IRow end>
                        <p class="_text-align:right! _font-size:lg! _font-weight:light!">
                            {{ project.regions.Description.value }}
                        </p>
                    </IRow>
                </IColumn>
            </IRow>
            <hr>

            <IRow id="project-properties">
                <IColumn md="4">
                    <IRow id="project-year" start>
                        <p class="_text-align:left! _font-size:lg! _font-weight:light!">
                            Year
                        </p>
                    </IRow>
                    <IRow id="project-year-value" start>
                        <p class="_text-align:left! _font-size:lg! _font-weight:light!">
                            {{ project.regions.Year.value }}
                        </p>
                    </IRow>
                </IColumn>
                <IColumn md="4">
                    <IRow id="project-client" start>
                        <p class="_text-align:left! _font-size:lg! _font-weight:light!">
                            Client
                        </p>
                    </IRow>
                    <IRow id="project-client-value" start>
                        <p class="_text-align:left! _font-size:lg! _font-weight:light!">
                            <span v-html="replaceNewlines(project.regions.Client.value)"></span>
                        </p>
                    </IRow>
                </IColumn>
                <IColumn md="4">
                    <IRow id="project-categories" end>
                        <p class="_text-align:left! _font-size:lg! _font-weight:light!">
                            Categories
                        </p>
                    </IRow>
                    <IRow id="project-categories" end>
                        <IColumn>
                            <IRow class="project-category _font-size:lg! _font-weight:light!" end
                                v-for="category in categories" :key="category.id">
                                {{ category.title }}
                            </IRow>
                        </IColumn>
                    </IRow>
                </IColumn>
            </IRow>

            <IRow id="project-images" center>
                <masonry-grid class="container _margin-top:8!" :gap="masonrygrid.gap"
                    :defaultDirection="masonrygrid.defaultDirection" :align="masonrygrid.align"
                    :column="masonrygrid.column" :columnSize="masonrygrid.columnSize"
                    :columnSizeRatio="masonrygrid.columnSizeRatio">
                    <img class="gallery-images" v-for="file in images" :key="file.id" :src="file.src" :alt="file.alt"
                        @click="() => showLightbox(file.src)">
                </masonry-grid>
            </IRow>

            <IRow id="project-video" class="_margin-x:auto!" center>
                <iframe v-if="videoType == videoProviders.YOUTUBE"
                    :src="`https://www.youtube.com/embed/${project.regions.Video.value}`" id="project-video"
                    title="project-video"
                    allow="accelerometer; autoplay; fullscreen; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                    referrerpolicy="strict-origin-when-cross-origin">
                </iframe>
                <iframe v-else-if="videoType == videoProviders.VIMEO" id="project-video" title="project-video"
                    :src="`https://player.vimeo.com/video/${vimeoId}?h=092e774b5c&loop=1`"
                    allow="autoplay; fullscreen; picture-in-picture"></iframe>
            </IRow>

        </IContainer>
    </div>
</template>

<style scoped lang="scss">
#project-video {
    width: 100%;
    max-width: 1086px;
    aspect-ratio: 16/9;
    margin-top: 4rem;
}

.gallery-images {
    transition: .2s all;

    &:hover {
        filter: brightness(50%);
    }
}
</style>