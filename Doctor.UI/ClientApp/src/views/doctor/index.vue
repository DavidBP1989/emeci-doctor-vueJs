<template>
    <div class="main">
        <main-header></main-header>
        <b-container class="mt-4" :fluid="!loggedIn">
            <router-view></router-view>
        </b-container>
    </div>
</template>

<script>
import header from '../../shared/header/index.vue';

export default {
    beforeCreate() {
        const loggedIn = localStorage.getItem('appconfig') != null;
        document.body.className = loggedIn ? 'main_body' : 'register_body';
    },
    components: {
        'main-header': header
    },
    computed: {
        loggedIn() {
            return localStorage.getItem('appconfig') != null;
        }
    }
}
</script>