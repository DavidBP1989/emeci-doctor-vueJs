<template>
    <b-button @click="show" class="mb-2" variant="outline-secondary">Estudios de laboratorio</b-button>
</template>

<script>
import modal from '../shared/modal-LabCab.vue';
import Vue from 'vue';

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
                    type: 'laboratory',
                    dataList: this.dataList
                }
            });
            instance.$mount();
            
            this.$swal({
                title: 'Estudios de laboratorio',
                html: '<div></div>',
                showCloseButton: true,
                showConfirmButton: false,
                confirmButtonColor: '#28a745',
                reverseButtons: true,
                onBeforeOpen: () => {
                    this.$swal.getContent()
                    .querySelector('div')
                    .append(instance.$el);
                }
            });
        }
    }
}
</script>