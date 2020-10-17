<template>
    <div class="header-top">
        <b-container class="p-0">
            <b-row>
                <b-col cols="11" md="8" lg="6">
                    <b-list-group class="justify-content-center justify-content-md-start" horizontal="md">
                        <b-list-group-item class="mb-0">
                            <fa-icon :icon="['fas', 'user-md']" size="lg"/>
                            <span class="ml-1">{{ doctorName }}</span>
                        </b-list-group-item>
                        <b-list-group-item class="mb-0 ml-md-4">
                            <span class="font-weight-light">{{ emeci }}</span>
                        </b-list-group-item>
                    </b-list-group>
                </b-col>
                <b-col cols="1" md="4" lg="6">
                    <b-list-group class="justify-content-end point" horizontal>
                        <b-list-group-item class="mb-0 d-none d-md-block">
                            <b-link target="_blank" href="https://www.facebook.com/emeci.mexico">
                                <fa-icon :icon="['fab', 'facebook-f']" size="lg" />
                            </b-link>
                        </b-list-group-item>
                        <b-list-group-item class="mb-0 d-none d-md-block">
                            <b-link target="_blank" href="#">
                                <fa-icon :icon="['fab', 'twitter']" size="lg" />
                            </b-link>
                        </b-list-group-item>
                        <b-list-group-item class="mb-0">
                            <b-dropdown right no-caret toggle-class="text-decoration-none">
                                <b-dropdown-header>Configuraci&oacute;n</b-dropdown-header>
                                <template v-slot:button-content>
                                    <fa-icon :icon="['fas', 'caret-down']" size="lg" />
                                </template>
                                <b-dropdown-item-button @click="toRegister">
                                    <fa-icon :icon="['fas', 'user-md']" size="lg" />
                                    <span class="ml-2">Mis datos m&eacute;dicos</span>
                                </b-dropdown-item-button>
                                <b-dropdown-item-button @click="changePwd">
                                    <fa-icon :icon="['fas', 'unlock-alt']" size="lg" />
                                    <span class="ml-2">Cambiar contrase&ntilde;a</span>
                                </b-dropdown-item-button>
                                <b-dropdown-item-button @click="logout">
                                    <fa-icon :icon="['fas', 'sign-out-alt']" size="lg" />
                                    <span class="ml-2">Cerrar sesi&oacute;n</span>
                                </b-dropdown-item-button>
                            </b-dropdown>
                        </b-list-group-item>
                    </b-list-group>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script>
import Vue from 'vue';
import changePassword from '../changePassword.vue';

export default {
    data() {
        return {
            emeci: this.$appConfig.doctor.emeci,
            doctorName: this.$appConfig.doctor.name
        }
    },
    methods: {
        logout() {
            localStorage.removeItem('appconfig');
            this.$router.push('/auth');
        },
        changePwd() {
            const componentToLoad = Vue.extend(changePassword);
            const instance = new componentToLoad();
            instance.$mount();
            
            this.$swal({
                title: 'Cambiar contraseña',
                html: '<div></div>',
                showCloseButton: true,
                showConfirmButton: false,
                reverseButtons: true,
                onBeforeOpen: () => {
                    this.$swal.getContent()
                    .querySelector('div')
                    .append(instance.$el);
                }
            })
        },
        toRegister() {
            this.$router.push('/register');
        }
    }
}
</script>