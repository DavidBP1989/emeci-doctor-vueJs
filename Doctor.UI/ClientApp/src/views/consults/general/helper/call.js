import api_diagnostic from '../../../../api/diagnostic-service';
import api_treatments from '../../../../api/treatments-service';
import api_laboratory from '../../../../api/studies-laboratory-service';
import api_cabinet from '../../../../api/studies-cabinet-service';


//DIAGNOSTICOS
function getDiagnostics(doctorId) {
    return api_diagnostic.get(doctorId)
    .then(response => response.body);
}

function saveDiagnostic(doctorId, request) {
    return api_diagnostic.post(doctorId, request)
    .then(response => response.body);
}

function deleteDiagnostic(diagnosticId) {
    return api_diagnostic.delete(diagnosticId)
    .then(response => response.status);
}

//TRATAMIENTOS
function getTreatments(doctorId) {
    return api_treatments.get(doctorId)
    .then(response => response.body);
}

function saveTreatment(doctorId, request) {
    return api_treatments.post(doctorId, request)
    .then(response => response.status);
}

function deleteTreatment(treatmentId) {
    return api_treatments.delete(treatmentId)
    .then(response => response.status);
}


/**
 * obtiene estudios de
 * laboratorio
 */
function getLaboratory() {
    return api_laboratory.get()
    .then(response => response.body);
};


/**
 * obtiene estudios de
 * gabinete
 */
function getCabinet() {
    return api_cabinet.get()
    .then(response => response.body);
};


export {
    getDiagnostics,
    saveDiagnostic,
    deleteDiagnostic,
    getTreatments,
    saveTreatment,
    deleteTreatment,
    getLaboratory,
    getCabinet
}