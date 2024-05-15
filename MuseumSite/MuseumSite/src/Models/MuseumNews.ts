export class MuseumNewsResponse {
    id: number;
    title: string;
    description: string;
    image: string;

    constructor(id: number, title: string, desc: string, image: string) {
        this.id = id;
        this.title = title;
        this.description = desc;
        this.image = image;
    }
}

export class MuseumNewsRequest {
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
