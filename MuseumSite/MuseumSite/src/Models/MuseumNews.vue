<script lang="ts">
export class MuseumNewsResponce {
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
</script>