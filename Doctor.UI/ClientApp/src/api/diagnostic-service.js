import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
    get: {
        method: 'GET',
        url: `${API_URL}/diagnostics/{doctorId}`
    },
    post: {
        method: 'POST',
        url: `${API_URL}/diagnostics/{doctorId}`
    },
    delete: {
        method: 'DELETE',
        url: `${API_URL}/diagnostics/{id}`
    }
};

const api = Vue.resource(API_URL, {}, customActions);

export default {
    /**
     * Obtiene diagnosticos guardados
     * por doctor
     */
    get(doctorId) {
        return api.get({
            doctorId
        });
    },
    /**
     * Guarda un nuevo diagnostico
     */
    post(doctorId, request) {
        return api.post({ doctorId }, request);
    },
    /**
     * Elimina diagnosticos
     */
    delete(id) {
        return api.delete({
            id
        });
    },
}