import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
    get: {
        method: 'GET',
        url: `${API_URL}/treatments/{doctorId}`
    },
    post: {
        method: 'POST',
        url: `${API_URL}/treatments/{doctorId}`
    },
    delete: {
        method: 'DELETE',
        url: `${API_URL}/treatments/{id}`
    }
};

const api = Vue.resource(API_URL, {}, customActions);

export default {
    /**
     * Obtiene tratamientos guardados
     * por doctor
     */
    get(doctorId) {
        return api.get({
            doctorId
        });
    },
    /**
     * Guarda un nuevo tratamiento
     */
    post(doctorId, request) {
        return api.post({ doctorId }, request);
    },
    /**
     * Elimina tratamiento
     */
    delete(id) {
        return api.delete({
            id
        });
    },
}