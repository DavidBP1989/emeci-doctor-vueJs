<template>
    <div>
        <p class="brand-wrapper mb-3">Iniciar sesi&oacute;n en su cuenta</p>
        <p class="error" v-if="error">N&uacute;mero de tarjeta o contrase&ntilde;a incorrecta</p>

        <b-form @submit="onSubmit" autocomplete="off">
            <b-form-group class="mb-3">
                <b-form-input maxlength="10" v-model="emeci" @keypress="format" placeholder="Número de tarjeta" />
            </b-form-group>
            <b-form-group class="mb-4">
                <b-form-input v-model="password" type="password" placeholder="***********" />
            </b-form-group>
            <b-button type="submit" class="w-100 mb-4 main-button">Iniciar sesi&oacute;n</b-button>
        </b-form>
        <router-link to="/auth/forgotPwd">¿Se te olvido tu contrase&ntilde;a?</router-link>
        <p class="footer">
            <span class="n-link">¿No tienes una cuenta?.</span>
            <router-link to="/register">Registrarse como m&eacute;dico</router-link>
        </p>
        <b-nav class="mt-5">
            <b-nav-item target="_blank" href="https://www.emeci.com/ConsultaMedico/uso.pdf">T&eacute;rminos de uso</b-nav-item>
            <b-nav-item target="_blank" href="https://www.emeci.com/wsite/home/NoticeOfPrivacy">Aviso de privacidad</b-nav-item>
        </b-nav>
    </div>
</template>

<script>
import api from '../../../api/auth-service';
import apiDoctor from '../../../api/doctor-service';
import { userFormat } from '../../../helper/util';

let loader = null;

export default {
    data() {
        return {
            emeci: '',
            password: '',
            error: false
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();

            if (this.validFeedback()) {
                loader = this.loader();

                api.getToken({ username: this.emeci, password: this.password }).then(response => {
                    this.error = false;
                    this.getDoctor();
                })
                .catch(_error => {
                    loader.hide();
                    this.error = true
                })
            }
        },
        getDoctor() {
            apiDoctor.get(this.$appConfig.doctor.doctorId).then(response => {
                this.$appConfig.doctor.emeci = response.body.EMECI;
                this.$appConfig.doctor.name = response.body.Name;
                this.$saveConfig();
                loader.hide();
                this.$router.push('/');
            }).catch(_error => {
                loader.hide();
                this.error = true;
            })
        },
        validFeedback() {
            return (this.emeci !== '' && this.password !== '');
        },
        loader() {
            return this.$loading.show({
                loader: 'dots',
                height: 155,
                width: 135,
                color: '#D22874',
                backgroundColor: '#E8E8E8'
            });
        },
        format(evt) {
            userFormat(evt, this.emeci.includes('-'));
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