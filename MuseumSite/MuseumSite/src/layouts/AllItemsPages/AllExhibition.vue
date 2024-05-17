<template>
  <main>
  <header class="w-100">
        <p class="header-text">Виставки нашого музею!</p>
        <div class="search-bar">
          <input type="text" placeholder="Пошук..." class="search-input form-control-md" v-model="exhibitionName" />
          <b-button variant="dark" class="search-button btn-lg" @click="FindExhibition(exhibitionName)">
            <span class="button-content">
              <img src="../../img/magnifying-glass.png" alt="Search Icon" class="search-icon" />
              Знайти
            </span>
          </b-button>
          <b-button variant="outline-light button-content" @click="ExhibitionFetch" class="search-button btn-lg">
              Оновити елементи
          </b-button>
        </div>
      </header>
    <div class="container">
      <div class="divider"></div>
      <main class="w-100">
        <b-card-group deck class="card-group">
          <b-card
            v-for="item in exhibitions"
            :key="item.id"
            :title="item.name"
            :img-src="'data:;base64,' + item.image"
            img-alt="Image"
            class="card"
          >
            <b-card-text>{{ item.description.substring(0, 100) }}...</b-card-text>
            <b-button variant="outline-dark w-100 m-1" @click="showModal(item)">Детальніше</b-button>
          </b-card>
        </b-card-group>
      </main>

      <b-modal v-model="modalVisible" title="Інформація про виставку" hide-footer no-close-on-backdrop>
      <div class="mb-12" v-if="selectedExhibition">
        <b-card no-body class="overflow-hidden w-100 m-0 mb-3">
          <b-row no-gutters>
            <b-col md="6">
              <b-card-img v-if="selectedExhibition.id !== 0" :src="'data:;base64,' + selectedExhibition.image" alt="Image" class="rounded-0"></b-card-img>
              <b-card-img v-else src="https://ih1.redbubble.net/image.2634106597.4467/bg,f8f8f8-flat,750x,075,f-pad,750x1000,f8f8f8.jpg" alt="Image" class="rounded-0"></b-card-img>

            </b-col>
            <b-col md="6">
              <b-card-body :title=" selectedExhibition.id + ' ' + selectedExhibition.name ">
                  <b-card-text>{{ selectedExhibition.description }}</b-card-text>
                  <b-card-text>Всього на виставці такі номери експонатів: <br> {{ selectedExhibition.Exhibits }}</b-card-text>                  
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
  </main>
  </template>
  
<script lang="ts">
    import { ref, onMounted } from 'vue';
    import { ExhibitionResponse } from '../../Models/Exhibition';
    import { apiClient } from '../../apiClient';

    export default {
        setup() {

            const modalVisible = ref(false);

            const exhibitionName = ref('');
            const exhibitions = ref<ExhibitionResponse[]>([]);
            const selectedExhibition = ref<ExhibitionResponse>();

            const ExhibitionFetch = async () => {
                
                try {
                    const response = await apiClient.get('/Client/GetAllExhibitions');

                    if (response.status === 200) {
                        console.log(response.status)

                        exhibitions.value = response.data.map((exhibition: any) => new ExhibitionResponse(
                            exhibition.id,
                            exhibition.name,
                            exhibition.description,
                            exhibition.date,
                            exhibition.image,
                            exhibition.exhibits
                        ));
                    }
                }
                catch (error) {
                    console.log(error);
                }
            }

            const FindExhibition = async (name: string) => {

                try {

                    const response = await apiClient.get(`/Search/GetExhibitionByName?name=${name}`);

                    if (response.status === 200) {

                        selectedExhibition.value = new ExhibitionResponse(
                            response.data.id,
                            response.data.name,
                            response.data.description,
                            response.data.date,
                            response.data.image,
                            response.data.exhibits
                        );

                        if (selectedExhibition.value) {
                            showModal(selectedExhibition.value);
                        }
                    }
                }
                catch (error) {
                    console.log(error);
                }
            }

            const showModal = (item: ExhibitionResponse) => {
                selectedExhibition.value = item;
                modalVisible.value = true
            }

            const closeModal  = () => {
                modalVisible.value = false;
            }

            onMounted(ExhibitionFetch);

            return {
                closeModal, showModal, FindExhibition, ExhibitionFetch,
                modalVisible, exhibitionName, exhibitions, selectedExhibition
            }

        }
    }

</script>

<style>
    @import url('../../assets/main.css');
    header {
    background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)),
      url(../../img/art_exhibition_museum_69903.jpg);
  }
</style>