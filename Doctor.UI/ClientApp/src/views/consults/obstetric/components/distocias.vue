<template>
    <b-collapse class="w-100" v-model="show">
        <b-card class="pl-3 pr-3" title="Partos">
            <b-row>
                <b-col cols="12" :sm="(!showCollapseType && !showCollapseReason) ? 6 : 12">
                    <b-form-group label="Tipo de distocia">
                        <b-form-select v-model="dystociaV.type" :options="types" />
                    </b-form-group>
                    <b-collapse v-model="showCollapseType">
                        <b-card class="border-0" no-body>
                            <b-form-group label="Especifique">
                                <b-form-textarea rows="3" v-model="dystociaV.specifyTpe" />
                            </b-form-group>
                        </b-card>
                    </b-collapse>
                </b-col>
                <b-col cols="12" :sm="(!showCollapseType && !showCollapseReason) ? 6 : 12">
                    <b-form-group label="Motivo de la distocia">
                        <b-form-select v-model="dystociaV.reason" :options="reasons" />
                    </b-form-group>
                    <b-collapse v-model="showCollapseReason">
                        <b-card class="border-0" no-body>
                            <b-form-group label="Especifique">
                                <b-form-textarea rows="3" v-model="dystociaV.specifyReason" />
                            </b-form-group>
                        </b-card>
                    </b-collapse>
                </b-col>
            </b-row>
        </b-card>
    </b-collapse>
</template>

<script>
export default {
    props: {
        show: {
            type: Boolean,
            required: true
        },
        dystocia: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            dystociaV: this.dystocia,
            types: [
                { text: 'Cesárea', value: 0 },
                { text: 'Fórceps', value: 1 },
                { text: 'Otro', value: 2 }
            ],
            reasons: [
                { text: 'Sufrimiento fetal agudo', value: 0 },
                { text: 'Postura anómala', value: 1 },
                { text: 'Prematurez', value: 2 },
                { text: 'Otro', value: 3 }
            ]
        }
    },
    computed: {
        showCollapseType() {
            return this.type == 2
        },
        showCollapseReason() {
            return this.reason == 3
        }
    }
}
</script>