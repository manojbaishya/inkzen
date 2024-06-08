<template>
    <INavbar size="lg" fluid>
        <INavbarBrand to="/">
            <img class="NavbarLogo" :src="NavbarLogo" alt="Studio Inkzen">
        </INavbarBrand>
        <INavbarCollapsible class="_justify-content:center">
            <INav>
                <INavItem to="/projects" exact-active-class="-link-active"> Projects </INavItem>
                <INavItem to="/services" exact-active-class="-link-active"> Services </INavItem>
                <INavItem to="/about" exact-active-class="-link-active"> About </INavItem>
            </INav>
        </INavbarCollapsible>
    </INavbar>
</template>

<script setup>
import NavbarLogo from '/assets/NavbarLogo.png';
</script>

<style lang="scss">
@import '@inkline/inkline/css/mixins';

:root {
    --navbar--dark--background: var(--color-black);
    --navbar--dark--collapsed--background: var(--color-black);
    --navbar--collapsed--item--background: var(--color-black);
    --navbar--border-width: 0px;
    --navbar--dark--item--hover--background: var(--color-black);

    .nav-item:hover {
        text-decoration: underline !important;
        text-underline-offset: 1rem;
    }

    .-link-active {
        text-decoration: underline !important;
        text-underline-offset: 1rem;
    }

    @include breakpoint-down('sm') {
        .NavbarLogo {
            max-width: 12rem;
        }
    }

    @include breakpoint-up('md') {
        --navbar--lg--font-size: 2rem;
        --navbar--lg--padding-top: 4rem;
        font-size: 20px;

        .NavbarLogo {
            max-width: 28rem;
        }
    }

}
</style>