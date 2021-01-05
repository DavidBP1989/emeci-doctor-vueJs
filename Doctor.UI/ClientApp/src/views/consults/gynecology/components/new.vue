<template>
    <div class="gine">
        <b-row>
            <b-col cols="6" sm="6" md="3">
                <b-form-group label="Peso">
                    <b-input-group append="kg">
                        <b-form-input v-model="req.weight" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="6" md="3">
                <b-form-group label="Talla">
                    <b-input-group append="m">
                        <b-form-input v-model="req.size" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col cols="7" sm="6" md="4" lg="3">
                <b-form-group label="Índice de masa corporal">
                    <b-input-group append="kg/m2">
                        <b-form-input disabled :value="mass" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col cols="5" sm="6" md="2" lg="3">
                <b-form-group label="Temperatura">
                    <b-input-group append="c">
                        <b-form-input v-model="req.temperature" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col sm="6" md="4" lg="3">
                <b-form-group label="Edad de su menarca">
                    <b-form-input v-model="req.menarcaAge" @keypress="onlyDecimals" />
                </b-form-group>
            </b-col>
            <b-col sm="6" md="4" lg="3">
                <b-form-group label="Presión arterial sistólica">
                    <b-form-input v-model="req.bloodPressure_A" @keypress="onlyDecimals" />
                </b-form-group>
            </b-col>
            <b-col sm="6" md="4" lg="3">
                <b-form-group label="Presión arterial diastólica">
                    <b-form-input v-model="req.bloodPressure_B" @keypress="onlyDecimals" />
                </b-form-group>
            </b-col>
            <b-col sm="6" md="4" lg="3">
                <b-form-group
                description="Puede cambiar el mes y año en la parte superior del calendario"
                label="Fecha última menstruación">
                    <b-input-group>
                        <v-date-picker
                        class="form-control p-0"
                        v-model="req.lastMenstruationDate"
                        :input-props="{ disabled: visibleCollapse }"
                        color="pink"
                        :popover="{ placement: 'bottom', visibility: 'click' }">
                        </v-date-picker>
                        <template v-slot:prepend>
                            <b-input-group-text>
                                <fa-icon :icon="['fas', 'calendar-alt']" size="lg" />
                            </b-input-group-text>
                        </template>
                    </b-input-group>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col cols="4" sm="3" md="2">
                <b-form-group label="Gestas">
                    <b-form-input type="number" min="0" v-model="req.gestas" />
                </b-form-group>
            </b-col>
            <b-col cols="4" sm="3" md="2">
                <b-form-group label="Paragestas">
                    <b-form-input type="number" min="0" v-model="req.paragestas" />
                </b-form-group>
            </b-col>
            <b-col cols="4" sm="3" md="2">
                <b-form-group label="Cesáreas">
                    <b-form-input type="number" min="0" v-model="req.cesareans" />
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="3" md="2">
                <b-form-group label="Abortos">
                    <b-form-input type="number" min="0" v-model="req.abortions" />
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="6" md="4" lg="2">
                <b-form-group label="Recién nacidos">
                    <b-form-input type="number" min="0" v-model="req.newlyBorn" />
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="6" md="2">
                <b-form-group label="Mortinatos">
                    <b-form-input type="number" min="0" v-model="req.stillbirth" />
                </b-form-group>
            </b-col>
            <b-col order="6" sm="8" md="6">
                <b-form-group label="Edad de inicio de vida sexual activa">
                    <b-form-input v-model="req.ageOfOnsetOfActiveSexualLife" @keypress="onlyDecimals"/>
                </b-form-group>
            </b-col>
            <b-col order="5" cols="6" sm="4" md="4">
                <b-form-group label="Menacmia">
                    <b-form-input v-model="req.menacma" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group>
                    <b-form-checkbox-group
                    v-model="req.selected"
                    :options="options" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row v-if="req.selected.includes(9)">
            <b-col>
                <b-form-group label="Explique">
                    <b-form-textarea rows="3" v-model="req.specifyOthers" />
                </b-form-group>
            </b-col>
        </b-row>
        <partner :partnerReq="req.partner" />
        <b-row class="mt-3">
            <b-col>
                <b-form-group label="Motivo de la consulta">
                    <b-form-textarea rows="3" v-model="req.reasonForConsultation" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row class="mt-2">
            <b-col class="text-md-right">
                <b-button variant="success" @click="saveConsult">Guardar consulta</b-button>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import partner from './partner.vue'

