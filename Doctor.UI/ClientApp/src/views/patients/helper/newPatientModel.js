import { validEmail } from '../../../helper/util';

class formValidation {
    constructor(name, lastName, mothersName,
        fathersName, phone, sex, emails, birthDate,
        allergy, password, confirmPassword) {

        this.errors = [];

        this.__$ = {
            Name: name,
            LastName: lastName,
            MothersName: mothersName,
            FathersName: fathersName,
            Phone: phone,
            Sex: sex,
            Emails: emails,
            BirthDate: birthDate,
            Allergy: allergy,
            Password: password
        };
        
        this.confirmPassword = confirmPassword;
    }

    validate() {
        if (!this.__$.Name)
            this.errors.push(this.empty('Nombre'));
        if (!this.__$.LastName)
            this.errors.push(this.empty('Apellido'));
        if (!this.__$.MothersName)
            this.errors.push(this.empty('Nombre completo de la madre'));
        if (!this.__$.FathersName)
            this.errors.push(this.empty('Nombre completo del padre'));
        if (!this.__$.Phone)
            this.errors.push(this.empty('Teléfono'));

        if (!this.__$.Emails)
            this.errors.push(this.empty('Correo electrónico'));
        else if (!validEmail(this.__$.Emails))
            this.errors.push('Correo electrónico invalido');

        if (!this.__$.BirthDate)
            this.errors.push(this.empty('Fecha de nacimiento'));
        if (!this.__$.Allergy)
            this.errors.push(this.empty('Alergia'));
        if (!this.__$.Password)
            this.errors.push(this.empty('Contraseña'));
        if (!this.confirmPassword)
            this.errors.push(this.empty('Confirmación de contraseña'));
        
        if (this.__$.Password && this.confirmPassword) {
            if (this.__$.Password !== this.confirmPassword)
                this.errors.push('La contraseñas no coinciden');
        }
    }

    empty(txt) {
        return `<strong>${txt}</strong> no debe ser vacio`;
    }
}

export default formValidation;