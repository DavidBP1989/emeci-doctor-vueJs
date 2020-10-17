/**
 * encargado de obtener cualquier solicitud
 * que no requiera datos del doctor o paciente,
 * siempre y cuando siga autenticado
 */
import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
    getStudiesLaboratory: {
        method: 'GET',
        url: `${API_URL}/base/studiesLaboratory`
    },
    getStudiesCabinet: {
        method: 'GET',
        url: `${API_URL}/base/studiesCabinet`
    }
};

const api = Vue.resource(API_URL, {}, customActions);

export default {
    getStudiesLaboratory() {
        return api.getStudiesLaboratory();
    },
    getStudiesCabinet() {
        return api.getStudiesCabinet();
    }
}