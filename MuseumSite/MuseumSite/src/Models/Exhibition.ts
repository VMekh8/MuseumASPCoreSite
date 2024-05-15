import type { ExhibitResponse } from './Exhibit';

export class ExhibitionResponse {
    id: number;
    name: string;
    description: string;
    EventDate: Date | null;
    image: string;
    Exhibits: ExhibitResponse[];

    constructor(id: number, name: string, desc: string, EventDate: Date | null, image: string, Exhibits: ExhibitResponse[]) {
        this.id = id;
        this.name = name;
        this.description = desc;
        this.image = image;
        this.EventDate = EventDate;
        this.Exhibits = Exhibits;
    }
}

export class ExhibitionRequest {
    id: number;
    name: string;
    description: string;
    EventDate: Date | null;
    image: File | null;

    constructor(id: number, name: string, desc: string, EventDate: Date | null, image: File | null) {
        this.id = id;
        this.name = name;
        this.description = desc;
        this.EventDate = EventDate;
        this.image = image;
    }
}
