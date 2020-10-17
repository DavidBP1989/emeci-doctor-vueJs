<template>
    <b-card no-body :header="`Grupo de ${getTextType}s guardados`">
        <b-list-group flush>
            <b-list-group-item
            class="d-flex justify-content-between list-saved-data"
            button
            v-for="(x, index) in saved_Data"
            :key="index"
            :class="(index % 2) == 0 ? 'bg-gray-list-group-item' : ''"
            :disabled="x.disabled"
            @click="add(x.id)">
                <span class="text-left">
                    {{ x.name }}
                    <br>
                    <b-button variant="link" class="p-0" @click.stop="seeListOf(index)">{{ `Ver ${getTextType}` }}</b-button>
                </span>
                <span class="delete" @click.stop="deleteItem(x.id)">Eliminar</span>
            </b-list-group-item>
        </b-list-group>
    </b-card>
</template>

<script>
import { deleteDiagnostic, deleteTreatment } from '../../helper/call';
import EventBus from '../../../../../helper/event-bus';

export default {
    props: ['dataResponse', 'savedData', 'type'],
    data() {
        return {
            dataRes: this.dataResponse,
            saved_Data: this.savedData
        }
    },
    computed: {
        getTextType() {
            return this.type === 'diagnostic' ? 'diagnóstico' : 'tratamiento';
        }
    },
    methods: {
        add(id) {
            const index = this.saved_Data.findIndex(x => x.id === id);
            this.saved_Data[index].list.forEach(x => {
                this.dataRes.push({
                    id: this.getLastId(),
                    idSavedData: id,
                    text: x,
                    edit: false,
                    exclusive: false
                });
            });
            this.saved_Data[index].disabled = true;
            EventBus.$emit('changeToNewItems');
        },
        getLastId() {
            let count = 1;
            if (this.dataRes.length > 0) {
                count = this.dataRes[this.dataRes.length - 1].id + 1;
            }
            return count;
        },
        deleteItem(id) {
            this.$bvModal.msgBoxConfirm('¿Estas seguro que deseas eliminarlo?', {
                okTitle: 'Eliminar',
                cancelTitle: 'Cancelar',
                centered: true,
                id: 'modal-delete'
            })
            .then(value => {
                if (value) {
                    if (this.type === 'diagnostic')
                        deleteDiagnostic(id).then(response => this.confirmDelete(response, id));
                    else deleteTreatment(id).then(response => this.confirmDelete(response, id));
                }
            });
        },
        confirmDelete(status, id) {
            if (status === 200)
                this.saved_Data.splice(this.saved_Data.findIndex(x => x.id === id), 1);
            else {
                this.$bvModal.msgBoxOk('No se puedo eliminar', {
                    title: 'Error',
                    centered: true,
                    id: 'modal-delete-error'
                });
            }
        },
        seeListOf(index) {
            const item = this.saved_Data[index];

            const h = this.$createElement;
            let list = [];
            item.list.forEach(x => {
                list.push(
                    h('b-list-group-item', x)
                );
            });
            const message = h('b-list-group', list);

            this.$bvModal.msgBoxOk([message], {
                centered: true,
                title: item.name,
                okTitle: 'Cerrar',
                id: 'modal-list'
            });
        }
    }
}
</script>
<style scoped src="../../styles/saved.scss"></style>