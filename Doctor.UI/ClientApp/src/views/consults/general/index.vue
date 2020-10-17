<template>
    <div role="tablist" class="mb-3">
        <b-card v-if="consultationDates.length > 0" no-body class="mb-1">
            <b-card-header header-tag="header" class="p-0" role="tab">
                <b-button block v-b-toggle._previous class="text-left p-2">
                    <fa-icon class="mr-2" :icon="['fas', (visible_previous ? 'caret-down' : 'caret-right')]" size="lg" />
                    Consulta anterior
                </b-button>
            </b-card-header>
            <b-collapse
            id="_previous"
            v-model="visible_previous"
            accordion="consult-accordion"
            role="tabpanel">
                <b-card-body>
                    <previous :consultationDates="consultationDates" />
                </b-card-body>
            </b-collapse>
        </b-card>

        <b-card no-body class="mb-1">
            <b-card-header header-tag="header" class="p-0" role="tab">
                <b-button block v-b-toggle._consult class="text-left p-2">
                    <fa-icon class="mr-2" :icon="['fas', (visible_consult ? 'caret-down' : 'caret-right')]" size="lg" />
                    Nueva consulta
                </b-button>
            </b-card-header>
            <b-collapse
            id="_consult"
            v-model="visible_consult"
            accordion="consult-accordion"
            role="tabpanel">
                <b-card-body>
                    <consult :patientInfo="patient" />
                </b-card-body>
            </b-collapse>
        </b-card>
    </div>
</template>


<script>
import consult from './components/newConsult.vue';
import previous from './components/previousConsult.vue';

import api from '../../../api/consult-service';
import EventBus from '../../../helper/event-bus';

export default {
    mounted() {
        this.$root.$on('bv::collapse::state', (collapseId, isJustShown) => {
            //siempre mantener uno abierto
            if (!this.visible_consult && !this.visible_previous) {
                this.visible_consult = true;
                this.$root.$emit('bv::toggle::collapse', '_consult');
            }
        });
    },
    created() {
        this.getConsultationDates();
        EventBus.$on('putInPreviewConsult', () => this.refreshPreviewConsult());
    },
    props: {
        patientInfo: {
            type: Object,
            required: true
        }
    },
    components: {
        consult,
        previous
    },
    data() {
        return {
            patient: this.patientInfo,
            patientId: this.$route.params.id,
            consultationDates: [],
            visible_consult: true,
            visible_previous: false,
        }
    },
    methods: {
        getConsultationDates() {
            api.getConsultationDates(this.patientId).then(response => {
                if (response.body) {
                    let array = [];
                    response.body.forEach(element => {
                        array.push({
                            value: element.Id,
                            text: this.$moment(element.ConsultationDate).format('dddd DD [de] MMMM [de] YYYY')
                        });
                    });
                    this.consultationDates = array;              
                }
            });
        },
        refreshPreviewConsult() {
            this.getConsultationDates();
        }
    }
}
</script>