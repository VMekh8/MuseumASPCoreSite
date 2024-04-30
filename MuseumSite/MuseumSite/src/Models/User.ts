export class UserResponce {
    id: string;
    email: string;
    password: string;
    phonenumber: string | null;
    firstname: string;
    lastname: string;

    constructor (id: string, email: string, password: string, phonenumber: string | null, firstname: string, lastname: string) {
        this.id = id;
        this.email = email;
        this.password = password; 
        this.phonenumber = phonenumber;
        this.firstname = firstname;
        this.lastname = lastname;
    }

}