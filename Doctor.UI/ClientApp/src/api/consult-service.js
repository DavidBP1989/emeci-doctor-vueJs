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
    get(consultId) {
        return api.get({
            consultId
        });
    },
    post(doctorId, request) {
        return api.post({ doctorId }, request);
    },
    getConsultationDates(pacientId) {
        return api.getConsultationDates({
            pacientId
        });
    }
}