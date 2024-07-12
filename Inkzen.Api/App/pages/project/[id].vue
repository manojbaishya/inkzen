<script setup>
import { replaceNewlines } from '../../utils/text.mjs';
import { extractFilename, ProjectImageWidth } from '../../utils/images.mjs';

import { MasonryGrid } from "@egjs/vue-grid/dist/grid.esm.js";
import VueEasyLightbox from 'vue-easy-lightbox';

// #region Project Details API
const config = useRuntimeConfig();
const route = useRoute();
const imageWidth = ProjectImageWidth; //px
const getProjectDetails = `${config.public.baseUrl}/${config.public.api.getProject}/${route.params.id}?imageSize=${imageWidth}`;

const { data: projectData } = await useFetch(getProjectDetails);

const projectDetails = ref({
    project: projectData.value.project,
    categories: projectData.value.project.tags,
    images: projectData.value.gallery.map(img => ({
        id: img.id,
        src: img.url,
        alt: img.altText
    }))
});
// #endregion

// #region Video Providers
const videoProviders = {
    YOUTUBE: 'youtube',
    VIMEO: 'vimeo'
};

const videoType = ref(videoProviders.YOUTUBE);
const vimeoId = ref("");

videoType.value = projectData.value.project.regions.Video.value.includes(videoProviders.VIMEO) ? videoProviders.VIMEO : videoProviders.YOUTUBE;

if (videoType.value === videoProviders.VIMEO)
    vimeoId.value = projectData.value.project.regions.Video.value.split('/').at(-1);

// #endregion

// #region Lightbox
const lightboxVisible = ref(false);
const lightboxImage = ref("");

function showLightbox(url) {
    lightboxImage.value = url;
    toggleLightboxVisibility();
}

function toggleLightboxVisibility() {
    lightboxVisible.value = !lightboxVisible.value;
}
// #endregion

// #region Masonry Grid
const masonrygrid = reactive({
    align: "center",
    defaultDirection: "end",
    gap: 50,
    column: 2,
    columnSize: 0,
    columnSizeRatio: 0
});

const viewport = useViewport();

if (viewport.isLessThan('lg')) {
    masonrygrid.column = 1;
} else if (viewport.isLessThan('2xl')) {
    masonrygrid.column = 2;
} else {
    masonrygrid.column = 3;
    masonrygrid.gap = 30;
}

watch(viewport.breakpoint, (newBreakpoint, oldBreakpoint) => {
    if (viewport.isLessThan('lg')) {
        masonrygrid.column = 1;
    } else if (viewport.isLessThan('2xl')) {
        masonrygrid.column = 2;
    } else {
        masonrygrid.column = 3;
        masonrygrid.gap = 30;
    }
});

// #endregion

</script>

