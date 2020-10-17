import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
    get: {
        method: 'GET',
        url: `${API_URL}/consults/general/{consultId}`
    },
    post: {
        method: 'POST',
        url: `${API_URL}/consults/general/{doctorId}`
    },
    getConsultationDates: {
        method: 'GET',
        url: `${API_URL}/consults/general/dates//{pacientId}`
    }
};

const api = Vue.resource(API_URL, {}, customActions);

export default {
    /**
     * Obtiene consulta por id
     */
    get(consultId) {
        return api.get({
            consultId
        });
    },
    /**
     * guarda consulta
     */
    post(doctorId, request) {
        return api.post({ doctorId }, request);
    },
    /**
     * Obtiene un listado de fechas
     * para buscar consultas pasadas
     */
    getConsultationDates(pacientId) {
        return api.getConsultationDates({
            pacientId
        });
    }
}