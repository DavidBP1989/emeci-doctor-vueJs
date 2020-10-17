import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        infoDoctor: {
            name: null,
            emeci: null,
        }
    },
    mutations: {
        updateInfoDoctor(state, info) {
            state.infoDoctor = info;
        }
    }
});