<template>
    <b-card no-body class="mb-3" header="Consulta Obstetrica">
        <b-card-body>
            <div class="obste">
                <b-row>
                    <b-col cols="6" sm="6" md="3">
                        <b-form-group label="Peso">
                            <b-input-group append="kg">
                                <b-form-input @keypress="onlyDecimals" />
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
                    <b-col cols="6" sm="6" md="4" lg="3">
                        <b-form-group label="Índice de masa corporal">
                            <b-input-group append="kg/m2">
                                <b-form-input disabled :value="mass" />
                            </b-input-group>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="6" md="2" lg="3">
                        <b-form-group label="Temperatura">
                            <b-input-group append="c">
                                <b-form-input @keypress="onlyDecimals" />
                            </b-input-group>
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col cols="4" sm="4" md="3" lg="2">
                        <b-form-group label="# de embarazo">
                            <b-form-input type="number" />
                        </b-form-group>
                    </b-col>
                    <b-col cols="4" sm="4" md="3" lg="2">
                        <b-form-group label="Activa sexualmente">
                            <div class="d-flex">
                                <span>No</span>
                                <b-form-checkbox class="ml-2" switch>Si</b-form-checkbox>
                            </div>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="6" md="4" lg="3" order="4" order-sm="4">
                        <b-form-group label="Pres. arterial sistólica">
                            <b-form-input @keypress="onlyDecimals" />
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="6" md="4" lg="3" order="5" order-sm="5">
                        <b-form-group label="Pres. arterial diastólica">
                            <b-form-input />
                        </b-form-group>
                    </b-col>
                    <b-col cols="4" sm="4" lg="2">
                        <b-form-group label="Abortos">
                            <b-form-input type="number" />
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col cols="4" sm="4" md="3">
                        <b-form-group label="Fecha último parto">
                            <b-form-input />
                        </b-form-group>
                    </b-col>
                    <b-col cols="4" sm="4" md="5" lg="3">
                        <b-form-group label="Primer día de ult. menstruación">
                            <b-form-input />
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="6" md="3">
                        <b-form-group label="Toxemias previas">
                            <div class="d-flex">
                                <span>No</span>
                                <b-form-checkbox class="ml-2" switch>Si</b-form-checkbox>
                            </div>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="6" md="4" lg="3">
                        <b-form-group label="Partos">
                            <b-form-select :options="childbirthOptions" v-model="req.childbirth" />
                        </b-form-group>
                    </b-col>
                </b-row>
                <distocias :show="req.childbirth === 1" />
            </div>
        </b-card-body>
    </b-card>
</template>

<script>
import { onlyNumber } from '../../../helper/util';
import distocias from './components/distocias.vue';

export default {
    components: {
        distocias
    },
    data() {
        return {
            req: {
                childbirth: 0
            },
            childbirthOptions: [
                { text: 'Eutócico', value: 0 },
                { text: 'Distocico', value: 1 }
            ]
        }
    },
    methods: {
        onlyDecimals(evt) {
            onlyNumber(evt, true)
        }
    }
}
</script>