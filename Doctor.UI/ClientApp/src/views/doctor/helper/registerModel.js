import { validEmail } from '../../../helper/util';

export default class registerModel {
    constructor(data, states, stateSelected, cities, citySelected) {

        this.errors = [];

        this.__data = {
            states,
            cities,
            stateSelected,
            citySelected
        };

        this.__$ = {
            Name: data.name,
            LastName: data.lastName,
            RFC: data.rfc,
            CURP: data.curp,
            NoSEP_ProfessionalCertificate: data.sepCertificate,
            NoSSA: data.ssa,
            NoCertification_CMCP: data.cmcp,
            ProfessionalResidenceHospital: data.hospital,
            UniversitySpecialty: data.university,
            SpecialtyCertificate: data.certificate,
            NameStateSchool: data.school,
            NameStateGrouping: data.grouping,
            Address: data.address,
            Colony: data.colony,
            PostalCode: data.cp,
            State: data.state,
            City: data.city,
            OfficePhone: data.officePhone,
            OfficeAddress: data.officeAddress,
            Phone: data.phone,
            CellPhone: data.cellphone,
            Email: data.email
        }
    }

    validate() {
        if (!this.__$.Name)
            this.errors.push(this.empty('Nombre(s)'));
        if (!this.__$.LastName)
            this.errors.push(this.empty('Apellido(s)'));
        if (!this.__$.RFC)
            this.errors.push(this.empty('RFC'));
        if (!this.__$.CURP)
            this.errors.push(this.empty('CURP'));
        if (!this.__$.NoSEP_ProfessionalCertificate)
            this.errors.push(this.empty('No. Cédula profesional SEP'));
        if (!this.__$.NoSSA)
            this.errors.push(this.empty('No. S.S.A.'));
        if (!this.__$.SpecialtyCertificate)
            this.errors.push(this.empty('Cédula de especialidad SEP'));

        if (!this.__data.stateSelected)
            this.errors.push(this.empty('Estado'));
        else {
            const value = this.__data.states.find(x => x.Name === this.__data.stateSelected);
            if (value) this.__$.State = value.Id;
            else {
                this.errors.push('<strong>Estado</strong> incorrecto');
            }
        }

        if (!this.__data.citySelected)
            this.errors.push(this.empty('Ciudad'));
        else {
            const value = this.__data.cities.find(x => x.Name === this.__data.citySelected);
            if (value) this.__$.City = value.Id;
            else {
                this.errors.push('<strong>Ciudad</strong> incorrecto');
            }
        }

        if (!this.__$.PostalCode)
            this.errors.push(this.empty('Código postal'));
        if (!this.__$.OfficePhone)
            this.errors.push(this.empty('Teléfono de consultorio'));
        if (!this.__$.Phone)
            this.errors.push(this.empty('Teléfono'));
        if (!this.__$.CellPhone)
            this.errors.push(this.empty('Celular'));

        if (!this.__$.Email)
            this.errors.push(this.empty('Correo(s) electrónico(s)'));
        else if (!validEmail(this.__$.Email))
            this.errors.push('Correo(s) electrónico(s) invalido');
    }

    empty(txt) {
        return `<strong>${txt}</strong> no debe ser vacio`;
    }
}