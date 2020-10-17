<template>
    <b-button class="mb-2" variant="success" @click="show">Guardar consulta</b-button>
</template>

<script>
import modal from './_prognostic.vue';
import Vue from 'vue';
import EventBus from '../../../../../helper/event-bus';

export default {
    props: {
        dataList: {
            type: Array,
            required: true
        }
    },
    methods: {
        show() {
            const componentToLoad = Vue.extend(modal);
            const instance = new componentToLoad({
                propsData: {
                    dataList: this.dataList
                }
            });
            instance.$mount();
            
            this.$swal({
                title: 'Pronóstico',
                html: '<div></div>',
                showCloseButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Confirmar consulta',
                confirmButtonColor: '#28a745',
                reverseButtons: true,
                onBeforeOpen: () => {
                    this.$swal.getContent()
                    .querySelector('div')
                    .append(instance.$el);
                }
            }).then(result => {
                if (result.value) {
                    EventBus.$emit('save');
                }
            })
        }
    }
}
</script>