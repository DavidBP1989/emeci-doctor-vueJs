import Vue from 'vue';
import { router } from '../router';
import $ from 'jquery';

const urlFileEmeci = 'https://www.emeci.com/PacienteExpediente/AccesEmeci.aspx';

function userFormat(evt, onceDash = false) {
    const value = $(evt.path[0]).val();
    let charCode = (evt.which) ? evt.which : evt.keyCode;

    if (value.length === 5 && !onceDash) {

    }
    // 0 - 9 or dash
    if (onceDash && charCode === 45) // solo se puede agregar una vez el guion
        return evt.preventDefault();
    if ((charCode > 47 && charCode < 58) || charCode == 45)
        return true;
    else evt.preventDefault();
}

function onlyLetter(evt) {
    const regex = new RegExp('^[a-zA-ZÀ-ÖØ-öø-ÿ ]+$');
    const key = String.fromCharCode(!evt.charCode ? evt.which : evt.charCode);
    if (!regex.test(key)) {
        evt.preventDefault();
    } else return true;
}

function onlyNumber(evt, point = false) {
    let charCode = (evt.which) ? evt.which : evt.keyCode;
    if (point && charCode === 46)
        return true;
    if (charCode > 47 && charCode < 58)
        return true;
    else evt.preventDefault();
}

function validEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function initLoader() {
    return Vue.$loading.show({
        loader: 'spinner',
        height: 155,
        width: 135,
        color: '#D22874',
        backgroundColor: '#E8E8E8'
    });
}

/** muestra un modal
 * de confirmacion cuando se
 * guarda cualquier dato a la bd
 */
function saved(isConfirmed = true, title, route) {
    Vue.swal({
        position: 'top-end',
        icon: isConfirmed ? 'success' : 'error',
        title: title,
        showConfirmButton: false,
        timer: 1500,
        allowEscapeKey: false,
        allowOutsideClick: false
    }).then(() => {
        if (route != null && isConfirmed) router.push(route);
    });
}



function emeciNumber(val) {
    if (/^[0-9]{5}$/.test(val)) {
        val += '-';
    }
    else if (/^[0-9]{5}-[0-9]{4}$/.test(val)) {
        val += '-';
    }
    return val;
}

export {
    userFormat,
    onlyNumber,
    onlyLetter,
    validEmail,
    initLoader,
    saved,
    emeciNumber,
    urlFileEmeci
};