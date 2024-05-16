<template>
    <div class="container">
      <header class="w-100">
        <p class="header-text">Новини нашого музею!</p>
        <div class="search-bar">
          <input type="text" placeholder="Пошук..." class="search-input form-control-md" v-model="museumNewsName" />
          <b-button variant="dark" class="search-button btn-lg" @click="FindElement(museumNewsName)">
            <span class="button-content">
              <img src="../../img/magnifying-glass.png" alt="Search Icon" class="search-icon" />
              Знайти
            </span>
          </b-button>
          <b-button variant="outline-light" @click="MuseumNewsFetch" class="search-button btn-lg">
            <span class="button-content">
              Оновити елементи
            </span>
          </b-button>
        </div>
      </header>
      <div class="divider"></div>
      <main class="w-100">
        <b-card-group deck class="card-group">
          <b-card
            v-for="item in museumNews"
            :key="item.id"
            :title="item.title"
            :img-src="'data:;base64,' + item.image"
            img-alt="Image"
            class="card"
          >
            <b-card-text>{{ item.description.substring(0, 100) }}...</b-card-text>
            <b-button variant="outline-dark w-100 m-1" @click="showModal(item)">Детальніше</b-button>
          </b-card>
        </b-card-group>
      </main>

      <b-modal v-model="modalVisible" title="Інформація про експонат" hide-footer no-close-on-backdrop>
      <div class="mb-12" v-if="selectedNews">
        <b-card no-body class="overflow-hidden w-100 m-0 mb-3">
          <b-row no-gutters>
            <b-col md="6">
              <b-card-img v-if="selectedNews.id !== 0" :src="'data:;base64,' + selectedNews.image" alt="Image" class="rounded-0"></b-card-img>
              <b-card-img v-else src="../../img/smile.png" alt="Image" class="rounded-0"></b-card-img>
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
    </div>
  </template>
  
<script lang="ts">
import { ref, onMounted } from 'vue';
import { apiClient } from '../../apiClient';
import { MuseumNewsResponse } from '../../Models/MuseumNews';


export default {
    setup() {

        const modalVisible = ref(false);

        const museumNews = ref<MuseumNewsResponse[]>([]);
        const selectedNews = ref<MuseumNewsResponse>();

        const museumNewsName = ref('');

        const MuseumNewsFetch = async () => {
            
            try {

                const response = await apiClient.get('/Client/GetAllNews');

                if (response.status === 200) {

                    museumNews.value = response.data.map((news: any) => new MuseumNewsResponse(
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
            modalVisible, museumNews, selectedNews,
            museumNewsName, MuseumNewsFetch, FindElement,
            showModal, closeModal
        }
    }
}
</script>

<style>
    @import url('../../assets/main.css');
    header {
    background-image: linear-gradient(rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7)),
      url(../../img/museumnews.jpg);
  }
</style>