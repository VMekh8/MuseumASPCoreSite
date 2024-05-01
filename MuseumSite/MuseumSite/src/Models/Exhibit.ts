export class ExhibitResponce {
    id: number;
    title: string;
    description: string;
    image: string;

    constructor(id: number, title: string, description: string, image: string) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.image = image;
    }
}

export class ExhibitRequest {
    id: number;
    title: string;
    description: string;
    image: File | null;

    constructor (id: number, title: string, desc: string, image: File | null) {
        this.id = id;
        this.title = title;
        this.description = desc;
        this.image = image;
    }
}
