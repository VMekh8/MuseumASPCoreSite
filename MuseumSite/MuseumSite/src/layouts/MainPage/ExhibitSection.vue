<template>
    <section class="exhibit-section" id="exhibit-section">
        <h2>Експонати музею!</h2>
        <p>Експонати - це тихі оповідачі, що розповідають нам історії без слів.</p>

        <div class="gallery-exhibit">
            <Carousel>
                <Slide v-for="slide in exhibits" :key="slide.id">
                    <div class="slide-exhibit">
                        <img :src="'data:;base64, '+ slide.image" alt="">
                        <h3>{{ slide.title }}</h3>
                        <p>{{ slide.description }}</p>    
                    </div>
                </Slide>


            <template #addons>
                
                <Navigation />
                <Pagination />
            </template>
            </Carousel>
        </div>
        <router-link to="/allexhibit" class="btn btn-outline-dark w-50 h-50 m-0 mb-5 mt-5">Переглянути всі експонати</router-link>

    </section>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { ExhibitResponse } from '../../Models/Exhibit';
import { apiClient } from '../../apiClient';
import { defineComponent } from 'vue'
import { Carousel, Navigation, Pagination, Slide } from 'vue3-carousel'

export default defineComponent({
    components: {
        Carousel,
        Slide,
        Pagination,
        Navigation,
    },
    setup() {

        const slide = ref(0);

        const exhibits = ref<ExhibitResponse[]>([]);

        const onSlideStart = (slide: number) => {
            console.log('Carousel started sliding to slide', slide);
        } 

        const onSlideEnd = (slide: number) => {
            console.log('Carousel finished sliding to slide', slide);
        }

        const exhibitsFetch = async () => {

            try {

                const response = await apiClient.get('/Client/GetAllExhibit');

                if (response.status === 200) {
                    console.log(response.status);

                    exhibits.value = response.data.map((e) => new ExhibitResponse(
                        e.id,
                        e.title,
                        e.description,
                        e.image
                    ));

                    console.log(exhibits.value[0].id);
                }
            }
            catch(error) {
                console.log(error);
            }
        }

        onMounted(exhibitsFetch);

        return {
            slide, exhibits, onSlideStart,
            onSlideEnd, exhibitsFetch
        }
    }
})
</script>

<style>
    @import url('../../assets/mainPage.css');
</style>