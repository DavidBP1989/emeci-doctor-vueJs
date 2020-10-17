import Vue from 'vue';
import store from './store';
import { BootstrapVue } from 'bootstrap-vue';
import './icons';
import './api/interceptor';
import { router } from './router/index';
import './helper/app.settings';
import moment from 'moment';
import VueMoment from 'vue-moment';
import loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import VueSweetalert2 from 'vue-sweetalert2';
import VCalendar from 'v-calendar';

import './scss/app.scss';
import 'sweetalert2/dist/sweetalert2.min.css';
import view from './view.vue';

moment.locale('es');

Vue.use(BootstrapVue);
Vue.use(VueMoment, { moment });
Vue.use(loading);
Vue.use(VueSweetalert2);
Vue.use(VCalendar);

new Vue({
    router,
    store,
    render: h => h(view)
}).$mount('#app');

Vue.config.devtools = true;