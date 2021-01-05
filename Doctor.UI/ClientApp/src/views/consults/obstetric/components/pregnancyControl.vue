<template>
    <div>
        <hr>
        <h5>Control de embarazo</h5>
        <b-row>
            <b-col cols="6" md="4">
                <b-form-group label="Semanas de gestación">
                    <b-form-input disabled />
                </b-form-group>
            </b-col>
            <b-col cols="6" md="4">
                <b-form-group label="Fecha probable de parto">
                    <b-form-input disabled />
                </b-form-group>
            </b-col>
            <b-col cols="4" md="2">
                <b-form-group label="F.U. (CM)">
                    <b-form-input type="number" v-model="pregnancy.fu" />
                </b-form-group>
            </b-col>
            <b-col cols="4" md="2">
                <b-form-group label="F.C.F.">
                    <b-form-input type="number" v-model="pregnancy.fcf" />
                </b-form-group>
            </b-col>
            <b-col cols="4" md="3">
                <b-form-group label="C.C.">
                    <b-form-input type="number" v-model="pregnancy.cc" />
                </b-form-group>
            </b-col>
            <b-col cols="4" md="3">
                <b-form-group label="C.A.">
                    <b-form-input type="number" v-model="pregnancy.ca" />
                </b-form-group>
            </b-col>
            <b-col cols="4" md="3">
                <b-form-group label="L.F.">
                    <b-form-input type="number" v-model="pregnancy.lf" />
                </b-form-group>
            </b-col>
            <b-col cols="4" md="3">
                <b-form-group label="D.B.P.">
                    <b-form-input type="number" v-model="pregnancy.dbp" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col cols="6" md="3">
                <b-form-group label="Pocisión">
                    <b-form-input v-model="pregnancy.position" />
                </b-form-group>
            </b-col>
            <b-col cols="6" md="3">
                <b-form-group label="Presentación">
                    <b-form-input v-model="pregnancy.presentation" />
                </b-form-group>
            </b-col>
            <b-col cols="6" md="3">
                <b-form-group label="Situación">
                    <b-form-input v-model="pregnancy.situation"/>
                </b-form-group>
            </b-col>
            <b-col cols="6" md="3">
                <b-form-group label="Actitud">
                    <b-form-input v-model="pregnancy.attitude" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col cols="5" sm="6" md="4" lg="2">
                <b-form-group label="Movimientos fetales">
                    <b-form-input v-model="pregnancy.fetalMovements" />
                </b-form-group>
            </b-col>
            <b-col cols="7" sm="6" md="4" lg="3">
                <b-form-group label="Peso aproximado producto">
                    <b-input-group append="Kg">
                        <b-form-input v-model="pregnancy.weight" />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="6" md="4" lg="2">
                <b-form-group label="Peso de la madre">
                    <b-input-group append="Kg">
                        <b-form-input disabled />
                    </b-input-group>
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="3" lg="1">
                <b-form-group label="T/A">
                    <b-form-input v-model="pregnancy.ta" />
                </b-form-group>
            </b-col>
            <b-col cols="4" sm="3" lg="1">
                <b-form-group label="F.C.M.">
                    <b-form-input v-model="pregnancy.fcm" />
                </b-form-group>
            </b-col>
            <b-col cols="4" sm="6" md="3" lg="1">
                <b-form-group label="Edema">
                    <b-form-input v-model="pregnancy.edema" />
                </b-form-group>
            </b-col>
            <b-col cols="4" sm="6" md="3" lg="2">
                <b-form-group label="Se hizo us">
                    <div class="d-flex">
                        <span>No</span>
                        <b-form-checkbox class="ml-2" v-model="pregnancy.madeUf" switch>Si</b-form-checkbox>
                    </div>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Reporte de ultrazonido realizado">
                    <b-form-textarea v-model="pregnancy.ultrasound" />
                </b-form-group>
            </b-col>
        </b-row>
    </div>
</template>

<script>
export default {
    props: {
        pregnancyControl: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            pregnancy: this.pregnancyControl
        }
    }
}
</script>