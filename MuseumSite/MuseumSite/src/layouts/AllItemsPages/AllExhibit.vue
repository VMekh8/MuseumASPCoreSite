<template>
    <div class="container">
      <header class="w-100">
        <p class="header-text">Експонати нашого музею!</p>
        <div class="search-bar">
          <input type="text" placeholder="Пошук..." class="search-input form-control-md" v-model="exhibitName" />
          <b-button variant="dark" class="search-button btn-lg" @click="FindElement(exhibitName)">
            <span class="button-content">
              <img src="../../img/magnifying-glass.png" alt="Search Icon" class="search-icon" />
              Знайти
            </span>
          </b-button>
          <b-button variant="outline-light" @click="exhibitsFetch" class="search-button btn-lg">
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
            v-for="item in exhibits"
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
      <div class="mb-12" v-if="selectedExhibit">
        <b-card no-body class="overflow-hidden w-100 m-0 mb-3">
          <b-row no-gutters>
            <b-col md="6">
              <b-card-img :src="'data:;base64,' + selectedExhibit.image" alt="Image" class="rounded-0"></b-card-img>
            </b-col>
            <b-col md="6">
              <b-card-body :title="selectedExhibit.id + ' ' + selectedExhibit.title">
                  <b-card-text>{{ selectedExhibit.description }}</b-card-text>
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
import { ExhibitResponse } from '../../Models/Exhibit';
import { apiClient } from '../../apiClient';

  export default {
    setup() {
      const exhibits = ref<ExhibitResponse[]>([]);
      const exhibitName = ref('');
      const selectedExhibit = ref<ExhibitResponse>();

      const modalVisible = ref(false);

      const FindElement = async (name: string) => {
        
        try {

          const response = await apiClient.get(`/Search/GetExhibitByName?name=${name}`);
          console.log(response.data);
          if (response.status === 200) {
            console.log(response.status);

            selectedExhibit.value = response.data.map((exhibit: any) => new ExhibitResponse(
                exhibit.id,
                exhibit.title,
                exhibit.description,
                exhibit.image,
              ));

            if (selectedExhibit.value) {
              showModal(selectedExhibit.value)
            }
            
          }          
        }
        catch (error) {
          console.log(error);
        }
      }

      const showModal =  (item: ExhibitResponse) => {
        selectedExhibit.value = item;
        modalVisible.value = true;
      }

      const closeModal = () => {
        modalVisible.value = false
      }

      const exhibitsFetch = async () => {

        try {
          const response = await apiClient.get('/Client/GetAllExhibit');

          if (response.status === 200) {
            console.log(response.status);
            exhibits.value = response.data.map((exhibit: any) => new ExhibitResponse(
                exhibit.id,
                exhibit.title,
                exhibit.description,
                exhibit.image
              ));
            }
          }
        catch (error) {
          console.log(error);
        }
      }

      onMounted(exhibitsFetch);

      return {exhibits, exhibitName, selectedExhibit, 
        exhibitsFetch, showModal, closeModal, modalVisible,
        FindElement
      }
      
    }
  }
  </script>
  
  <style>
  .container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
  }
  .header-text {
    color: #fff;
    font-size: 45px;
    font-weight: 600;
  }
  header {
    width: 100%;
    height: 40%;
    background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)),
      url(../../img/exhibitBackground.jpg);
    background-position: center;
    background-size: cover;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 8vh 0;
  }
  .search-bar {
    margin-top: 20px;
    display: flex;
    align-items: center;
  }
  .search-input {
    padding: 10px;
    border: none;
    border-radius: 5px;
    margin-right: 10px;
    width: 300px;
  }
  .search-button {
    height: 38px;
    margin-right: 10px;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .button-content {
    display: flex;
    align-items: center;
  }
  .search-icon {
    height: 16px;
    width: 16px;
    margin-right: 5px;
  }
  main {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    margin-top: 20px;
    width: 100%;
  }
  .card {
    width: calc(33.33% - 20px);
    margin: 10px;
  }
  .divider {
    width: 100%;
    height: 3px;
    margin: 1%;
    background-color: #ccc;
  }
  @media (max-width: 768px) {
    .card {
      width: calc(50% - 20px);
    }
  }
  .modal-backdrop {
    display: none !important;
  }
  
  .modal {
    z-index: 1050 !important;
  }
  </style>
