<template>
    <div v-if="consult != null">
        <b-row>
            <b-col offset-md="8" class="text-right">
                <b-form-group class="nlegend" label="Buscar consulta">
                    <b-form-select v-model="selectedDate" :options="dates" @change="getConsult" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row class="mt-3 align-items-center">
            <b-col cols="3" sm="2">
                <b-form-group label="Peso">
                    <span>{{ consult.BasicConsult.Weight }} kg</span>
                </b-form-group>
            </b-col>
            <b-col cols="3" sm="2">
                <b-form-group label="Talla">
                    <span>{{ consult.BasicConsult.Size }} m</span>
                </b-form-group>
            </b-col>
            <b-col cols="6" sm="5" md="4" lg="3">
                <b-form-group label="Índice de masa corporal">
                    <span>{{ consult.BasicConsult.Mass }} kg/m2</span>
                </b-form-group>
            </b-col>
            <b-col cols="3" md="2">
                <b-form-group label="Temperatura">
                    <span>{{ consult.BasicConsult.Temperature }} c</span>
                </b-form-group>
            </b-col>
            <b-col cols="5" sm="6" md="4" lg="3">
                <b-form-group label="Edad de su menarca">
                    <span>{{ consult.MenarcaAge }}</span>
                </b-form-group>
            </b-col>
            <b-col cols="4" sm="6" md="2" order-md="1">
                <b-form-group label="Menacmia">
                    <span>{{ consult.Menacma }}</span>
                </b-form-group>
            </b-col>
            <b-col cols="6" md="4">
                <b-form-group label="Presión arterial sistólica">
                    <span>{{ consult.BasicConsult.BloodPressure_A }}</span>
                </b-form-group>
            </b-col>
            <b-col cols="6" md="4">
                <b-form-group label="Presión arterial diastólica">
                    <span>{{ consult.BasicConsult.BloodPressure_B }}</span>
                </b-form-group>
            </b-col>
            <b-col sm="5" md="4">
                <b-form-group label="Fecha última menstruación">
                    <span>{{ getDateFormat(consult.LastMenstruationDate) }}</span>
                </b-form-group>
            </b-col>
            <b-col sm="7" md="5">
                <b-form-group label="Edad de inicio de vida sexual activa">
                    <span>{{ consult.AgeOfOnsetOfActiveSexualLife }}</span>
                </b-form-group>
            </b-col>
        </b-row>
        <hr>
        <b-row>
            <b-col md="3">
                <b-row>
                    <b-col md="12">
                        <b-list-group>
                            <b-list-group-item>
                                <span class="fw">Gestas</span>
                                <b-badge variant="default">{{ consult.Gestas }}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <span class="fw">Paragestas</span>
                                <b-badge variant="default">{{ consult.Paragestas }}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <span class="fw">Cesáreas</span>
                                <b-badge variant="default">{{ consult.Cesareans }}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <span class="fw">Abortos</span>
                                <b-badge variant="default">{{ consult.Abortions }}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <span class="fw">Recién nacidos</span>
                                <b-badge variant="default">{{ consult.NewlyBorn }}</b-badge>
                            </b-list-group-item>
                            <b-list-group-item>
                                <span class="fw">Mortinatos</span>
                                <b-badge variant="default">{{ consult.Stillbirth }}</b-badge>
                            </b-list-group-item>
                        </b-list-group>
                    </b-col>
                </b-row>
            </b-col>
            <b-col class="mt-3 mt-md-0">
                <b-row>
                    <b-col md="12">
                        <b-form-checkbox
                        inline
                        disabled
                        :checked="o.value"
                        v-for="o in options"
                        :key="o">{{ o.text }}</b-form-checkbox>
                    </b-col>
                </b-row>
                <b-row v-if="consult.Checkbox.Others">
                    <b-col>
                        <b-form-group label="Explique">
                            <span>{{ consult.SpecifyOthers }}</span>
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row v-else class="mt-2">
                    <b-col>
                        <b-form-group label="Motivo de la consulta">
                            <span>{{ consult.BasicConsult.ReasonForConsultation }}</span>
                        </b-form-group>
                    </b-col>
                </b-row>
            </b-col>
        </b-row>
        <b-row class="mt-3" v-if="!consult.Partner.HasAPartner">
            <b-col>
                <b-form-group style="color:indianred" label="No tiene pareja" />
            </b-col>
        </b-row>
        <b-card no-body v-else class="mt-3">
            <b-card-body>
                <p>Datos de la pareja</p>
                <b-row>
                    <b-col sm="8" md="6" lg="3">
                        <b-form-group label="Nombre y apellido">
                            <span>{{ consult.Partner.Name }}</span>
                        </b-form-group>
                    </b-col>
                    <b-col cols="4" md="3">
                        <b-form-group label="Sexo">
                            <span>{{ sex(consult.Partner.Sex) }}</span>
                        </b-form-group>
                    </b-col>
                    <b-col cols="4" sm="3" md="3">
                        <b-form-group label="Estado civil">
                            <span>{{ maritalStatus(consult.Partner.MaritalStatus) }}</span>
                        </b-form-group>
                    </b-col>
                    <b-col cols="4" sm="3">
                        <b-form-group label="Grupo y RH">
                            <span>{{ consult.Partner.GroupRH }}</span>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" md="4" lg="3">
                        <b-form-group label="Fecha de nacimiento">
                            <span>{{ getDateFormat(consult.Partner.BirthDate) }}</span>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="2" lg="3">
                        <b-form-group label="Edad">
                            <span>{{ consult.Partner.Age }}</span>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="5" md="6" lg="3">
                        <b-form-group label="Ocupación">
                            <span>{{ consult.Partner.Occupation }}</span>
                        </b-form-group>
                    </b-col>
                    <b-col cols="6" sm="5" lg="3">
                        <b-form-group label="Teléfono(s)">
                            <span>{{ consult.Partner.Phone }}</span>
                        </b-form-group>
                    </b-col>
                </b-row>
            </b-card-body>
        </b-card>
        <b-row v-if="consult.Checkbox.Others" class="mt-2">
            <b-col>
                <b-form-group label="Motivo de la consulta">
                    <span>{{ consult.BasicConsult.ReasonForConsultation }}</span>
                </b-form-group>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import api from '@/api/gyneConsult-service'

