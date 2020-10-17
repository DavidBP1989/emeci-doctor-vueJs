<template>
    <div id="draggable-parent" class="draggable-parent">
        <b-container>
            <b-row>
                <b-col md="3" lg="6">
                    <b-card id="preview" no-body>
                        <b-row>
                            <b-col>
                                <h6>Javier Fernando Garcidueñas Campo</h6>
                                <div>
                                    <b>Nombre del paciente: </b>Ivonne Alejandra Castro Saldaña
                                    <br>
                                    <b>Fecha de nacimiento: </b>18 junio 2018
                                    <br>
                                    <b>Fecha de consulta: </b>11 junio 2020
                                </div>
                            </b-col>
                        </b-row>
                    </b-card>
                </b-col>
            </b-row>
        </b-container>
    </div>
    <!--<b-container class="p-3 text-left" id="preview">
        <b-row>
            <b-col sm="6">
                <b-container>
                    <b-row>
                        <b-col>
                            <h6>Javier Fernando Garcidueñas Campo</h6>
                            <div>
                                <b>Nombre del paciente: </b>Ivonne Alejandra Castro Saldaña
                                <br>
                                <b>Fecha de nacimiento: </b>18 junio 2018
                                <br>
                                <b>Fecha de consulta: </b>11 junio 2020
                            </div>
                        </b-col>
                    </b-row>
                </b-container>
                
            </b-col>
        </b-row>
        <b-row class="mt-3">
            <b-col cols="3" md="4" lg="2">
                <b-form-group label="Peso">
                    <span>{{ consult.Weight }} kg</span>
                </b-form-group>
            </b-col>
            <b-col cols="3" md="4" lg="2">
                <b-form-group label="Talla">
                    <span>{{ consult.Size }} m</span>
                </b-form-group>
            </b-col>
            <b-col cols="6" md="4" lg="3">
                <b-form-group label="Indice de masa corporal">
                    <span>{{ consult.Mass }} kg/m2</span>
                </b-form-group>
            </b-col>
            <b-col cols="4" md="4" lg="2">
                <b-form-group label="Temperatura">
                    <span>{{ consult.Temperature }} c</span>
                </b-form-group>
            </b-col>
            <b-col>
                <b-form-group label="Presión arterial">
                    <span>{{ consult.BloodPressure_A }}/{{ consult.BloodPressure_B }} mm Hg</span>
                </b-form-group>
            </b-col>
        </b-row>
    </b-container>-->
</template>

<script>
import $ from 'jquery';
import 'jquery-ui';

export default {
    mounted() {
        this.$nextTick(() => {
            $('#preview').draggable({
                containment: '#draggable-parent'
            });
        });
        //window.print();
    },
    data() {
        return {
            consult: {
                Weight: '',
                BloodPressure_A: '',
                BloodPressure_B: '',
                Temperature: '',
                Mass: '',
                Size: ''
            }
        }
    }
}
</script>

<style lang="scss">
/*#preview {
    height: 70vh;
    border: 1px solid;
    font-size: 14px;
}*/
.draggable-parent {
    height: 70vh;
    overflow: hidden;
    border: 1px solid red;
}
</style>