import eventBus from '@/helper/event-bus'
import model from '../helper/model'
import { saved, onlyNumber, initLoader } from '@/helper/util'
import api from '@/api/gyneConsult-service'
let loader = null

export default {
    props: {
        patientInfo: {
            type: Object,
            required: true
        }
    },
    components: {
        partner
    },
    data() {
        return {
            patientId: this.$route.params.id,
            doctorId: this.$appConfig.doctor.doctorId,
            options: [
                { text: 'Oligomenorrea', value: 0 },
                { text: 'Proiomenorrea', value: 1 },
                { text: 'Hipermenorrea', value: 2 },
                { text: 'Dismenorrea', value: 3 },
                { text: 'Dispareunia', value: 4 },
                { text: 'Leucorrea', value: 5 },
                { text: 'Lactorrea', value: 6 },
                { text: 'Amenorrea', value: 7 },
                { text: 'Metrorragia', value: 8 },
                { text: 'Otros', value: 9 }
            ],
            req: {
                weight: null,
                size: null,
                temperature: null,
                menarcaAge: null,
                bloodPressure_A: null,
                bloodPressure_B: null,
                lastMenstruationDate: null,
                gestas: 0,
                paragestas: 0,
                cesareans: 0,
                abortions: 0,
                newlyBorn: 0,
                stillbirth: 0,
                ageOfOnsetOfActiveSexualLife: null,
                menacma: null,
                selected: [],
                specifyOthers: null,
                partner: {
                    hasAPartner: false,
                    name: null,
                    sex: 'M',
                    maritalStatus: 0,
                    groupRH: null,
                    birthDate: null,
                    age: null,
                    occupation: null,
                    phone: null
                },
                reasonForConsultation: null
            },
            error: false,
            reqTosend: {}
        }
    },
    computed: {
        mass() {
            if (!isNaN(this.req.weight) && !isNaN(this.req.size)) {
                const size = parseFloat(this.req.size);
                const weight = parseFloat(this.req.weight);
                if (size > 0 && weight > 0) {
                    return (weight / (size * size)).toFixed(2);
                }
            }
            return '';
        }
    },
    methods: {
        onlyDecimals(evt) {
            onlyNumber(evt, true)
        },
        saveConsult() {
            this.reqValidation();
            if (!this.error) {
                loader = initLoader();

                api.post(this.doctorId, this.reqTosend).then(resp => {
                    loader.hide();
                    saved(true, 'Consulta agregada', null);
                    this.clear();
                    eventBus.$emit('putInPreviewGyneConsult');
                })
                .catch(_error => {
                    loader.hide();
                });
            }
        },
        reqValidation() {
            const _model = new model(this.patientId, this.patientInfo, this.req)

            _model.validate();
            if (_model.errors.length > 0) this.error = true;
            else {
                this.reqTosend = _model.__$;
                this.error = false;
            }
        },
        clear() {
            this.req.weight = null;
            this.req.size = null;
            this.req.temperature = null;
            this.req.menarcaAge = null;
            this.req.bloodPressure_A = null;
            this.req.bloodPressure_B = null;
            this.req.lastMenstruationDate = null;
            this.req.gestas = 0;
            this.req.paragestas = 0;
            this.req.cesareans = 0;
            this.req.abortions = 0;
            this.req.newlyBorn = 0;
            this.req.stillbirth = 0;
            this.req.ageOfOnsetOfActiveSexualLife = null;
            this.req.menacma = null;
            this.req.selected = [];
            this.req.specifyOthers = null;
            this.req.partner.hasAPartner = false;
            this.req.partner.name = null;
            this.req.partner.sex = 'M';
            this.req.partner.maritalStatus = 0;
            this.req.partner.groupRH = null;
            this.req.partner.birthDate = null;
            this.req.partner.age = null;
            this.req.partner.occupation = null;
            this.req.partner.phone = null
            this.req.reasonForConsultation = null
        }
    }
}
</script>