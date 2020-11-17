<template>
    <div class="main">
        <main-header />
        <b-container class="mt-4">
            <b-sidebar
            title="Paciente"
            id="sidebar-info-patient"
            v-model="sidebar"
            backdrop
            shadow
            backdrop-variant="dark">
                <pacient-info :patientInfo="patient" :patientData="patientData" />
            </b-sidebar>

            <b-row class="align-items-end">
                <b-col class="mb-2" md="6">
                    <p class="mb-1 title-patient">
                        <strong>Paciente: </strong>{{ patientData.Name }} {{ patientData.LastName }}
                    </p>
                    <b-button variant="link" class="pl-0" v-b-toggle.sidebar-info-patient>Ver datos del paciente</b-button>
                </b-col>
                <b-col class="text-md-right mt-2 mb-2">
                    <b-form-group v-if="showConsultType" class="mb-2" label="Cambiar tipo de consulta">
                        <b-form-select v-model="consultType" @change="changeRoute" :options="options" />
                    </b-form-group>
                </b-col>
            </b-row>
            <router-view class="mt-1 card-consult" :patientInfo="patient" />
        </b-container>
    </div>
</template>

<script>
import header from '../../shared/header/index.vue';
import pacientInfo from './shared/pacientInfo.vue';

import api from '../../api/patient-service';

export default {
    beforeCreate() {
        document.body.className = 'main_body';
    },
    created() {
        this.getPatient();
    },
    components: {
        'main-header': header,
        pacientInfo
    },
    data() {
        return {
            sidebar: false,
            patientId: this.$route.params.id,
            patientData: {},
            patient: {
                allergies: '',
                reserved: '',
                relevantPathologies: ''
            },
            options: [
                { value: 'gynecology', text: 'Ginecología' },
                { value: 'obstetric', text: 'Obstétrica' },
                { value: '', text: 'General' }
                
            ],
            consultType: 'gynecology'
        }
    },
    computed: {
        showConsultType() {
            const years = Math.trunc(this.patientData.AgeInMonths / 12);
            return (years >= 12 && this.patientData.Sex === 'F');
        }
    },
    methods: {
        getPatient() {
            api.getById(this.patientId).then(response => {
                this.patientData = response.body;
                this.$appConfig.patient.emeci = response.body.EMECI;
                this.$appConfig.patient.coordinate = response.body.RandomCoordinate;
                this.$appConfig.patient.coordinateValue = response.body.RandomCoordinateValue;
                this.$saveConfig();
            });
        },
        changeRoute(value) {
            this.$router.push({ path: `/consults/${this.patientId}/${value}` });
        }
    },
    beforeRouteEnter(to, from, next) {
        
        next(vm => {
            /**
             * si agregaron un nuevo paciente
             * abrir la informacion del paciente al iniciar
             * la consulta
             */
            if (from.path === '/newPatient') {
                vm.sidebar = true;
            }
        });
    }
}
</script>