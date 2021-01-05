<template>
    <b-row class="align-items-center">
        <b-col
        :cols="(!ectopico && !previou) ? 6 : 12"
        :sm="(!ectopico && !perinatal) ? 6 : 12"
        :md="(!ectopico && !previou && !perinatal) ? 3 : 12"
        :lg="(!ectopico && !previou && !perinatal) ? 4 : 12">
            <b-form-group label="Embarazos ectópicos">
                <div class="d-flex">
                    <span>No</span>
                    <b-form-checkbox v-model="preg.ectopic" class="ml-2" switch>Si</b-form-checkbox>
                </div>
            </b-form-group>
            <b-collapse v-model="ectopico">
                <b-card class="border-0" no-body>
                    <b-form-group label="Especifique">
                        <b-form-textarea rows="3" v-model="preg.specifyEctopic" />
                    </b-form-group>
                </b-card>
            </b-collapse>
        </b-col>
        <b-col
        :cols="(!ectopico && !previou) ? 6 : 12"
        sm="12" order-sm="1"
        :md="(!ectopico && !previou && !perinatal) ? 5 : 12"
        order-md="0"
        :lg="(!ectopico && !previou && !perinatal) ? 4 : 12">
            <b-form-group label="Complicaciones de embarazo previou">
                <div class="d-flex">
                    <span>No</span>
                    <b-form-checkbox v-model="preg.previous" class="ml-2" switch>Si</b-form-checkbox>
                </div>
            </b-form-group>
            <b-collapse v-model="previou">
                <b-card class="border-0" no-body>
                    <b-form-group label="Especifique">
                        <b-form-textarea rows="3" v-model="preg.specifyPrevious" />
                    </b-form-group>
                </b-card>
            </b-collapse>
        </b-col>
        <b-col
        :sm="(!ectopico && !perinatal) ? 6 : 12"
        :md="(!ectopico && !previou && !perinatal) ? 4 : 12">
            <b-form-group label="Complicaciones perinatales">
                <div class="d-flex">
                    <span>No</span>
                    <b-form-checkbox v-model="preg.perinatal" class="ml-2" switch>Si</b-form-checkbox>
                </div>
            </b-form-group>
            <b-collapse v-model="perinatal">
                <b-card class="border-0" no-body>
                    <b-form-group label="Especifique">
                        <b-form-textarea rows="3" v-model="preg.specifyPerinatal" />
                    </b-form-group>
                </b-card>
            </b-collapse>
        </b-col>
        <b-col sm="12" order-sm="2">
            <b-form-group label="Embarazos anormales: Mola H, Anembriónicos, etc.">
                <div class="d-flex">
                    <span>No</span>
                    <b-form-checkbox v-model="preg.abnormal" class="ml-2" switch>Si</b-form-checkbox>
                </div>
            </b-form-group>
            <b-collapse v-model="abnormal">
                <b-card class="border-0" no-body>
                    <b-form-group label="Especifique">
                        <b-form-textarea rows="3" v-model="preg.specifyAbnormal" />
                    </b-form-group>
                </b-card>
            </b-collapse>
        </b-col>
    </b-row>
</template>

<script>
export default {
    props: {
        pregnancies: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            preg: this.pregnancies
        }
    }
}
</script>