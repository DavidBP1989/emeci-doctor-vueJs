<template>
    <b-navbar toggleable="md" class="p-0 bg-gray">
            <b-container class="p-0">
                <b-navbar-brand class="justify-content-between mr-0">
                    <b-navbar-toggle class="border-0" target="headernavbar" v-if="menu !== ''">
                        <template v-slot:default="{ expanded }">
                            <fa-icon :icon="['fas', (expanded ? 'times' : 'align-justify')]" size="lg"/>
                        </template>
                    </b-navbar-toggle>
                    <div v-else></div> <!--empty-->
                    <div>
                        <b-img :src="logo" />
                    </div>
                    <div></div> <!--empty-->
                </b-navbar-brand>

                <b-collapse is-nav id="headernavbar">
                    <b-navbar-nav v-if="menu === 'consult'" class="ml-auto pl-3">
                        <b-nav-item @click="goToVaccinations" class="text-md-center ml-3">
                            <fa-icon :icon="['fas', 'syringe']" class="mb-md-2" size="2x"/>
                            <br class="d-none d-md-block">
                            <span class="ml-3 ml-md-0">Vacunas</span>
                        </b-nav-item>
                        <b-nav-item @click="goToemeciFile" class="text-md-center ml-3">
                            <fa-icon :icon="['fas', 'archive']" size="2x"/>
                            <br class="d-none d-md-block">
                            <span class="ml-3 ml-md-0">Expediente <br class="d-none d-md-block"> EMECI</span>
                        </b-nav-item>
                        <b-nav-item @click="goToPatients" class="text-md-center ml-3">
                            <fa-icon :icon="['fas', 'folder-open']" class="brown" size="2x"/>
                            <br class="d-none d-md-block">
                            <span class="ml-3 ml-md-0 brown">Cerrar <br class="d-none d-md-block"> expediente</span>
                        </b-nav-item>
                    </b-navbar-nav>

                    <b-navbar-nav v-if="menu === 'register'" class="ml-auto pl-3">
                        <b-nav-item @click="goToPatients" class="text-md-center ml-3">
                            <fa-icon :icon="['fas', 'user']" class="mb-md-2" size="2x"/>
                            <br class="d-none d-md-block">
                            <span class="ml-3 ml-md-0">Pacientes</span>
                        </b-nav-item>
                    </b-navbar-nav>
                </b-collapse>
            </b-container>
        </b-navbar>
</template>

<script>
import logo from '../../assets/images/logo-emeci.png';
import { urlFileEmeci } from '../../helper/util';

export default {
    data() {
        return {
            logo: logo,
            consultMenu: false,
            menu: this.$route.meta.typeMenu || '',
            emeciPatient: this.$route.params.id
        }
    },
    methods: {
        goToVaccinations() {
            window.open(this.getUrl('V'));
        },
        goToemeciFile() {
            window.open(this.getUrl('E'));
        },
        getUrl(type) {
            return `${urlFileEmeci}?opt=${type}&emeci=${this.$appConfig.patient.emeci}&posicion=${this.$appConfig.patient.coordinate}&dato=${this.$appConfig.patient.coordinateValue}`;
        },
        goToPatients() {
            this.$router.push('/');
        }
    }
}
</script>