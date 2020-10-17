<template>
    <b-container>
        <b-row>
            <b-col class="text-right">
                <diagnostic-link v-if="type === 'diagnostic'" />
                <treatment-link v-else />
            </b-col>
        </b-row>
        <hr class="mt-1">
        <b-row class="align-items-end mb-3">
            <b-col class="text-left" sm="12" v-if="!showSavedData">
                <b-form autocomplete="off" @submit="add">
                    <b-form-group class="mb-0" :label="`Nuevo ${getTextType}`">
                        <b-input-group>
                            <b-form-input v-model="input" :placeholder="`Escribir ${getTextType}`"/>
                            <b-input-group-append>
                                <b-button type="submit" variant="info">Agregar</b-button>
                            </b-input-group-append>
                        </b-input-group>
                    </b-form-group>
                </b-form>
            </b-col>
            <b-col class="text-left mt-2">
                <fa-icon :icon="['fas', showSavedData ? 'times' : 'caret-right']" size="lg" />
                <b-button @click="showSavedData = !showSavedData" variant="link" class="pl-0">
                    {{ showSavedData ? 'Cerrar' : `Ver ${getTextType}s guardados`}}
                </b-button>
            </b-col>
        </b-row>
        <new-items v-if="!showSavedData" :type="type" :dataResponse="dataResponse" :savedData="savedData" />
        <saved
        v-else
        :dataResponse="dataResponse"
        :savedData="savedData"
        :type="type" />
    </b-container>
</template>

<script>
let count = 1;
import diagnosticLink from './diagnostic-link.vue';
import treatmentLink from './treatment-link.vue';
import newItems from './newItems.vue';
import saved from './saved.vue';
import EventBus from '../../../../../helper/event-bus';

export default {
    created() {
        EventBus.$on('changeToNewItems', () => {
            this.showSavedData = !this.showSavedData;
        });
    },
    props: ['type', 'dataList', 'savedDataList'],
    components: {
        diagnosticLink,
        treatmentLink,
        newItems,
        saved
    },
    data() {
        return {
            input: '',
            showSavedData: false,
            dataResponse: this.dataList,
            savedData: this.savedDataList
        }
    },
    computed: {
        getTextType() {
            return this.type === 'diagnostic' ? 'diagnóstico' : 'tratamiento';
        }
    },
    methods: {
        add(evt) {
            evt.preventDefault();
            if (this.input !== '') {
                this.dataResponse.push({
                    id: this.getLastId(),
                    idSavedData: null,
                    text: this.input.trim(),
                    edit: false,
                    exclusive: false
                });
                this.input = '';
                this.showSavedData = false;
            }
        },
        getLastId() {
            if (this.dataResponse.length > 0) {
                count = this.dataResponse[this.dataResponse.length - 1].id + 1;
            }
            return count;
        },
    }
}
</script>
<style scoped src="../../styles/modal.scss"></style>