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
</script>