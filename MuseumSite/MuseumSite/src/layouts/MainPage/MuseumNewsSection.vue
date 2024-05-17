<template>
    <section class="museumnews-section" id="museumnews-section">
        <h2>Новини музею!</h2>
        <p>Залишайтесь в курсі останніх нових з життя нашого музею</p>
        
        <Carousel :items-to-show="2" :wrap-around="false">
          <Slide v-for="news in museumNews" :key="news.id">
            <div class="my-cards">
                <div class="my-card">
                    <img :src="'data:;base64, ' + news.image" alt="img">
                    <h3>Mountain Hiking</h3>
                    <p>Embark on an exhilarating hiking adventure in the breathtaking mountain ranges.</p>             
                </div>
            </div>
          </Slide>         
          <template #addons>
            <Navigation />
          </template>
        </Carousel>
        <b-button>Переглянути всі новини</b-button>
    </section>
</template>


<script lang="ts">
import { ref, onMounted } from 'vue';
import { apiClient } from '../../apiClient';
import { MuseumNewsResponse } from '../..//Models/MuseumNews';
import { defineComponent } from 'vue'
import { Carousel, Navigation, Slide } from 'vue3-carousel'

import 'vue3-carousel/dist/carousel.css'

export default defineComponent({
    components: {
        Carousel,
        Slide,
        Navigation,
    },
    setup() {
        const museumNews = ref<MuseumNewsResponse[]>([]);

        const MuseumNewsFetch = async () => {
            try {
                const response = await apiClient.get('/Client/GetAllNews');

                if (response.status === 200) {
                    museumNews.value = response.data.map((news: MuseumNewsResponse) => new MuseumNewsResponse(
                        news.id,
                        news.title,
                        news.description,
                        news.image
                    ));
                }
            }
            catch (error) {
                console.log(error);
            }
        }
        
        onMounted(MuseumNewsFetch);

        return {
            MuseumNewsFetch, museumNews
        }
    }
})
</script>

<style>
    @import url('../../assets/mainPage.css');
</style>