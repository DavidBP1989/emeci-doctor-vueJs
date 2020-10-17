import Vue from 'vue';

function modalInstance(title) {
    Vue.swal({
        title: title,
        html: '<div></div>',
        showCloseButton: true,
        confirmButtonText: 'Agregar a la consulta',
        confirmButtonColor: '#28a745',
        reverseButtons: true,
        onBeforeOpen: () => {
            Vue.swal
            .getContent()
            .querySelector('div')
            .append(instance.$el);
        }
    });
}


function printConsult(component) {
    let componentToLoad = Vue.extend(component);
    const instance = new componentToLoad();
    instance.$mount();

    return Vue.swal({
        title: 'Mover recuadro para ajustar margenes',
        width: '40%',
        html: '<div></div>',
        showCloseButton: true,
        showConfirmButton: true,
        confirmButtonText: 'Imprimir',
        onBeforeOpen: () => {
            Vue.swal
            .getContent()
            .querySelector('div')
            .append(instance.$el);
        }
    });
}

export { modalInstance, printConsult }