<script setup>

import { extractFilename } from '../utils/images.mjs';

const config = useRuntimeConfig();

const getAllTeam = `${config.public.baseUrl}/${config.public.api.getAllTeam}`;

const { data: team } = await useFetch(getAllTeam);

</script>

<template>
    <div>
        <div
            id="aboutpage-intro"
            class="_margin-x:auto! about-page-width"
        >
            <IContainer fluid>
                <IRow class="_margin-y:2!">
                    <IColumn
                        id="aboutpage-title"
                        lg="10"
                        class="_font-weight:light!"
                    >
                        We are Inkzen Studios
                    </IColumn>
                    <IColumn lg="2" />
                </IRow>
                <IRow>
                    <IColumn lg="4" />
                    <IColumn lg="8">
                        <IRow class="_margin-y:2!">
                            <IColumn class="_text-align:right! _font-weight:light! aboutpage-subtitle">
                                A Multi Disciplinary Design Company.
                            </IColumn>
                        </IRow>
                        <IRow class="_text-align:right! _margin-y:2!">
                            <IColumn>
                                <p>
                                    Among the areas we work in are: graphics and identity, products and packaging,
                                    exhibitions
                                    and
                                    installations, websites and digital experiences, advertising and communications, and
                                    sound
                                    and
                                    motion.
                                </p>
                            </IColumn>
                        </IRow>
                        <IRow class="_margin-y:2!">
                            <IColumn
                                md="6"
                                class="_text-align:right! _padding-right:0!"
                            >
                                <p>
                                    I'm a paragraph. Click here to add your own text and
                                    edit me. It's easy. Just
                                    click “Edit Text” or double click me to add your own content and make changes to the
                                    font.
                                    Feel free to drag and drop me anywhere you like on your page. I'm a great place for
                                    you
                                    to
                                    tell a story and let your users know a little more about you.
                                </p>
                            </IColumn>
                            <IColumn
                                md="6"
                                class="_text-align:right! _padding-right:0!"
                            >
                                <p>
                                    This is a great space to write a long text about your
                                    company and your services.
                                    You can use this space to go into a little more detail about your company. Talk
                                    about
                                    your
                                    team and what services you provide. Tell your visitors the story of how you came up
                                    with
                                    the
                                    idea for your business and what makes you different from your competitors. Make your
                                    company
                                    stand out and show your visitors who you are.
                                </p>
                            </IColumn>
                        </IRow>
                    </IColumn>
                </IRow>
            </IContainer>
        </div>

        <video
            id="aboutpage-video"
            class="_margin-y:2!"
            autoplay
            loop
            muted
        >
            <source
                src="/About/loop.mp4"
                type="video/mp4"
            >
        </video>

        <div
            id="aboutpage-founders"
            class="_margin-x:auto! about-page-width"
        >
            <IContainer fluid>
                <hr>
                <IRow>
                    <IColumn lg="4" />
                    <IColumn
                        lg="8"
                        class="_text-align:right! _font-weight:light! aboutpage-subtitle"
                    >
                        The People for you.
                    </IColumn>
                </IRow>

                <!-- /* --------- */ -->
                <!-- /* FOUNDER 1 */ -->
                <!-- /* --------- */ -->
                <IRow
                    end
                    class="_margin-y:2!"
                >
                    <IColumn md="6">
                        <IRow>
                            <IColumn class="_text-align:left! h5">
                                Erland Shaw
                            </IColumn>
                        </IRow>
                        <IRow>
                            <IColumn class="_text-align:left! h6">
                                Partner
                            </IColumn>
                        </IRow>
                        <IRow>
                            <IColumn
                                md="8"
                                class="_text-align:left! _padding-bottom:1!"
                            >
                                <p>
                                    I'm a paragraph. Click here to add your own text and edit me. It's easy. Just click
                                    “Edit
                                    Text”
                                    or double click me to add your own content and make changes to the font.
                                </p>
                            </IColumn>
                            <IColumn md="2" />
                        </IRow>
                    </IColumn>
                    <IColumn
                        md="6"
                        class="image-founder-container"
                    >
                        <img
                            class="image-founder"
                            src="/About/founder-1.webp"
                            alt="founder-1.webp"
                        >
                    </IColumn>
                </IRow>

                <!-- /* --------- */ -->
                <!-- /* FOUNDER 2 */ -->
                <!-- /* --------- */ -->
                <IRow
                    reverse-md
                    class="_margin-y:2!"
                >
                    <IColumn md="6">
                        <IRow end>
                            <IColumn class="_text-align:right! h5">
                                Deja Fos
                            </IColumn>
                        </IRow>
                        <IRow end>
                            <IColumn class="_text-align:right! h6">
                                Partner
                            </IColumn>
                        </IRow>
                        <IRow end>
                            <IColumn md="2" />
                            <IColumn
                                md="8"
                                class="_text-align:right! _padding-bottom:1!"
                            >
                                <p>
                                    I'm a paragraph. Click here to add your own text and edit me. It's easy. Just click
                                    “Edit
                                    Text”
                                    or double click me to add your own content and make changes to the font.
                                </p>
                            </IColumn>
                        </IRow>
                    </IColumn>
                    <IColumn
                        md="6"
                        class="image-founder-container"
                    >
                        <img
                            class="image-founder"
                            src="/About/founder-2.webp"
                            alt="founder-2.webp"
                        >
                    </IColumn>
                </IRow>
                <hr>
            </IContainer>
        </div>

        <div
            id="aboutpage-team"
            class="_margin-x:auto! about-page-team-section-width"
        >
            <IContainer fluid>
                <IRow>
                    <IColumn lg="4" />
                    <IColumn
                        lg="8"
                        class="_text-align:right! _font-weight:light! aboutpage-subtitle"
                    >
                        Our Superstar Team.
                    </IColumn>
                </IRow>


                <IRow center>
                    <IColumn
                        v-for="member in team"
                        :key="member.id"
                        md="6"
                        xxl="4"
                        class="_margin-y:2!"
                    >
                        <ICard
                            color="dark"
                            size="lg"
                        >
                            <template #image>
                                <NuxtImg
                                    :src="`/About/${extractFilename(member.photo.media.publicUrl)}`"
                                    :alt="member.photo.media.altText"
                                />
                            </template>
                            <h2>{{ member.name.value }}</h2>
                            <p>
                                {{ member.bio.value }}
                            </p>
                            <a :href="member.link.value">#SOCIAL</a>
                        </ICard>
                    </IColumn>
                </IRow>
            </IContainer>
        </div>
    </div>
