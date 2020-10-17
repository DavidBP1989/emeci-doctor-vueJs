import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
    get: {
        method: 'GET',
        url: `${API_URL}/doctor/{doctorId}`
    },
    forgotPwd: {
        method: 'GET',
        url: `${API_URL}/wAuthority/forgotpwd/{emeci}`
    },
    changePwd: {
        method: 'PUT',
        url: `${API_URL}/doctor/{doctorId}/changePwd`
    },
    getStates: {
        method: 'GET',
        url: `${API_URL}/wAuthority/states`
    },
    getCities: {
        method: 'GET',
        url: `${API_URL}/wAuthority/cities/{stateId}`
    },
    register: {
        method: 'POST',
        url: `${API_URL}/wAuthority/doctorregister`
    },
    getRegister: {
        method: 'GET',
        url: `${API_URL}/doctor/{doctorId}/registerInfo`
    },
    updateRegisterInfo: {
        method: 'PUT',
        url: `${API_URL}/doctor/{doctorId}`
    },
};
const api = Vue.resource(API_URL, {}, customActions);

export default {
    /**
     * Obtiene la informacion basica
     * del doctor
     */
    get(doctorId) {
        return api.get({ doctorId });
    },
    /**
     * enviar correo para recuperar
     * la contraseña del doctor
     */
    forgotPwd(emeci) {
        return api.forgotPwd({ emeci })
    },
    /**
     * cambia la contraseña del
     * doctor
     */
    changePwd(doctorId, request) {
        return api.changePwd({ doctorId }, request);
    },
    /**
     * 
     */
    getStates() {
        return api.getStates();
    },
    /**
     * 
     */
    getCities(stateId) {
        return api.getCities({ stateId })
    },
    /**
     * registro medico
     */
    register(request) {
        return api.register({ }, request);
    },
    /**
     * obtiene todo la info del
     * doctor registrado
     */
    getRegister(doctorId) {
        return api.getRegister({ doctorId });
    },
    /**
     * actualizar datos del
     * medico
     */
    updateRegisterInfo(doctorId, request) {
        return api.updateRegisterInfo({ doctorId }, request);
    }
}