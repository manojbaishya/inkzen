<script setup>
import NavbarLogo from '~/assets/NavbarLogo.png';
</script>

<template>
    <INavbar
        id="inkzen-navbar"
        size="lg"
        collapse="md"
        fluid
    >
        <INavbarBrand to="/">
            <img
                class="NavbarLogo"
                :src="NavbarLogo"
                alt="Studio Inkzen"
            >
        </INavbarBrand>
        <INavbarCollapsible class="_justify-content:end">
            <INav>
                <INavItem
                    to="/projects"
                    exact-active-class="-link-active"
                >
                    <span class="nav-link">Projects</span>
                </INavItem>
                <INavItem
                    to="/services"
                    exact-active-class="-link-active"
                >
                    <span class="nav-link">Services</span>
                </INavItem>
                <INavItem
                    to="/about"
                    exact-active-class="-link-active"
                >
                    <span class="nav-link">About</span>
                </INavItem>
            </INav>
        </INavbarCollapsible>
    </INavbar>
</template>

<style lang="scss">
@import '@inkline/inkline/css/mixins';

:root {
    --navbar--dark--background: var(--color-black);
    --navbar--dark--collapsed--background: var(--color-black);
    --navbar--collapsed--item--background: var(--color-black);
    --navbar--border-width: 0px;
    --navbar--dark--item--hover--background: var(--color-black);

    @include breakpoint-up('md') {
        --navbar--lg--font-size: 2rem;
        --navbar--lg--padding-top: 4rem;
        font-size: 20px;
    }

    .nav-item:hover {
        text-decoration: underline !important;
        text-underline-offset: 1rem;
    }

    .-link-active {
        text-decoration: underline !important;
        text-underline-offset: 1rem;
    }

    #inkzen-navbar>.container {
        @include breakpoint-down('lg') {
            justify-content: space-around;
        }
    }
}

.NavbarLogo {
    max-width: 28rem;

    @include breakpoint-down('xl') {
        max-width: 25rem;
    }

    @include breakpoint-down('lg') {
        max-width: 19rem;
    }

    @include breakpoint-down('md') {
        max-width: 20rem;
    }

    @include breakpoint-down('sm') {
        max-width: 17rem;
    }

    @include breakpoint-down('xs') {
        max-width: 16rem;
    }

    @media screen and (max-width: 380px) {
        max-width: 10rem;
    }
}

.nav-link {
    @include breakpoint-down('lg') {
        font-size: 1.6rem;
    }
}

.hamburger-menu {
    .hamburger-menu-bars, .hamburger-menu-bars::before, .hamburger-menu-bars::after {
        @include breakpoint-down('sm') {
            max-width: 25px;
        }

        @include breakpoint-down('xs') {
            max-width: 20px;
        }

        @media screen and (max-width: 380px) {
            max-width: 18px;
        }
    }

}
</style>