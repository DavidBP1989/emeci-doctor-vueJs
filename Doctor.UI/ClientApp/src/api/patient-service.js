import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
    get: {
        method: 'GET',
        url: `${API_URL}/patient/{doctorId}{?page,itemsPerPage,columnName,textToSearch,orderby}`
    },
    getById: {
        method: 'GET',
        url: `${API_URL}/patient/byId/{patientId}`
    },
    getLastEmeci: {
        method: 'GET',
        url: `${API_URL}/patient/{doctorId}/last`
    },
    post: {
        method: 'POST',
        url: `${API_URL}/patient/{doctorId}/`
    },
    newExistingPatient: {
        method: 'GET',
        url: `${API_URL}/patient/newpatient/{?Emeci,Coordinate,Value}`
    }
};

const api = Vue.resource(API_URL, {}, customActions);

export default {
    /**
     * Obtiene el listado de pacientes
     */
    get(doctorId, page, itemsPerPage, columnName, textToSearch, orderby) {
        return api.get({
            doctorId,
            page,
            itemsPerPage,
            columnName,
            textToSearch,
            orderby
        });
    },
    /**
     * Obtiene el paciente por id
     */
    getById(patientId) {
        return api.getById({
            patientId
        });
    },
    /**
     * Obtiene el siguiente # de emeci 
     * por agregar
     */
    getLastEmeci(doctorId) {
        return api.getLastEmeci({ doctorId });
    },
    /**
     * Guarda un nuevo paciente
     */
    post(doctorId, request) {
        return api.post({ doctorId }, request);
    },
    /**
     * Agregar paciente existente
     */
    newExistingPatient(doctorId, Emeci, Coordinate, Value) {
        return api.newExistingPatient({
            doctorId,
            Emeci,
            Coordinate,
            Value
        });
    }
};
