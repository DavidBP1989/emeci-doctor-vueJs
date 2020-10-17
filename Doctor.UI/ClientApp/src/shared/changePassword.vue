<template>
    <b-container class="text-left">
        <b-form @submit="onSubmit" autocomplete="off">
            <b-row>
                <b-col md="6">
                    <b-form-group label="Contraseña actual">
                        <b-form-input v-model="currentPwd" type="password" placeholder="Contraseña" />
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col md="6">
                    <b-form-group label="Nueva contraseña">
                        <b-form-input v-model="newPwd" type="password" placeholder="Contraseña" />
                    </b-form-group>
                </b-col>
                <b-col>
                    <b-form-group label="Confirmar contraseña">
                        <b-form-input v-model="confirmNewPwd" type="password" placeholder="Contraseña" />
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row v-if="error !== ''">
                <b-col>
                    <b-alert :show="true" variant="danger">
                        {{ error }}
                    </b-alert>
                </b-col>
            </b-row>
            <b-row>
                <b-col class="text-right">
                    <b-button variant="success" type="submit">Cambiar contraseña</b-button>
                </b-col>
            </b-row>
        </b-form>
    </b-container>
</template>

<script>
import api from '../api/doctor-service';
import { saved, initLoader } from '../helper/util';
import Vue from 'vue';
let loader = null;

export default {
    data() {
        return {
            doctorId: this.$appConfig.doctor.doctorId,
            currentPwd: '',
            newPwd: '',
            confirmNewPwd: '',
            error: ''
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();

            this.valid();
            if (this.error === '') {
                loader = initLoader();
                const req = {
                    CurrentPassword: this.currentPwd,
                    NewPassword: this.newPwd
                };

                api.changePwd(this.doctorId, req).then(response => {
                    if (response.body.Error === '') {
                        loader.hide();
                        Vue.swal.close();
                        localStorage.removeItem('appconfig');
                        saved('Contraseña actualizada', '/auth');
                    } else {
                        this.error = response.body.Error;
                        loader.hide();
                    }
                }).catch(_error => {
                    this.error = _error;
                    loader.hide();
                });
            }
        },
        valid() {
            this.error = '';
            if (!this.currentPwd || !this.newPwd || !this.confirmNewPwd) {
                this.error = 'Todos los campos son necesarios';
                return;
            }

            if (this.newPwd !== this.confirmNewPwd) {
                this.error = 'La nueva contraseña no coincide';
                return;
            }
        }
    }
}
</script>