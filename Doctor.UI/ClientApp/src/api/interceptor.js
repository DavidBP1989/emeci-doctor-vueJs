import Vue from 'vue';
import { router } from '../router/index';

Vue.http.interceptors.push((request) => {

    const url = request.url;
    const isAuthUrl = url.includes('/authentication');

    if (!isAuthUrl) {
        if (Vue.appConfig.token !== null)
            request.headers.set('Authorization', `Bearer ${Vue.appConfig.token}`);
    }

    return (response) => {
        if (response.status === 200) {
            if (isAuthUrl)
            {                
                Vue.appConfig.token = response.body.access_token;
                Vue.appConfig.doctor.doctorId = response.headers.map['did'][0];
                Vue.saveConfig();
            }
        }

        if (response.status === 401) {
            localStorage.removeItem('appconfig');
            /**
             * cierra cualquier ventana que siga abierta
             * si la sesion ya termino
             */
            Vue.swal.close();
            router.push('/auth');
        }
    }
})