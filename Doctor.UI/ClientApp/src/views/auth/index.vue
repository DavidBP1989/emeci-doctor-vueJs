<template>
    <div class="authentication d-flex align-items-center pb-0 pt-0 min-vh-100 vld-parent">
        <b-container>
            <b-card class="login-card" no-body>
                <b-row no-gutters>
                    <b-col lg="5">
                        <img :src="bgLogin" class="login-card-img" />
                    </b-col>
                    <b-col>
                        <b-card>
                            <div class="mb-4">
                                <img :src="logo" />
                            </div>
                            <router-view />
                        </b-card>
                    </b-col>
                </b-row>
            </b-card>
        </b-container>
    </div>
</template>

<script>
import bgLogin from '../../assets/images/bglogin.jpg';
import logo from '../../assets/images/logo-emeci.png';

export default {
    beforeCreate() {
        document.body.className = 'auth_body';
    },
    data() {
        return {
            bgLogin: bgLogin,
            logo: logo
        }
    }  
}
</script>