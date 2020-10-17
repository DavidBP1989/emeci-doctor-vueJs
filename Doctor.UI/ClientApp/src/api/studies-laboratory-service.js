import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
    get: {
        method: 'GET',
        url: `${API_URL}/laboratory`
    }
};

const api = Vue.resource(API_URL, {}, customActions);

export default {
    /**
     * Obtiene los estudios de laboratorio
     */
    get() {
        return api.get();
    }
}