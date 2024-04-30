import type { ExhibitResponce } from './Exhibit';

export class ExhibitionResponce {
    id: number;
    name: string;
    description: string;
    dateEvent: Date | null;
    image: string;
    Exhibits: ExhibitResponce[];

    constructor(id: number, name: string, desc: string, dateEvent: Date | null, image: string, Exhibits: ExhibitResponce[]) {
        this.id = id;
        this.name = name;
        this.description = desc;
        this.image = image;
        this.dateEvent = dateEvent;
        this.Exhibits = Exhibits;
    }
}

export class ExhibitionRequest {
    id: number;
    name: string;
    description: string;
    dateEvent: Date | null;
    image: File | null;

    constructor(id: number, name: string, desc: string, dateEvent: Date | null, image: File | null) {
        this.id = id;
        this.name = name;
        this.description = desc;
        this.dateEvent = dateEvent;
        this.image = image;
    }
}
