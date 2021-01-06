<template>
    <div>
        <b-form @submit="onSubmit" autocomplete="off">
            <p class="brand-wrapper mb-3">Recordar contrase&ntilde;a</p>
            <b-form-group class="mb-4">
                <b-form-input v-model="emeci" maxlength="10" @keypress="format" placeholder="Número de tarjeta" />
            </b-form-group>
            <b-button type="submit" class="w-100 mb-4 main-button">Recordar</b-button>
        </b-form>
        <b-alert :show="showError" variant="danger">
            N&uacute;mero de tarjeta incorrecto
        </b-alert>
        <b-alert :show="showSuccess" variant="success">
            Se ha enviado la contrase&ntilde;a a su correo electr&oacute;nico
        </b-alert>
        <fa-icon :icon="['fas', 'chevron-left']" size="sm" class="align-middle"/>
        <b-button variant="link" class="pl-1" @click="$router.go(-1)">Volver a inicio de sesi&oacute;n</b-button>
    </div>
</template>

<script>
import { userFormat } from '../../../helper/util';
import api from '../../../api/doctor-service';

export default {
    data() {
        return {
            emeci: '',
            showSuccess: false,
            showError: false
        }
    },
    methods: {
        format(evt) {
            userFormat(evt, this.emeci.includes('-'));
        },
        onSubmit(evt) {
            evt.preventDefault();

            if (this.emeci !== '') {
                api.forgotPwd(this.emeci)
                .then(response => {
                    console.log(response);
                    this.showError = false;
                    this.showSuccess = true;
                    this.emeci = '';
                }).catch(_error => {
                    this.showSuccess = false;
                    this.showError = true;
                });
            }
        }
    },
    watch: {
        emeci(val) {
            if (/^[0-9]{5}$/.test(val)) {
                this.emeci += '-';
            }
        }
    }
}
</script>