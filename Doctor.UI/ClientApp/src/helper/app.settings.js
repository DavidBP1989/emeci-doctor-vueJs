import Vue from 'vue';

let appConfig = {
    token: null,
    doctor: {
        doctorId: 0,
        emeci: 0,
        name: null
    },
    patient: {
        emeci: null,
        coordinate: null,
        coordinateValue: null
    }
};

const ls = JSON.parse(localStorage.getItem('appconfig'));
if (ls !== null) appConfig = ls;

function saveConfig() {
    localStorage.setItem('appconfig', JSON.stringify(Vue.appConfig));
}

const plugin = {
    install($vue) {
        $vue.prototype.$appConfig = appConfig;
        $vue.appConfig = appConfig;
        //
        $vue.prototype.$saveConfig = saveConfig;
        $vue.saveConfig = saveConfig;
    }
}
Vue.use(plugin);
