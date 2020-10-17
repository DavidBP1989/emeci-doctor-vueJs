<template>
    <div class="_modalnewitem">
        <b-card no-body :header="`${type === 'diagnostic' ? 'Diagnóstico' : 'Tratamiento'} del Paciente`">
            <b-list-group flush>
                <b-list-group-item v-for="d in dataRes" :key="d.id">
                    <b-row class="align-items-center">
                        <b-col col md="9" class="text-left">
                            <div v-if="d.edit">
                                <b-form-group class="mb-0" description="Se editará al instante">
                                    <b-form-textarea v-model="d.text" />
                                </b-form-group>
                            </div>
                            <span v-else>{{ d.text }}</span>
                        </b-col>
                        <b-col class="pl-0 pr-2 text-right">
                            <b-form-checkbox v-model="d.exclusive" v-if="type === 'diagnostic'" size="sm">
                                Exclusivo
                            </b-form-checkbox>
                            <b-link class="mr-1" @click="d.edit = !d.edit">{{ d.edit ? 'Cerrar' : 'Editar' }}</b-link>
                            <b-link class="delete" v-if="!d.edit" @click="deleteItem(d.id, d.idSavedData)">Eliminar</b-link>
                        </b-col>
                    </b-row>
                </b-list-group-item>
                <b-list-group-item v-if="dataRes.length === 0" class="font-weight-bold">{{ `Ningún ${getTextType} agregado` }}</b-list-group-item>
            </b-list-group>
        </b-card>
        <b-row class="mt-1 mb-1" v-if="dataRes.length > 0">
            <b-col class="text-right">
                <b-button variant="secondary" @click="deleteItem(-1)">Eliminar todo</b-button>
                <b-button variant="secondary" v-if="dataRes.length > 0" v-b-modal.saveas>Guardar como</b-button>
            </b-col>
        </b-row>

        <b-modal id="saveas" ok-title="Guardar" @ok="handleOk" :title="`Guardar ${getTextType}:`" centered>
            <form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group label="Nombre del grupo" invalid-feedback="El campo es requerido" :state="nameState">
                    <b-form-input required v-model="groupName" :state="nameState"></b-form-input>
                </b-form-group>
            </form>
        </b-modal>
    </div>
</template>

<script>
import { saveDiagnostic, saveTreatment } from '../../helper/call';

export default {
    props: ['type', 'dataResponse', 'savedData'],
    data() {
        return {
            doctorId: this.$appConfig.doctor.doctorId,
            dataRes: this.dataResponse,
            saved_Data: this.savedData,
            groupName: '',
            nameState: null
        }
    },
    computed: {
        getTextType() {
            return this.type === 'diagnostic' ? 'diagnóstico' : 'tratamiento';
        }
    },
    methods: {
        deleteItem(id, idSavedData) {
            if (id === -1) {
                this.deleteAll();
                return;
            }
            this.$delete(this.dataRes, this.dataRes.findIndex(x => x.id == id));
            if (idSavedData !== null) {
                this.saved_Data[this.saved_Data.findIndex(x => x.id == idSavedData)].disabled = false;
            }
        },
        deleteAll() {
            this.dataRes.splice(0);
            this.saved_Data.forEach(x => x.disabled = false);
        },
        checkFormValidity() {
            const valid = this.$refs.form.checkValidity();
            this.nameState = valid;
            return valid;
        },
        handleOk(bvModalEvt) {
            bvModalEvt.preventDefault();
            this.handleSubmit();
        },
        handleSubmit() {
            if (!this.checkFormValidity()) {
                return;
            }
            this.saveas();
            this.$nextTick(() => {
                this.$bvModal.hide('saveas');
            })
        },
        saveas() {
            let list = this.dataRes.map((x) => {
                return x.text;
            });

            const req = {
                GroupName: this.groupName,
                List: list
            };

            if (this.type === 'diagnostic')
                saveDiagnostic(this.doctorId, req).then(response => this.pushInSavedData(response, list));
            else saveTreatment(this.doctorId, req).then(response => this.pushInSavedData(response, list));
        },
        pushInSavedData(body, list) {
            if (body.Id !== null && body.Id !== '')
                this.saved_Data.unshift({
                    id: body.Id,
                    name: this.groupName,
                    disabled: true,
                    list: list
                });
            else {
                this.$bvModal.msgBoxOk('No se puedo agregar', {
                    title: 'Error',
                    centered: true,
                    id: 'modal-newGroup'
                });
            }
        }
    }
}
</script>
<style scoped src="../../styles/newItem.scss"></style>