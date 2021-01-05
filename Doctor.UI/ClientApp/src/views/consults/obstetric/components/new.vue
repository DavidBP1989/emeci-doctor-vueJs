<template>
    <div class="obste">
        <b-row class="align-items-lg-center">
            <b-col cols="6" sm="6" md="3" lg="2">
                <b-form-group label="Peso">
                    <b-input-group append="kg">
                        <b-form-input v-model="req.weight" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="6" md="3" lg="2">
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
            <b-col class="showExtraCol" lg="2">
                <b-form-group label="Activa sexualmente">
                    <div class="d-flex">
                        <span>No</span>
                        <b-form-checkbox class="ml-2" v-model="req.sexuallyActive" switch>Si</b-form-checkbox>
                    </div>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row class="align-items-center">
            <b-col cols="7" sm="5" md="4" lg="3">
                <b-form-group label="Número de embarazos">
                    <b-form-input v-model="req.pregnancyNumber" type="number" />
                </b-form-group>
            </b-col>
            <b-col class="hideExtracol" order="3" order-sm="0" sm="4">
                <b-form-group label="Activa sexualmente">
                    <div class="d-flex">
                        <span>No</span>
                        <b-form-checkbox class="ml-2" v-model="req.sexuallyActive" switch>Si</b-form-checkbox>
                    </div>
                </b-form-group>
            </b-col>
            <b-col cols="6" lg="3" order="1" order-sm="1">
                <b-form-group label="Pres. arterial sistólica">
                    <b-form-input @keypress="onlyDecimals" v-model="req.bloodPressure_A" />
                </b-form-group>
            </b-col>
            <b-col cols="6" lg="3" order="2" order-sm="2">
                <b-form-group label="Pres. arterial diastólica">
                    <b-form-input @keypress="onlyDecimals" v-model="req.bloodPressure_B" />
                </b-form-group>
            </b-col>
            <b-col cols="5" sm="3" md="4" lg="3">
                <b-form-group label="Abortos">
                    <b-form-input type="number" v-model="req.abortions" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col cols="6" md="4" lg="3">
                <b-form-group
                label="Fecha último parto"
                description="Puede cambiar el mes y año en la parte superior del calendario">
                    <b-input-group>
                        <v-date-picker
                        class="form-control p-0"
                        v-model="req.lastParturitionDate"
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
            <b-col order="1" order-sm="0" sm="6" md="5" lg="3">
                <b-form-group
                label="Primer día de ult. menstruación"
                description="Puede cambiar el mes y año en la parte superior del calendario">
                    <b-input-group>
                        <v-date-picker
                        class="form-control p-0"
                        v-model="req.firstDayMenstruation"
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
            <b-col cols="6" order="2" order-sm="0" sm="6" md="3">
                <b-form-group label="Toxemias previas">
                    <div class="d-flex">
                        <span>No</span>
                        <b-form-checkbox class="ml-2" v-model="req.toxemias" switch>Si</b-form-checkbox>
                    </div>
                </b-form-group>
            </b-col>
            <b-col cols="6" lg="3">
                <b-form-group label="Partos">
                    <b-form-select v-model="req.parturition" :options="parturitionOptions" />
                </b-form-group>
            </b-col>
            <distocias :show="req.parturition == 1" :dystocia="req.dystocia" class="mb-2" />
        </b-row>
        <b-row>
            <b-col cols="6" md="3">
                <b-form-group label="Cesáreas previas">
                    <b-form-input type="number" v-model="req.cesarean" />
                </b-form-group>
            </b-col>
            <b-col cols="6" md="3">
                <b-form-group label="Uso de forceps">
                    <b-form-input type="number" v-model="req.forceps" />
                </b-form-group>
            </b-col>
            <b-col cols="6" md="3" lg="3">
                <b-form-group label="Mortinatos">
                    <b-form-input type="number" v-model="req.stillbirths" />
                </b-form-group>
            </b-col>
            <b-col cols="6" md="3" lg="3">
                <b-form-group label="R.N. vivos">
                    <b-form-input type="number" v-model="req.newBornAlive" />
                </b-form-group>
            </b-col>
        </b-row>
        <pregnancies :pregnancies="req._pregnancies" />
        <b-row>
            <b-col>
                <b-form-group label="Observaciones">
                    <b-form-textarea rows="3" v-model="req.observations" />
                </b-form-group>
            </b-col>
        </b-row>
        <pregnancy-control :pregnancyControl="req.pregnancyControl" />
        <b-row>
            <b-col cols="12" lg="6">
                <b-form-group label="Motivo de la consulta">
                    <b-form-textarea rows="3" v-model="req.reasonForConsultation" />
                </b-form-group>
            </b-col>
            <b-col cols="12" lg="6">
                <b-form-group label="Exploración física">
                    <b-form-textarea rows="3" v-model="req.exploration" />
                </b-form-group>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import distocias from './distocias.vue'
import pregnancies from './pregnancies.vue'
import pregnancyControl from './pregnancyControl.vue'
import { onlyNumber } from '@/helper/util';

export default {
    components: {
        distocias,
        pregnancies,
        pregnancyControl
    },
    data() {
        return {
            parturitionOptions: [
                { text: 'Eutócico', value: '0' },
                { text: 'Distocico', value: '1' }
            ],
            req: {
                weight: null,
                size: null,
                temperature: null,
                sexuallyActive: false,
                pregnancyNumber: 0,
                abortions: 0,
                bloodPressure_A: null,
                bloodPressure_B: null,
                lastParturitionDate: null,
                firstDayMenstruation: null,
                toxemias: false,
                parturition: 0,
                dystocia: {
                    type: 0,
                    specifyTpe: null,
                    reason: 0,
                    specifyReason: null
                },
                cesarean: 0,
                forceps: 0,
                stillbirths: 0,
                newBornAlive: 0,
                _pregnancies: {
                    ectopic: false,
                    specifyEctopic: null,
                    previous: false,
                    specifyPrevious: null,
                    perinatal: false,
                    specifyPerinatal: null,
                    abnormal: false,
                    specifyAbnormal: null
                },
                observations: null,
                pregnancyControl: {
                    fu: 0,
                    fcf: 0,
                    cc: 0,
                    ca: 0,
                    lf: 0,
                    dbp: 0,
                    position: null,
                    presentation: null,
                    situtation: null,
                    attitude: null,
                    fetalMovements: null,
                    weight: 0,
                    ta: 0,
                    fcm: 0,
                    edema: null,
                    madeUf: false,
                    ultrasound: null
                },
                reasonForConsultation: null,
                exploration: null
            }
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
        }
    }
}
</script>