<template>
    <div>
        <vue-easy-lightbox
            :visible="lightboxVisible"
            :imgs="lightboxImage"
            :dblclick-disabled="true"
            :move-disabled="true"
            @hide="toggleLightboxVisibility"
        >
            <template #toolbar="" />
        </vue-easy-lightbox>

        <div
            id="project-details-1"
            class="_width:75%! _margin-x:auto!"
        >
            <IContainer fluid>
                <IRow id="project-title">
                    <IColumn lg="10">
                        <h1 class="_margin-top:6! _font-weight:light! d3">
                            {{ projectDetails.project.title }}
                        </h1>
                    </IColumn>
                    <IColumn lg="2" />
                </IRow>
                <IRow id="project-description">
                    <IColumn lg="4" />
                    <IColumn lg="8">
                        <IRow end>
                            <p class="_text-align:right! _font-size:lg! _font-weight:light!">
                                {{ projectDetails.project.regions.Description.value }}
                            </p>
                        </IRow>
                    </IColumn>
                </IRow>
                <hr>
                <IRow id="project-properties">
                    <IColumn md="4">
                        <IRow
                            id="project-categories"
                            start-md
                        >
                            <p class="_text-align:left! _font-size:lg! _font-weight:black!">
                                Categories
                            </p>
                        </IRow>
                        <IRow
                            id="project-categories"
                            start-md
                        >
                            <IColumn>
                                <IRow
                                    v-for="category in projectDetails.categories"
                                    :key="category.id"
                                    class="project-category _font-size:lg! _font-weight:light!"
                                    start-md
                                >
                                    {{ category.title }}
                                </IRow>
                            </IColumn>
                        </IRow>
                    </IColumn>

                    <IColumn md="4">
                        <IRow
                            id="project-client"
                            center-md
                        >
                            <p class="_text-align:left! _font-size:lg! _font-weight:black!">
                                Client
                            </p>
                        </IRow>
                        <IRow
                            id="project-client-value"
                            center-md
                        >
                            <p class="_md:text:center! _font-size:lg! _font-weight:light!">
                                <span v-html="replaceNewlines(projectDetails.project.regions.Client.value)" />
                            </p>
                        </IRow>
                    </IColumn>

                    <IColumn md="4">
                        <IRow
                            id="project-year"
                            end-md
                        >
                            <p class="_text-align:left! _font-size:lg! _font-weight:black!">
                                Year
                            </p>
                        </IRow>
                        <IRow
                            id="project-year-value"
                            end-md
                        >
                            <p class="_text-align:left! _font-size:lg! _font-weight:light!">
                                {{ projectDetails.project.regions.Year.value }}
                            </p>
                        </IRow>
                    </IColumn>
                </IRow>
            </IContainer>
        </div>

        <IContainer fluid>
            <IRow
                id="project-images"
                center
            >
                <IColumn>
                    <masonry-grid
                        id="masonry-grid-container"
                        :gap="masonrygrid.gap"
                        :default-direction="masonrygrid.defaultDirection"
                        :align="masonrygrid.align"
                        :column="masonrygrid.column"
                        :column-size="masonrygrid.columnSize"
                        :column-size-ratio="masonrygrid.columnSizeRatio"
                    >
                        <NuxtImg
                            v-for="file in projectDetails.images"
                            :key="file.id"
                            class="masonry-images"
                            :src="`/${extractFilename(file.src)}`"
                            :alt="file.alt"
                            @click="() => showLightbox(`/${extractFilename(file.src)}`)"
                        />
                    </masonry-grid>
                </IColumn>
            </IRow>
        </IContainer>

        <div
            id="project-details-3"
            class="_width:75%! _margin-x:auto!"
        >
            <IContainer fluid>
                <IRow
                    id="project-video"
                    class="_margin-x:auto!"
                    center
                >
                    <iframe
                        v-if="videoType == videoProviders.YOUTUBE"
                        id="project-video"
                        :src="`https://www.youtube.com/embed/${projectDetails.project.regions.Video.value}`"
                        title="project-video"
                        allow="accelerometer; autoplay; fullscreen; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                        referrerpolicy="strict-origin-when-cross-origin"
                    />
                    <iframe
                        v-else-if="videoType == videoProviders.VIMEO"
                        id="project-video"
                        title="project-video"
                        :src="`https://player.vimeo.com/video/${vimeoId}?h=092e774b5c&loop=1`"
                        allow="autoplay; fullscreen; picture-in-picture"
                    />
                </IRow>
            </IContainer>
        </div>
    </div>
</template>

<style scoped lang="scss">
@import '@inkline/inkline/css/mixins';

#project-details-1 {
    #project-properties {
        align-content: space-between;

        height: fit-content;

        @include breakpoint-down('sm') {
            height: 600px;
        }
    }
}


#project-images {
    margin-top: calc(var(--margin-top) * 4);

    #masonry-grid-container {

        .masonry-images {
            transition: .2s all;

            max-width: 515px;
            width: 500px;

            @media screen and (max-width: 1600px) {
                max-width: 450px;
                width: 405px;
            }

            @include breakpoint-down('xl') {
                max-width: 550px;
                width: 500px;
            }

            @include breakpoint-down('lg') {
                max-width: 450px;
                width: 400px;
            }

            @include breakpoint-down('md') {
                max-width: 500px;
            }

            @include breakpoint-down('xs') {
                max-width: 350px;
            }

            @media screen and (max-width: 380px) {
                max-width: 250px;
            }

            &:hover {
                filter: brightness(50%);
            }
        }
    }

}

#project-details-3 {

    #project-video {
        width: 100%;
        max-width: 1086px;
        aspect-ratio: 16/9;
        margin-top: 4rem;
    }
}
</style>