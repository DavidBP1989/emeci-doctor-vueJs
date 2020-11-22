<template>
    <header class="mb-2">
        <header-top v-if="loggedIn" />
        <header-menu />
        <b-container class="pt-3">
            <b-row class="align-items-center">
                <b-col md="6" order="2" order-md="1">
                    <h3 class="title">{{ title }}</h3>
                </b-col>
                <b-col v-if="crumbs.length > 0" order="1">
                    <b-breadcrumb :items="crumbs" class="justify-content-end mb-2" />
                </b-col>
            </b-row>
        </b-container>
    </header>
</template>

<script>
import headerTop from './header-top.vue';
import headerMenu from './header-menu.vue';

export default {
    components: {
        headerTop,
        headerMenu
    },
    computed: {
        title() {
            return this.$route.name;
        },
        crumbs() {
            return this.$route.meta.breadcrumb;
        },
        loggedIn() {
            return localStorage.getItem('appconfig') != null;
        }
    }
}
</script>