<template>
    <b-button @click="show" class="mb-2" variant="outline-secondary">Tratamientos</b-button>
</template>

<script>
import modal from '../shared/modal-diag-treat/modal.vue';
import Vue from 'vue';

export default {
    props: {
        dataList: {
            type: Array,
            required: true
        },
        savedDataList: {
            type: Array,
            required: true
        }
    },
    methods: {
        show() {
            const componentToLoad = Vue.extend(modal);
            const instance = new componentToLoad({
                propsData: {
                    type: 'treatments',
                    dataList: this.dataList,
                    savedDataList: this.savedDataList
                }
            });
            instance.$mount();

            this.$swal({
                title: 'Tratamientos',
                html: '<div></div>',
                showCloseButton: true,
                showConfirmButton: false,
                confirmButtonColor: '#28a745',
                reverseButtons: true,
                onBeforeOpen: () => {
                    this.$swal
                    .getContent()
                    .querySelector('div')
                    .append(instance.$el);
                }
            });
        }
    }
}
</script>