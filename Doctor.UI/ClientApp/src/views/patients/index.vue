<template>
    <div v-show="loggedIn" class="main">
        <main-header></main-header>
        <b-container>
            <router-view></router-view>
        </b-container>
    </div>
</template>

<script>
import header from '../../shared/header/index.vue';

export default {
    beforeCreate() {
        document.body.className = 'main_body';
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