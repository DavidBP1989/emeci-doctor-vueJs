import Vue from 'vue';

export default class model {
    constructor(patientId, patientInfo, req,) {

        this.errors = [];

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
            MenarcaAge: req.menarcaAge,
            LastMenstruationDate: req.lastMenstruationDate !== null
            ? Vue.moment(req.lastMenstruationDate).format('YYYY-MM-DD') : null,        
            Gestas: req.gestas,
            Paragestas: req.paragestas,
            Cesareans: req.cesareans,
            Abortions: req.abortions,
            NewlyBorn: req.newlyBorn,
            Stillbirth: req.stillBirth,
            AgeOfOnsetOfActiveSexualLife: req.ageOfOnsetOfActiveSexualLife,
            Menacma: req.menacma,
            Checkbox: {
                Oligomenorrea: req.selected.includes(0),
                Proiomenorrea: req.selected.includes(1),
                Hipermenorrea: req.selected.includes(2),
                Dismenorrea: req.selected.includes(3),
                Dispareunia: req.selected.includes(4),
                Leucorrea: req.selected.includes(5),
                Lactorrea: req.selected.includes(6),
                Amenorrea: req.selected.includes(7),
                Metrorragia: req.selected.includes(8),
                Others: req.selected.includes(9)
            },
            SpecifyOthers: req.specifyOthers,
            Partner: {
                HasAPartner: req.partner.hasAPartner,
                Name: req.partner.name,
                Sex: req.partner.sex,
                MaritalStatus: req.partner.maritalStatus,
                GroupRH: req.partner.groupRH,
                BirthDate: req.partner.birthDate !== null
                ? Vue.moment(req.partner.birthDate).format('YYYY-MM-DD') : null,
                Age: req.partner.age,
                Occupation: req.partner.occupation,
                Phone: req.partner.phone
            }
        }
    }

    validate() {
        if (!this.__$.PatientConsult.PatientId)
            this.errors.push('No se encontro el id del paciente');
    }
}