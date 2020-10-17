<template>
    <b-container v-if="patientData != null" fluid class="mt-3">
        <b-row>
            <b-col>
                <b-list-group>
                    <b-list-group-item>
                        <strong>Nombre:</strong> {{ patientData.Name }} {{ patientData.LastName }}
                    </b-list-group-item>
                    <b-list-group-item>
                        <strong>EMECI:</strong> {{ patientData.EMECI }}
                    </b-list-group-item>
                    <b-list-group-item>
                        <strong>Fecha de nacimiento:</strong> {{ birthDate }}
                        <br />{{ age }}
                    </b-list-group-item>
                    <b-list-group-item>
                        <strong>Grupo y RH: </strong> {{ patientData.GroupRH }}
                    </b-list-group-item>
                    <b-list-group-item>
                        <strong>Vencimiento: </strong>25/diciembre/2019
                    </b-list-group-item>
                </b-list-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <hr>
                <h6>Observaciones generales</h6>
                <b-form-group class="mb-1" label="Alergias">
                    <b-form-textarea v-model="patient.allergies"></b-form-textarea>
                </b-form-group>
                <b-form-group class="mb-1" label="Reservado">
                    <b-form-textarea v-model="patient.reserved"></b-form-textarea>
                </b-form-group>
                <b-form-group class="mb-0" label="Patologías relevantes">
                    <b-form-textarea v-model="patient.relevantPathologies"></b-form-textarea>
                </b-form-group>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
export default {
    props: {
        patientInfo: {
            type: Object,
            required: true
        },
        //informacion de la bd
        patientData: {
            type: Object,
            required: true,
            default: null
        }
    },
    data() {
        return {
            patient: this.patientInfo
        }
    },
    computed: {
        age() {
            let txt = '';
            const years = Math.trunc(this.patientData.AgeInMonths / 12);
            if (years > 0)
                txt = `${years} ${years > 1 ? 'Años' : 'Año'}`;
            const months = this.patientData.AgeInMonths % 12;
            if (months > 0)
                txt += `, ${months} ${months > 1 ? 'Meses' : 'Mes'}`;
            return txt;
        },
        birthDate() {
            return this.$moment(this.patientData.BirthDate).format('D/MMM/YYYY');
        }
    },
    watch: {
        patientData() {
            if (this.patientData != null) {
                this.patient.allergies = this.patientData.Allergies;
                this.patient.reserved = this.patientData.Reserved;
                this.patient.relevantPathologies = this.patientData.RelevantPathologies;
            }
        }
    }
}
</script>