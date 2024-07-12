<template>
    <div
        id="footer"
        class="_margin-x:auto! _margin-y:8!"
    >
        <IContainer fluid>
            <IRow>
                <IColumn
                    xs="9"
                    class="d1 _font-weight:light! footer-title"
                >
                    Inkzen Studios
                </IColumn>
                <IColumn
                    xs="3"
                    class="d1 _text-align:right! footer-title"
                >
                    ■
                </IColumn>
            </IRow>
            <IRow>
                <IColumn>
                    <img
                        id="footer-section-arrow"
                        src="~/assets/Footer-Section-Arrow.svg"
                        alt="Footer-Section-Arrow"
                    >
                </IColumn>
            </IRow>
            <IRow>
                <IColumn xs="8">
                    <IRow>
                        <IColumn class="h4 _font-weight:light! ">
                            <a href="https://www.instagram.com/inkzenstudios">
                                Instagram
                            </a>
                        </IColumn>
                    </IRow>
                    <IRow>
                        <IColumn class="h4 _font-weight:light! ">
                            <a href="https://www.behance.net/theinkmonk">
                                Behance
                            </a>
                        </IColumn>
                    </IRow>
                    <IRow>
                        <IColumn class="h4 _font-weight:light! ">
                            © 2024 by Inkzen Studios
                        </IColumn>
                    </IRow>
                </IColumn>
                <IColumn
                    class="vertical-axis-align-bottom footer-align_right"
                    xs="4"
                >
                    <IRow>
                        <IColumn class="h4 _font-weight:light! _text-align:right!">
                            Christian Basti
                        </IColumn>
                    </IRow>
                    <IRow>
                        <IColumn class="h4 _font-weight:light! _text-align:right!">
                            GHY 781005
                        </IColumn>
                    </IRow>
                </IColumn>
            </IRow>
        </IContainer>
    </div>
</template>

<style lang="scss">
@import '@inkline/inkline/css/mixins';

#footer {
    max-width: 85%;

    .footer-title {
        @include breakpoint-down('lg') {
            font-size: 6rem;
        }

        @include breakpoint-down('md') {
            font-size: 5rem;
        }

        @include breakpoint-down('sm') {
            font-size: 4rem;
        }

        @include breakpoint-down('xs') {
            font-size: 3.5rem;
        }

        @media screen and (max-width: 380px) {
            font-size: 2.5rem;
        }
    }

    #footer-section-arrow {
        filter: invert(100%);

        @include breakpoint-down('lg') {
            max-width: 180px;
        }

        @include breakpoint-down('md') {
            max-width: 150px;
        }

        @include breakpoint-down('sm') {
            max-width: 120px;
        }

        @include breakpoint-down('xs') {
            max-width: 80px;
        }

        @media screen and (max-width: 380px) {
            max-width: 60px;
        }
    }
}
</style>