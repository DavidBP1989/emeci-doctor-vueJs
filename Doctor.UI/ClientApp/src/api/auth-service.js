import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);
Vue.http.options.emulateJSON = true;

const customActions = {
    getToken: {
        method: 'POST',
        url: `${API_URL_AUTHENTICATION}/authentication`
    }
};

const api = Vue.resource(API_URL_AUTHENTICATION, {}, customActions);

export default {
    /**
     * obtiene el token para
     * iniciar sesion
     */
    getToken(request) {
        request.grant_type = 'password';
        return api.getToken({ }, request);
    }
}