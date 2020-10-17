class formValidation {
    constructor(patientId, req, patient) {

        this.errors = [];

        this.__$ = {
            PatientId: patientId,
            Weight: req.weight,
            Size: req.size,
            Temperature: req.temperature,
            BloodPressure_A: req.bloodPressure_a,
            BloodPressure_B: req.bloodPressure_b,
            HeadCircuference: req.headCircuference,
            HeartRate: req.heartRate,
            BreathingFrecuency: req.breathingFrecuency,
            ReasonForConsultation: req.reasonForConsultation,
            PhysicalExploration: req.physicalExploration,
            PreventiveMeasures: req.preventiveMeasures,
            Observations: req.observations,
            Diagnostics: this.getArrayFormat(req.diagnostics, true),
            Treatments: this.getArrayFormat(req.treatments, true),
            CabinetStudies: this.getArrayFormat(req.cabinet, false),
            LaboratoryStudies: this.getArrayFormat(req.laboratory, false),
            Prognostic: req.prognostic,
            //datos del paciente
            Allergies: patient.allergies,
            Reserved: patient.reserved,
            RelevantPathologies: patient.relevantPathologies
        };
    }

    getArrayFormat(array, isDiagnostirOrTreatment) {
        return array.map((x) => {
            return isDiagnostirOrTreatment ? x.text : x.name;
        });
    }

    validate() {
        if (!this.__$.PatientId)
            this.errors.push('No se encontro el id del paciente');
    }
}

export default formValidation;