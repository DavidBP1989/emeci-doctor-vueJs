<template>
    <div class="newconsult">
        <b-row>
            <b-col sm="6" md="3">
                <b-form-group label="Peso">
                    <b-input-group append="kg">
                        <b-form-input v-model="req.weight" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col sm="6" md="3">
                <b-form-group label="Talla">
                    <b-input-group append="m">
                        <b-form-input v-model="req.size" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col sm="6" md="4" lg="3">
                <b-form-group label="Índice de masa corporal">
                    <b-input-group append="kg/m2">
                        <b-form-input disabled :value="mass" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col sm="6" md="2" lg="3">
                <b-form-group label="Temperatura">
                    <b-input-group append="c">
                        <b-form-input v-model="req.temperature" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col sm="6" md="3">
                <b-form-group label="Tensión arterial">
                    <div class="d-flex">
                        <b-form-input v-model="req.bloodPressure_a" @keypress="onlyDecimals" class="mr-2"/>
                        <b-form-input v-model="req.bloodPressure_b" @keypress="onlyDecimals" />
                    </div>
                </b-form-group>
            </b-col>
            <b-col sm="6" md="3">
                <b-form-group label="Perímetro cefálico">
                    <b-input-group append="cm">
                        <b-form-input v-model="req.headCircuference" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col sm="6" md="6" lg="3">
                <b-form-group label="Frecuencia cardiaca">
                    <b-input-group append="lpm">
                        <b-form-input v-model="req.heartRate" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col sm="6" md="6" lg="3">
                <b-form-group label="Frecuencia respiratoria">
                    <b-input-group append="lpm">
                        <b-form-input v-model="req.breathingFrecuency" @keypress="onlyDecimals" />
                    </b-input-group>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col md="6">
                <b-form-group label="Motivo de la consulta">
                    <b-form-textarea v-model="req.reasonForConsultation"></b-form-textarea>
                </b-form-group>
            </b-col>
            <b-col>
                <b-form-group label="Exploración física">
                    <b-form-textarea v-model="req.physicalExploration"></b-form-textarea>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col md="6">
                <b-form-group label="Medidas preventivas">
                    <b-form-textarea v-model="req.preventiveMeasures"></b-form-textarea>
                </b-form-group>
            </b-col>
            <b-col>
                <b-form-group label="Observaciones">
                    <b-form-textarea v-model="req.observations"></b-form-textarea>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row class="mt-2">
            <b-col class="text-md-right">
                <diagnostics :dataList="req.diagnostics" :savedDataList="diagnostics_array" />
                <treatments :dataList="req.treatments" :savedDataList="treatments_array" />
                <laboratory :dataList="laboratory_array" />
                <cabinet :dataList="cabinet_array" />
                <prognostic :dataList="req.prognostic" /> <!--btn de confirmar nueva consulta -->
            </b-col>
        </b-row>
        <added-items v-if="req.diagnostics.length > 0" title="Diagnosticos" type="diagnostic" :dataList="req.diagnostics" />
        <added-items v-if="req.treatments.length > 0" title="Tratamientos" type="treatments" :dataList="req.treatments" />
        <added-items v-if="req.laboratory.length > 0" title="Estudios de laboratorio" type="laboratory" :dataList="req.laboratory" />
        <added-items v-if="req.cabinet.length > 0" title="Estudios de gabinete" type="cabinet" :dataList="req.cabinet" />
    </div>
</template>

<script>
import diagnostics from './diagnostics.vue';
import treatments from './treatments.vue';
import laboratory from './laboratory.vue';
import cabinet from './cabinet.vue';
import addedItems from '../shared/addedItems.vue';
import prognostic from './_prognostic/index.vue';

import EventBus from '../../../../helper/event-bus';
import { getDiagnostics, getTreatments, getLaboratory, getCabinet } from '../helper/call';
import { laboratory_cabinet_format, diagnostic_treatments_format, addLab_Cab_toReq } from '../helper/util';
import model from '../helper/newConsultModel';
import { initLoader, saved, onlyNumber } from '../../../../helper/util';
import formValidation from '../../../patients/helper/newPatientModel';
import api from '../../../../api/consult-service';

let loader = null;

export default {
    created() {
        this.get();
        EventBus.$on('update-prognostic', value => this.req.prognostic = value);
        EventBus.$on('add-labcab', type => this.addLab_Cab(type));
        EventBus.$on('save', () => this.save());
    },
    props: {
        patientInfo: {
            type: Object,
            required: true
        }
    },
    components: {
        diagnostics,
        treatments,
        laboratory,
        cabinet,
        addedItems,
        prognostic
    },
    data() {
        return {
            patient: this.patientInfo,
            doctorId: this.$appConfig.doctor.doctorId,
            patientId: this.$route.params.id,
            diagnostics_array: [],
            treatments_array: [],
            laboratory_array: [],
            cabinet_array: [],
            req: {
                weight: null,
                size: null,
                temperature: null,
                bloodPressure_a: null,
                bloodPressure_b: null,
                headCircuference: null,
                heartRate: null,
                breathingFrecuency: null,
                reasonForConsultation: null,
                physicalExploration: null,
                preventiveMeasures: null,
                observations: null,
                diagnostics: [],
                treatments: [],
                laboratory: [],
                cabinet: [],
                prognostic: []
            },
            error: false,
            post: {}
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
        get() {
            getDiagnostics(this.doctorId)
            .then(response => this.diagnostics_array = diagnostic_treatments_format(response));
            getTreatments(this.doctorId)
            .then(response => this.treatments_array = diagnostic_treatments_format(response));
            getCabinet()
            .then(response => this.cabinet_array = laboratory_cabinet_format(response));
            getLaboratory()
            .then(response => this.laboratory_array = laboratory_cabinet_format(response));
        },
        addLab_Cab(type) {
            if (type === 'laboratory')
                this.req.laboratory = addLab_Cab_toReq(this.laboratory_array);
            else this.req.cabinet = addLab_Cab_toReq(this.cabinet_array);
        },
        save() {
            this.formValidation();
            if (!this.error) {
                loader = initLoader();
                api.post(this.doctorId, this.post).then(response => {
                    loader.hide();
                    saved('Consulta agregada', null);
                    this.clear();
                    EventBus.$emit('putInPreviewConsult');
                })
                .catch(_error => {
                    loader.hide();
                });
            }
        },
        formValidation() {
            const form = new model(this.patientId, this.req, this.patient);

            form.validate();
            if (form.errors.length > 0) {
                this.error = true;

            } else {
                this.error = false;
                this.post = form.__$;
            }
            
            return;
        },
        clear() {
            this.req.weight = null;
            this.req.size = null;
            this.req.temperature = null;
            this.req.bloodPressure_a = null;
            this.req.bloodPressure_b = null;
            this.req.headCircuference = null;
            this.req.heartRate = null;
            this.req.breathingFrecuency = null;
            this.req.reasonForConsultation = null;
            this.req.physicalExploration = null;
            this.req.preventiveMeasures = null;
            this.req.observations = null;
            this.req.diagnostics = [];
            this.req.treatments = [];
            this.req.laboratory = [];
            this.req.cabinet = [];
        },
        onlyDecimals(evt) {
            onlyNumber(evt, true)
        }
    }
}
</script>