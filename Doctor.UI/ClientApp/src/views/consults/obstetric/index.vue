<template>
    <div role="tablist" class="mb-3">
        <b-card no-body class="mb-1">
            <b-card-header class="p-0" role="tab">
                <b-button block v-b-toggle.__preview class="text-left p-2">
                    <fa-icon
                    class="mr-2"
                    :icon="['fas', (collapsePreview ? 'caret-down' : 'caret-right')]"
                    size="lg" /> Consulta anterior
                </b-button>
            </b-card-header>
            <b-collapse id="__preview" v-model="collapsePreview" accordion="obts_consult" role="tabpanel">
                <b-card-body>
                    <preview-consult :consultationDates="consultationDates" />
                </b-card-body>
            </b-collapse>
        </b-card>
        <b-card no-body class="mb-1">
            <b-card-header class="p-0" role="tab">
                <b-button block v-b-toggle.new class="text-left p-2">
                    <fa-icon
                    class="mr-2"
                    :icon="['fas', (collapseNew ? 'caret-down' : 'caret-right')]"
                    size="lg" /> Nueva consulta
                </b-button>
            </b-card-header>
            <b-collapse id="new" v-model="collapseNew" accordion="obts_consult" role="tabpanel">
                <b-card-body>
                    <new-consult :patientInfo="patientInfo" />
                </b-card-body>
            </b-collapse>
        </b-card>
    </div>
</template>

<script>
import previewConsult from './components/preview.vue'
import newConsult from './components/new.vue'

export default {
    mounted() {
        this.$root.$on('bv::collapse::state', (collapseId, isJustShown) => {
            if (!this.collapseNew && !this.collapsePreview) {
                this.collapseNew = true;
                this.$root.$emit('bv::toggle::collapse', 'new');
            }
        });
    },
    props: {
        patientInfo: {
            type: Object,
            required: true
        }
    },
    components: {
        previewConsult,
        newConsult,
    },
    data() {
        return {
            collapsePreview: false,
            collapseNew: true
        }
    },
    
}
</script>