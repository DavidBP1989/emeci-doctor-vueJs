<template>
    <b-container>
        <b-row>
            <b-col cols="12" class="text-left">
                <p>
                    Seleccione uno de los grupos para ver los estudios disponibles, 
                    haga click sobre el nombre del estudio para seleccionarlo.
                </p>
            </b-col>
            <b-col class="text-right mb-2">
                <b-button variant="outline-secondary">Imprimir estudios seleccionados</b-button>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <div class="tablist" role="tablist">
                    <b-card no-body v-for="(d, index) in dataResponse" :key="index">
                        <b-card-header header-tag="header" class="p-0" :class="d.style ? 'selected' : ''" role="tab">
                            <b-button
                            block
                            v-b-toggle="`collapse-${d.id}`"
                            class="text-left">
                                {{ d.name }}
                            </b-button>
                        </b-card-header>
                        <b-collapse :id="`collapse-${d.id}`" accordion="accordion" role="tabpanel">
                            <b-card-body class="p-0">
                                <b-list-group>
                                    <b-list-group-item v-for="(s, indexStudies) in d.studies" :key="indexStudies">
                                        <b-row class="align-items-center">
                                            <b-col cols="2">
                                                <fa-icon
                                                @click="checkValue(index, indexStudies, s.check)"
                                                :class="s.check ? 'check' : 'uncheck'"
                                                :icon="['fas', 'check']"
                                                size="lg"/>
                                            </b-col>
                                            <b-col md="8" class="text-left" :cols="s.edit ? 10 : 7">
                                                <b-form-group v-if="s.edit" description="Se editará al instante">
                                                    <b-form-textarea  v-model="s.name" />
                                                </b-form-group>
                                                <span v-else>{{ s.name }}</span>
                                            </b-col>
                                            <b-col>
                                                <span :class="s.edit ? 'float-right' : ''" class="pointer" @click="s.edit = !s.edit">
                                                    {{ s.edit ? 'Cerrar' : 'Editar' }}
                                                </span>
                                            </b-col>
                                        </b-row>
                                    </b-list-group-item>
                                </b-list-group>
                            </b-card-body>
                        </b-collapse>
                    </b-card>
                </div>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
import EventBus from '../../../../helper/event-bus';

export default {
    props: ['type', 'dataList'],
    data() {
        return {
            dataResponse: this.dataList
        }
    },
    methods: {
        checkValue(index, indexStudies, check) {
            this.dataResponse[index]
            .studies[indexStudies].check = !check;

            const any = this.dataResponse[index]
            .studies.some((x) => {
                return x.check;
            });

            this.dataResponse[index].style = any;
            EventBus.$emit('add-labcab', this.type);
        }
    }
}
</script>

<style scoped src="../styles/lab-cab.scss"></style>