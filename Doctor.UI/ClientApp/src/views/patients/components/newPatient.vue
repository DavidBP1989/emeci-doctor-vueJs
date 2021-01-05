<template>
    <div class="mt-4">
        <b-row class="justify-content-lg-center mb-4">
            <b-col lg="8">
                <b-row>
                    <b-col v-if="!visibleCollapse" md="6">
                        <p class="mb-0">N&uacute;mero de tarjeta EMECI</p>
                        <h5>{{ emeci }}</h5>
                    </b-col>
                    <b-col class="text-md-right">
                        <b-button variant="link" class="pl-0" v-b-toggle.collapse-1>{{ txtLink }}</b-button>
                        <fa-icon :icon="['fas', visibleCollapse ? 'caret-up' : 'caret-down']" size="lg"/>
                    </b-col>
                </b-row>
                <b-collapse class="mt-4 mb-4" id="collapse-1" v-model="visibleCollapse">
                    <b-card class="shadow rounded">
                        <emeci-patient />
                    </b-card>
                </b-collapse>
                <b-form :class="!visibleCollapse ? 'mt-4' : ''">
                    <b-row>
                        <b-col md="6">
                            <b-form-group label="Nombre(s)">
                                <b-input-group>
                                    <b-form-input v-model="name" @keypress="format" :disabled="visibleCollapse" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'user']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Apellido(s)">
                                <b-form-input v-model="lastName" @keypress="format" :disabled="visibleCollapse" />
                            </b-form-group>
                        </b-col>
                    </b-row>
                    <b-row >
                        <b-col md="6">
                            <b-form-group label="Nombre completo de la madre">
                                <b-form-input v-model="mothersName" @keypress="format" :disabled="visibleCollapse" />
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Nombre completo del padre">
                                <b-form-input v-model="fathersName" @keypress="format" :disabled="visibleCollapse" />
                            </b-form-group>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col md="6">
                            <b-form-group label="Teléfono">
                                <b-input-group>
                                    <b-form-input v-model="phone" :disabled="visibleCollapse" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'phone']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Sexo">
                                <b-input-group>
                                    <b-form-select
                                    v-model="sex"
                                    :disabled="visibleCollapse"
                                    :options="options"></b-form-select>
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'venus-mars']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col md="6">
                            <b-form-group
                            description="Separar por coma más de una dirección de correo electrónico"
                            label="Correo electrónico">
                                <b-input-group>
                                    <b-form-input v-model="email" :disabled="visibleCollapse" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'at']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group
                            description="Puede cambiar el mes y año en la parte superior del calendario"
                            label="Fecha de nacimiento">
                                <b-input-group>
                                    <v-date-picker
                                    class="form-control p-0"
                                    v-model="birthDate"
                                    :input-props="{ disabled: visibleCollapse }"
                                    color="pink"
                                    :popover="{ placement: 'bottom', visibility: 'click' }">
                                    </v-date-picker>
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'calendar-alt']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <b-form-group label="Alergia(s)">
                                <b-form-input v-model="allergy" :disabled="visibleCollapse" />
                            </b-form-group>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col md="6">
                            <b-form-group label="Contraseña">
                                <b-input-group>
                                    <b-form-input
                                    type="password"
                                    v-model="password"
                                    :disabled="visibleCollapse" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'key']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Confirmar contraseña">
                                <b-input-group>
                                    <b-form-input
                                    type="password"
                                    v-model="confirmPassword"
                                    :disabled="visibleCollapse" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'key']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                    </b-row>
                    <b-row v-if="error">
                        <b-col>
                            <b-alert variant="danger">
                                No se pudo agregar el paciente. Error de servidor
                            </b-alert>
                        </b-col>
                    </b-row>
                    <b-row class="mt-2 mb-2">
                        <b-col class="text-right">
                            <b-button
                            @click="activateCard"
                            :disabled="visibleCollapse"
                            variant="success">Activar tarjeta</b-button>
                        </b-col>
                    </b-row>
                </b-form>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import emeciPatient from '../components/emeciPatient.vue';
import api from '../../../api/patient-service';
import helper from '../helper/newPatientModel';
import { initLoader, saved, onlyLetter } from '../../../helper/util';

let loader = null;

export default {
    created() {
        this.getLastPatient();
    },
    components: {
        emeciPatient
    },
    data() {
        return {
            doctorId: this.$appConfig.doctor.doctorId,
            visibleCollapse: false,
            options: [
                { value: 'F', text: 'Mujer' },
                { value: 'M', text: 'Hombre' }
            ],
            emeci: null,
            error: false,
            //form
            name: '',
            lastName: '',
            mothersName: '',
            fathersName: '',
            phone: '',
            sex: 'F',
            email: '',
            birthDate: null,
            allergy: '',
            password: '',
            confirmPassword: '',
            req: {}
        }
    },
    computed: {
        txtLink() {
            return this.visibleCollapse ? 'Cerrar' : 'Agregar paciente existente';
        }
    },
    methods: {
        getLastPatient() {
            api.getLastEmeci(this.doctorId).then(response => {
                this.emeci = response.body;
            })
        },
        activateCard() {
            this.formValidation();

            if (!this.error) {
                loader = initLoader();
                api.post(this.doctorId, this.req).then(response => {
                    console.log(response.body);
                    loader.hide();
                    const patientId = response.body.PatientId;
                    console.log(patientId);
                    if (patientId != null) {
                        
                        saved(true, 'Paciente agregado', `/consults/${patientId}`);
                    } this.failure();
                })
                .catch(_error => {
                    this.failure();
                });
            }
        },
        failure() {
            loader.hide();
            this.error = true;
            saved(false, 'Hubó un error al guardar el paciente', null);
        },
        formValidation() {
            const form = new helper(
                this.name,
                this.lastName,
                this.mothersName,
                this.fathersName,
                this.phone,
                this.sex,
                this.email,
                this.$moment(this.birthDate).format('YYYY-MM-DD'),
                this.allergy,
                this.password,
                this.confirmPassword
            );

            form.validate();
            if (form.errors.length > 0) {
                this.error = true;

                //di clic al boton y no lleno nada del formulario
                if (form.errors.length === 10) {
                    this.$swal({
                        icon: 'warning',
                        title: 'Formulario incorrecto',
                        text: 'Todos los campos son necesarios'
                    });
                    return;
                }

                let list = '';
                form.errors.forEach(x => {
                    list += `<div class="list-group-item border-0 p-1">- ${x}</div>`;
                });

                this.$swal({
                    icon: 'warning',
                    title: 'Formulario incorrecto',
                    html: `
                        <div class="list-group">${list}</div>
                    `
                });
            } else {
                this.error = false;
                this.req = form.__$;
            }
            
            return;
        },
        format(evt) {
            onlyLetter(evt);
        }
    }
}
</script>