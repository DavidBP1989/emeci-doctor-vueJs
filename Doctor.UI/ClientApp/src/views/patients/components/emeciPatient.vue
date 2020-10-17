<template>
    <div>
        <b-row class="align-items-center">
            <b-col md="4">
                <b-form-group label="Número de tarjeta EMECI">
                    <b-form-input placeholder="00000-0000-00000" v-model="emeci" @keypress="format" />
                </b-form-group>
            </b-col>
            <b-col md="3">
                <b-form-group class="mt-2" label="Clave de la tarjeta">
                    <div class="d-flex">
                        <label class="mr-4 fz25">{{ coordinate }}</label>
                        <b-input class="ml-auto" v-model="value" @keypress="numberFormat" maxlength="3" />
                    </div>
                </b-form-group>
            </b-col>
            <b-col class="text-right">
                <b-button @click="addPatient" variant="success">Agregar paciente</b-button>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-alert :show="error" variant="danger">
                    Paciente no encontrado.
                </b-alert>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import api from '../../../api/patient-service';
import { initLoader, userFormat, emeciNumber, onlyNumber } from '../../../helper/util';

let loader = null;

export default {
    created() {
        this.key();
    },
    data() {
        return {
            emeci: '',
            coordinate: '',
            value: '',
            doctorId: this.$appConfig.doctor.doctorId,
            error: false
        }
    },
    methods: {
        format(evt) {
            userFormat(evt);
        },
        numberFormat(evt) {
            onlyNumber(evt);
        },
        addPatient() {
            if (this.emeci !== '' && this.value !== '') {
                this.error = false;
                loader = initLoader();

                api.newExistingPatient(this.doctorId, this.emeci, this.coordinate, this.value)
                .then(response => {
                    loader.hide();
                    const patientId = response.body.PatientId;
                    if (patientId != null) {
                        this.$router.push(`/consults/${patientId}`);
                    } else this.error = true;

                }).catch(() => {
                    loader.hide();
                    this.error = true;
                });
            }
        },
        key() {
            const letters = 'ABCDEFGHIJ'.split('');
            const letter = letters[Math.floor(Math.random() * letters.length)];
            const number = Math.floor(Math.random() * 10) + 1;
            this.coordinate = `${letter}${number}`;
        }
    },
    watch: {
        emeci(val) {
            this.emeci = emeciNumber(val);
        }
    }
}
</script>