</template>

<style lang="scss" scoped>
@import '@inkline/inkline/css/mixins';

.about-page-width {
    width: 75%;

    @include breakpoint-down('lg') {
        width: 95%;
    }
}

.aboutpage-subtitle {
    font-size: calc(var(--font-size) * var(--scale-ratio-pow-5) * var(--scale-ratio-pow-1));

    @include breakpoint-down('sm') {
        font-size: calc(var(--font-size) * var(--scale-ratio-pow-5));
    }

    @include breakpoint-down('xs') {
        font-size: calc(var(--font-size) * var(--scale-ratio-pow-4));
    }

    @media screen and (max-width: 380px) {
        font-size: calc(var(--font-size) * var(--scale-ratio-pow-3));
    }
}

#aboutpage-intro {
    #aboutpage-title {
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

#aboutpage-video {
    min-width: 75%;
    max-width: 100%;
    object-fit: cover;
}

#aboutpage-founders {
    .image-founder-container {
        @include breakpoint-down('sm') {
            display: flex;
            justify-content: center;
            align-items: center;
        }
    }

    .image-founder {
        max-width: 100%;
        width: 100%;

        @include breakpoint-down('sm') {
            max-width: 85%;
            width: 85%;
        }
    }
}

#aboutpage-team {
    &.about-page-team-section-width {
        width: 75%;

        @include breakpoint-down('lg') {
            width: 75%;
        }
    }
}
</style>