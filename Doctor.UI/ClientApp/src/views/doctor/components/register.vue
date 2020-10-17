<template>
    <b-row :class="loggedIn ? 'mb-2' : ''">
        <b-col v-if="!loggedIn" md="3" class="contact-us">
            <b-row>
                <b-col md="12" cols="3" class="img-content">
                    <img src="https://image.ibb.co/kUASdV/contant-image.png" />
                </b-col>
                <b-col>
                    <h3 class="mb-3">Contactanos</h3>
                    <h6>
                        Cualquier duda o aclaraci&oacute;n no dude en enviar correo a 
                        <a href="mailto:soporte@emeci.com">soporte@emeci.com</a>
                    </h6>
                    <div>
                        <fa-icon :icon="['fas', 'chevron-left']" size="sm" class="align-middle"/>
                        <b-button variant="link" class="pl-1" @click="$router.go(-1)">Volver a inicio de sesi&oacute;n</b-button>
                    </div>
                </b-col>
            </b-row>
        </b-col>
        <b-col>
            <b-card :class="loggedIn ? 'no-border' : ''" class="register">
                <b-form @submit="onSubmit" autocomplete="off">
                    <h5 class="mt-3 mb-4">Informaci&oacute;n del m&eacute;dico</h5>
                    <b-form-row>
                        <b-col md="6" lg="3">
                            <b-form-group label="Nombre(s)">
                                <b-input-group>
                                    <b-form-input v-model="formModel.name" />
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
                                <b-form-input v-model="formModel.lastName" />
                            </b-form-group>
                        </b-col>
                        <b-col md="6" lg="3">
                            <b-form-group label="RFC">
                                <b-form-input v-model="formModel.rfc" />
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="CURP">
                                <b-form-input v-model="formModel.curp" />
                            </b-form-group>
                        </b-col>
                    </b-form-row>
                    <b-form-row>
                        <b-col md="6" lg="4">
                            <b-form-group label="No. Cédula profesional SEP">
                                <b-form-input v-model="formModel.sepCertificate" />
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="No. S.S.A.">
                                <b-form-input v-model="formModel.ssa" />
                            </b-form-group>
                        </b-col>
                        <b-col md="12" lg="4">
                            <b-form-group label="CMCP">
                                <b-form-input v-model="formModel.cmcp" />
                            </b-form-group>
                        </b-col>
                    </b-form-row>
                    <b-form-row>
                        <b-col md="6" lg="4">
                            <b-form-group label="Hospital de residencia">
                                <b-form-input v-model="formModel.hospital" />
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Universidad que avala">
                                <b-form-input v-model="formModel.university" />
                            </b-form-group>
                        </b-col>
                        <b-col md="12" lg="4">
                            <b-form-group label="Cédula de especialidad SEP">
                                <b-form-input v-model="formModel.certificate" />
                            </b-form-group>
                        </b-col>
                    </b-form-row>
                    <b-form-row>
                        <b-col md="6">
                            <b-form-group label="Nombre del colegio estatal">
                                <b-form-input v-model="formModel.school" />
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Nombre de agrupación estatal">
                                <b-form-input v-model="formModel.grouping" />
                            </b-form-group>
                        </b-col>
                    </b-form-row>
                    <h5 class="mt-3 mb-4">Domicilio particular</h5>
                    <b-form-row>
                        <b-col md="6" lg="4">
                            <b-form-group label="Estado">
                                <vue-bootstrap-typeahead
                                v-model="stateSelected"
                                :data="autocompleteStates"
                                @hit="hitState"
                                ref="stateTypeahead"
                                placeholder="Escoge un estado">
                                    <template slot="prepend">
                                        <b-input-group-text>
                                            <fa-icon :icon="['fa', 'search']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </vue-bootstrap-typeahead>
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Ciudad">
                                <vue-bootstrap-typeahead
                                v-model="citySelected"
                                :data="autocompleteCities"
                                ref="cityTypeahead"
                                placeholder="Escoge una ciudad">
                                    <template slot="prepend">
                                        <b-input-group-text>
                                            <fa-icon :icon="['fa', 'search']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </vue-bootstrap-typeahead>
                            </b-form-group>
                        </b-col>
                        <b-col md="12" lg="4">
                            <b-form-group label="Colonia">
                                <b-form-input v-model="formModel.colony" />
                            </b-form-group>
                        </b-col>
                    </b-form-row>
                    <b-form-row>
                        <b-col md="8" lg="6">
                            <b-form-group label="Calles">
                                <b-form-input v-model="formModel.address" />
                            </b-form-group>
                        </b-col>
                        <b-col md="4" lg="3">
                            <b-form-group label="Código postal">
                                <b-form-input v-model="formModel.cp" />
                            </b-form-group>
                        </b-col>
                        <b-col md="6" lg="3">
                            <b-form-group label="Teléfono de consultorio">
                                <b-input-group>
                                    <b-form-input v-model="formModel.officePhone" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'phone']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col md="6" lg="12">
                            <b-form-group label="Domicilio del consultorio">
                                <b-input-group>
                                    <b-form-input v-model="formModel.officeAddress" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'address-book']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                    </b-form-row>
                    <h5 class="mt-3 mb-4">Contacto</h5>
                    <b-form-row>
                        <b-col md="6" lg="4">
                            <b-form-group label="Teléfono">
                                <b-input-group>
                                    <b-form-input v-model="formModel.phone" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'phone']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col>
                            <b-form-group label="Celular">
                                <b-input-group>
                                    <b-form-input v-model="formModel.cellphone" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'phone']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col md="12" lg="4">
                            <b-form-group label="Correo(s) electrónico(s)" description="Separar por coma más de una dirección de correo electrónico">
                                <b-input-group>
                                    <b-form-input v-model="formModel.email" />
                                    <template v-slot:prepend>
                                        <b-input-group-text>
                                            <fa-icon :icon="['fas', 'at']" size="lg" />
                                        </b-input-group-text>
                                    </template>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                    </b-form-row>
                    <b-form-group class="text-right mb-3" v-if="!loggedIn">
                        <b-form-checkbox v-model="checkPolitics">
                            Acepta las <a target="_blank" href="https://www.emeci.com/ConsultaMedico/uso.pdf">politicas de uso de EMECI</a> y haber leido
                            nuestras <a target="_blank" href="https://www.emeci.com/wsite/home/NoticeOfPrivacy">politicas de privacidad.</a>
                        </b-form-checkbox>
                    </b-form-group>

                    <b-alert :show="!loggedIn && !checkPolitics && once" variant="danger">Debe aceptar las politicas de uso y privacidad</b-alert>

                    <b-button type="submit" class="float-right" variant="success">{{ textButton }}</b-button>
                </b-form>
            </b-card>
        </b-col>
    </b-row>
