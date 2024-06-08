<script setup>
import HeaderBar from './HeaderBar.vue';
import FooterBar from './FooterBar.vue';

import { ILayoutHeader } from '@inkline/inkline/components/ILayoutHeader';
import { ILayoutContent } from '@inkline/inkline/components/ILayoutContent';
import { ILayoutFooter } from '@inkline/inkline/components/ILayoutFooter';
</script>

<template>
    <ILayout>
        <ILayoutHeader>
            <HeaderBar></HeaderBar>
        </ILayoutHeader>
        <ILayoutContent>
            <RouterView></RouterView>
        </ILayoutContent>
        <ILayoutFooter>
            <FooterBar></FooterBar>
        </ILayoutFooter>
    </ILayout>
</template>

<style>
@import url("@/css/defaults.css")
</style>