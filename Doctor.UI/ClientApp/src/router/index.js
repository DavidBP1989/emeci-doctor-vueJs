import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

import index from '../views/patients/index.vue';
import pacientList from '../views/patients/components/pacientList.vue';
import newPatient from '../views/patients/components/newPatient.vue';

import index_consults from '../views/consults/index.vue';
import generalConsult from '../views/consults/general/index.vue';
import obstetricConsult from '../views/consults/obstetric/index.vue';
import gynecologyConsult from '../views/consults/gynecology/index.vue';

import index_doctor from '../views/doctor/index.vue';
import register from '../views/doctor/components/register.vue';

import index_authentication from '../views/auth/index.vue';
import authentication from '../views/auth/components/auth.vue';
import forgotPwd from '../views/auth/components/forgotPwd.vue';


export const router = new Router({
    base: '/Doctor',
    mode: 'history',
    routes: [
        //PACIENTES
        {
            path: '/',
            component: index,
            children: [
                {
                    path: '',
                    component: pacientList,
                    name: 'Pacientes',
                    meta: {
                        showPatientInfo: false,
                        breadcrumb: []
                    }
                },
                {
                    path: 'newPatient',
                    component: newPatient,
                    name: 'Nuevo paciente',
                    meta: { 
                        breadcrumb: [
                            { text: 'Pacientes', to: '/' },
                            { text: 'Nuevo paciente', to: '#', active: true }
                        ]
                    }
                }
            ]
        },
        //CONSULTAS
        {
            path: '/consults/:id',
            component: index_consults,
            meta: {
                menu: 'consults'
            },
            children: [
                {
                    path: '',
                    component: generalConsult,
                    name: 'Consulta general',
                    meta: {
                        breadcrumb: [
                            { text: 'Pacientes', to: '/' },
                            { text: 'Consulta general', to: '#', active: true }
                        ],
                        typeMenu: 'consult'
                    }
                },
                {
                    path: 'obstetric',
                    component: obstetricConsult,
                    name: 'Consulta obstétrica',
                    meta: {
                        breadcrumb: [
                            { text: 'Pacientes', to: '/' },
                            { text: 'Consulta obstétrica', to: '#', active: true }
                        ],
                        typeMenu: 'consult'
                    }
                },
                {
                    path: 'gynecology',
                    component: gynecologyConsult,
                    name: 'Consulta ginecológica',
                    meta: {
                        breadcrumb: [
                            { text: 'Pacientes', to: '/' },
                            { text: 'Consulta ginecológica', to: '#', active: true }
                        ],
                        typeMenu: 'consult'
                    }
                }
            ]
        },
        //DOCTOR
        {
            path: '/register',
            component: index_doctor,
            children: [
                {
                    path: '',
                    component: register,
                    name: localStorage.getItem('appconfig') != null ? 'Mis datos médicos' : 'Registro médico',
                    meta: {
                        breadcrumb: [],
                        typeMenu: localStorage.getItem('appconfig') != null ? 'register': ''
                    }
                },
            ]
        },
        //LOGIN
        {
            path: '/auth',
            component: index_authentication,
            children: [
                {
                    path: '',
                    component: authentication
                },
                {
                    path: 'forgotPwd',
                    component: forgotPwd,
                }
            ]
        }
    ]
});

router.beforeEach((to, from, next) => {
    //redirect to login page if not logged in

    const publicPages = ['/auth', '/auth/forgotPwd', '/register'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('appconfig');

    if (authRequired && !loggedIn) {
        return next('/auth');
    } else if (loggedIn && to.path === '/auth') return next(from.path);

    next();
});