</template>

<script>
import api from '../../../api/doctor-service';
import registerModel from '../helper/registerModel';
import { initLoader, saved } from '../../../helper/util';
import VueBootstrapTypeahead from 'vue-bootstrap-typeahead';
let loader = null;

export default {
    created() {
        this.loggedIn = localStorage.getItem('appconfig') != null;
        this.getStates();
        if (this.loggedIn) {
            this.getDoctorInformation();
        }
    },
    components: {
        VueBootstrapTypeahead
    },
    data() {
        return {
            loggedIn: false,
            states: [],
            once: false,
            autocompleteStates: [],
            stateSelected: '',
            cities: [],
            autocompleteCities: [],
            citySelected: '',
            formModel: {
                name: '',
                lastName: '',
                rfc: '',
                curp: '',
                sepCertificate: '',
                ssa: '',
                cmcp: '',
                hospital: '',
                university: '',
                certificate: '',
                school: '',
                grouping: '',
                address: '',
                colony: '',
                cp: '',
                state: null,
                city: '',
                officePhone: '',
                officeAddress: '',
                phone: '',
                cellphone: '',
                email: ''
            },
            checkPolitics: false,
            error: false,
            req: { }
        }
    },
    computed: {
        textButton() {
            return this.loggedIn ? 'Actualizar información' : 'Finalizar registro';
        }
    },
    methods: {
        getDoctorInformation() {
            loader = initLoader();
            api.getRegister(this.$appConfig.doctor.doctorId).then(response => {
                if (response.body) {
                    this.formModel.name = response.body.Name;
                    this.formModel.lastName = response.body.LastName;
                    this.formModel.rfc = response.body.RFC;
                    this.formModel.curp = response.body.CURP;
                    this.formModel.sepCertificate = response.body.NoSEP_ProfessionalCertificate;
                    this.formModel.ssa = response.body.NoSSA;
                    this.formModel.cmcp = response.body.NoCertification_CMCP;
                    this.formModel.hospital = response.body.ProfessionalResidenceHospital;
                    this.formModel.university = response.body.UniversitySpecialty;
                    this.formModel.certificate = response.body.SpecialtyCertificate;
                    this.formModel.school = response.body.NameStateSchool;
                    this.formModel.grouping = response.body.NameStateGrouping;
                    this.formModel.address = response.body.Address;
                    this.formModel.colony = response.body.Colony;
                    this.formModel.cp = response.body.PostalCode;
                    this.formModel.officePhone = response.body.OfficePhone;
                    this.formModel.officeAddress = response.body.OfficeAddress;
                    this.formModel.phone = response.body.Phone;
                    this.formModel.cellphone = response.body.CellPhone;
                    this.formModel.email = response.body.Email;

                    this.stateSelected = this.states.find(x => x.Id === response.body.State).Name;
                    this.$refs.stateTypeahead.inputValue = this.stateSelected;
                    
                    const stateId = this.states.find(x => x.Name === this.stateSelected).Id;
                    this.getCities(stateId, response.body.City);
                    loader.hide();
                }
            });
        },
        getStates() {
            api.getStates().then(response => {
                if (response.body) {
                    this.states = response.body;
                    let array = [];
                    response.body.forEach(element => {
                        array.push(element.Name);
                    });
                    this.autocompleteStates = array;
                }
            });
        },
        hitState() {
            const value = this.states.find(x => x.Name === this.stateSelected);
            if (value) {
                this.getCities(value.Id);
            }
        },
        getCities(stateId, citySelected) {
            api.getCities(stateId).then(response => {
                if (response.body) {
                    this.cities = response.body;
                    let array = [];
                    response.body.forEach(element => {
                        array.push(element.Name);
                    });
                    this.autocompleteCities = array;

                    if (this.loggedIn) {
                        this.citySelected = this.cities.find(x => x.Id == citySelected).Name;
                        this.$refs.cityTypeahead.inputValue = this.citySelected;
                    }
                }
            });
        },
        onSubmit(evt) {
            if (!this.once) this.once = true;
            evt.preventDefault();
            this.validation();
            this.loggedIn ? this.update() : this.register();
        },
        register() {
            const textError = 'Hubó un error al guardar el registro médico';

            if (!this.error && this.checkPolitics) {

                loader = initLoader();
                api.register(this.req).then(response => {
                    loader.hide();
                    if (response.status == 200) {
                        saved(true, 'Registro médico guardado', null);
                        window.location.href = 'https://emeci.com/';
                    } else this.failure(textError);
                })
                .catch(_error => {
                    this.failure(textError);
                });
            }
        },
        update() {
            const textError = 'Hubó un error al atualizar los datos del médico';

            if (!this.error) {

                loader = initLoader();
                api.updateRegisterInfo(this.$appConfig.doctor.doctorId, this.req).then(response => {
                    loader.hide();
                    if (response.status === 200) {
                        saved(true, 'La información ha sido actualizada', null);
                    } else this.failure(textError);
                })
                .catch(_error => {
                    this.failure(textError);
                });
            }
        },
        validation() {
            const model = new registerModel(this.formModel, this.states, this.stateSelected, this.cities, this.citySelected);

            model.validate();
            const errorCheck = (!this.loggedIn && !this.checkPolitics);
            if (model.errors.length > 0 || errorCheck) {
                this.error = true;

                if (model.errors.length > 0) {
                    let list = '';
                    model.errors.forEach(x => {
                        list += `<div class="list-group-item border-0 p-1">- ${x}</div>`;
                    });

                    this.$swal({
                        icon: 'warning',
                        title: 'Formulario incorrecto',
                        html: `
                            <div class="list-group">${list}</div>
                        `
                    });
                }

                if (errorCheck) {

                }
                
            } else {
                this.error = false;
                this.req = model.__$;
            }
        },
        failure(message) {
            loader.hide();
            this.error = true;
            saved(false, message, null);
        }
    }
}
</script>

<style scoped src="../styles/custom.scss"></style>