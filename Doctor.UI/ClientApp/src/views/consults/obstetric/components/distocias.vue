<template>
    <b-collapse v-model="showCollapse">
        <b-card title="Partos">
            <b-row>
                <b-col>
                    <b-form-group label="Tipo de distocia">
                        <b-form-select :options="type" />
                    </b-form-group>
                    <b-collapse v-model="typeShowOther">
                        <b-card>
                            <b-form-group>
                                <b-form-textarea rows="3" />
                            </b-form-group>
                        </b-card>
                    </b-collapse>
                </b-col>
                <b-col>
                    <b-form-group label="Motivo de la distocia">
                        <b-form-select :options="reasons" />
                    </b-form-group>
                    <b-collapse v-model="reasonShowOther">
                        <b-card>
                            <b-form-group>
                                <b-form-textarea rows="3" />
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
    props: ['show'],
    data() {
        return {
            showCollapse: this.show,
            type: [
                { text: 'Cesárea', value: 0 },
                { text: 'Fórceps', value: 1 },
                { text: 'Otro', value: 2 }
            ],
            typeShowOther: false,
            reasons: [
                { text: 'Sufrimiento fetal agudo', value: 0 },
                { text: 'Postura anómala', value: 1 },
                { text: 'Prematurez', value: 2 },
                { text: 'Otro', value: 3 }
            ],
            reasonShowOther: false
        }
    }
}
</script>