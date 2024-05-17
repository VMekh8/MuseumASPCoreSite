<template>
    <section class="museumnews-section" id="museumnews-section">
        <h2>Новини музею!</h2>
        <p>Залишайтесь в курсі останніх нових з життя нашого музею</p>
        
        <Carousel :items-to-show="2.5" :wrap-around="false">
          <Slide v-for="news in museumNews" :key="news.id">
            <div class="my-cards">
                <div class="my-card">
                    <img :src="'data:;base64, ' + news.image" alt="img">
                    <h3>{{news.title}}</h3>
                    <b-button variant="dark w-100" @click="FindElement(news.title)">Детальніше</b-button>     
                </div>
            </div>
          </Slide>         
          <template #addons>
            <Navigation />
          </template>
        </Carousel>
        <b-button>Переглянути всі новини</b-button>

    <b-modal v-model="modalVisible" title="Інформація про новину" hide-footer no-close-on-backdrop>
      <div class="mb-12" v-if="selectedNews">
        <b-card no-body class="overflow-hidden w-100 m-0 mb-3">
          <b-row no-gutters>
            <b-col md="6">
              <b-card-img v-if="selectedNews.id !== 0" :src="'data:;base64,' + selectedNews.image" alt="Image" class="rounded-0"></b-card-img>
              <b-card-img v-else src="https://ih1.redbubble.net/image.2634106597.4467/bg,f8f8f8-flat,750x,075,f-pad,750x1000,f8f8f8.jpg" alt="Image" class="rounded-0"></b-card-img>
            </b-col>
            <b-col md="6">
              <b-card-body :title="selectedNews.id + ' ' + selectedNews.title">
                  <b-card-text>{{ selectedNews.description }}</b-card-text>
              </b-card-body>
            </b-col>
          </b-row>
        </b-card>
      </div>
      <div class="d-flex justify-content-end">
        <b-button variant="primary" @click="closeModal">OK</b-button>
      </div>
    </b-modal>
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
        const selectedNews = ref<MuseumNewsResponse>();
        
        const modalVisible = ref(false);

        const museumNewsName = ref('');

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

        const FindElement = async (name:string) => {

            try {
                
                const response = await apiClient.get(`/Search/GetMuseumNewsByName?name=${name}`);
                console.log(response.status);

                selectedNews.value = new MuseumNewsResponse(
                    response.data.id,
                    response.data.title,
                    response.data.description,
                    response.data.image
                );

                showModal(selectedNews.value);
            }
            catch (error) {
                console.log(error);
            }
            
        }

        const showModal = (item: MuseumNewsResponse) => {
            selectedNews.value = item;
            modalVisible.value = true;
        }

        const closeModal = () => {
            modalVisible.value = false;
        }
        
        onMounted(MuseumNewsFetch);

        return {
            MuseumNewsFetch, museumNews,
            closeModal, showModal, FindElement,
            museumNewsName, modalVisible, selectedNews
        }
    }
})
</script>

<style>
    @import url('../../assets/mainPage.css');
</style>