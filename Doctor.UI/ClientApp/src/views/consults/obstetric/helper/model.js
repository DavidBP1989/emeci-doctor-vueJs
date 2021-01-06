import Vue from 'vue'

export default class model {
    constructor(req) {

        this.errors = []

        this.__$ = {
            BasicConsult: {
                Weight: req.weight,
                Size: req.size,
                Temperature: req.temperature,
                BloodPressure_A: req.bloodPressure_A,
                BloodPressure_B: req.bloodPressure_B,
                ReasonForConsultation: req.reasonForConsultation
            },
            PatientConsult: {
                PatientId: patientId,
                Allergies: patientInfo.allergies,
                Reserved: patientInfo.reserved,
                RelevantPathologies: patientInfo.relevantPathologies
            },
            PregnancyNumber: req.pregnancyNumber,
            SexuallyActive: req.sexuallyActive,
            Abortions: req.abortions,
            LastParturitionDate: req.lastParturitionDate != null
            ? Vue.moment(req.lastParturitionDate).format('YYYY-MM-DD') : null,
            FirstDayOfLastMenstruation: req.firstDayMenstruation != null
            ? Vue.moment(req.firstDayMenstruation).format('YYYY-MM-DD') : null,
            PreviousToxemias: req.toxemias,
            SpecifyToxemias: null,
            /*PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,
            PregnancyNumber: null,*/
        }
    }

    validate() {
        if (!this.__$.PatientConsult.PatientId)
            this.errors.push('No se encontro el id del paciente');
    }
}