export default {
    mounted() {
        this.$watch('consultationDates', newDate => {
            this.dates = newDate;
            this.init();
        });
    },
    created() {
        if (this.dates.length > 0) this.init();
    },
    props: {
        consultationDates: {
            type: Array,
            required: true
        }
    },
    data() {
        return {
            dates: this.consultationDates,
            selectedDate: null,
            consult: null
        }
    },
    computed: {
        options() {
            if (this.consult !== null) {
                let options = [];
                Object.keys(this.consult.Checkbox).forEach(key => {
                    options.push({
                        text: key === 'Others' ? 'Otros' : key,
                        value: this.consult.Checkbox[key]
                    })
                })

                return options;
            }
        }
    },
    methods: {
        init() {
            this.selectedDate = this.dates[0].value;
            this.getConsult(this.selectedDate);
        },
        getConsult(consultId) {
            api.get(consultId).then(response => {
                this.consult = response.body;
            });
        },
        getDateFormat(date) {
            if (date !== null)
                return this.$moment(date).format('D/MMM/YYYY')
            else return '---'
        },
        sex(_sex) {
            return _sex === 'M' ? 'Masculino' : 'Femenino';
        },
        maritalStatus(value) {
            let name = '';
            if (value === null) value = '0';
            switch(value) {
                case '0':
                    name = 'Casado';
                    break;
                case '1':
                    name = 'Separado';
                    break;
                case '2':
                    name = 'Soltero';
                    break;
                case '3':
                    name = 'Union libre';
                    break;
                case '4':
                    name = 'Viudo';
                    break;
                case '5':
                    name = 'Divorciado';
                    break;
            }
            return name;
        }
    }
}
</script>
