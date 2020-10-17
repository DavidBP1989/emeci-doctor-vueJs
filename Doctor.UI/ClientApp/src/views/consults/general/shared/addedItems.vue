<template>
    <b-row>
        <b-col>
            <fa-icon :icon="['fas', (visible ? 'caret-down' : 'caret-right')]" size="lg" />
            <b-button variant="link" v-b-toggle="`cll-${type}`">{{ title }}</b-button>

            <b-collapse :id="`cll-${type}`" v-model="visible">
                <b-list-group >
                    <b-list-group-item v-for="d in dataList" :key="d.id">
                        <span>{{ d.name || d.text }}</span>
                        <div v-if="type === 'laboratory' || type === 'cabinet'">
                            <span v-for="s in d.studies" :key="s.id">- {{ s.name }} <br /></span>
                        </div>
                    </b-list-group-item>
                </b-list-group>
            </b-collapse>
        </b-col>
    </b-row>
</template>

<script>
export default {
    props: ['title', 'dataList', 'type'],
    data() {
        return {
            visible: false
        }
    }
}
</script>
<style scoped src="../styles/addedItems.scss"></style>