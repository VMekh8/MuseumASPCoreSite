<script lang="ts">
export class ExibitResponce {
    id: number;
    title: string;
    description: string;
    image: string;
    exhibitionId : number | null;

    constructor(id: number, title: string, description: string, image: string, exhibitionId: number | null) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.image = image;
        this.exhibitionId = exhibitionId;
    }
}

export class ExhibitRequest {
    id: number;
    title: string;
    description: string;
    image: File | null;
    exhibitionId: number | null;

    constructor (id: number, title: string, desc: string, image: File | null, exhibId: number | null)
    {
        this.id = id;
        this.title = title;
        this.description = desc;
        this.image = image;
        this.exhibitionId = exhibId;
    }
}
</script>