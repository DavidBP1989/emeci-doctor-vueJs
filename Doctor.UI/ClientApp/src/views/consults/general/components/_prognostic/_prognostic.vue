<template>
    <b-container>
        <b-row>
            <b-col>
                <b-form-group class="text-left" label="Bueno">
                    <b-form-checkbox-group v-model="goodSelected" :options="options" switches stacked />
                </b-form-group>
            </b-col>
            <b-col>
                <b-form-group class="text-left" label="Malo">
                    <b-form-checkbox-group v-model="badSelected" :options="options" switches stacked />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group class="text-left" label="Reservado a evolución">
                    <b-form-checkbox v-model="evolution" switch />
                </b-form-group>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
export default {
    props: ['dataList'],
    data() {
        return {
            dataResponse: this.dataList,
            evolution: false,
            goodSelected: [],
            badSelected: [],
            options: [
                { text: 'Para la vida', value: 0 },
                { text: 'Para la función', value: 1 }
            ]
        }
    },
    methods: {
        setPrognostics() {
            this.dataResponse.splice(0);
            if (this.evolution) this.dataResponse.push('Reservado a evolución')
            if (this.goodSelected.length > 0) {
                if (this.goodSelected.includes(0))
                    this.dataResponse.push('Bueno para la vida');
                if (this.goodSelected.includes(1))
                    this.dataResponse.push('Bueno para la función');
            }
            if (this.badSelected.length > 0) {
                if (this.badSelected.includes(0))
                    this.dataResponse.push('Malo para la vida');
                if (this.badSelected.includes(1))
                    this.dataResponse.push('Malo para la función');
            }
        }
    },
    watch: {
        goodSelected() {
            this.setPrognostics();
        },
        badSelected() {
            this.setPrognostics();
        },
        evolution() {
            this.setPrognostics();
        }
    }
}
</script>