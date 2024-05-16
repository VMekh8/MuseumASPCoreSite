<template>
    <div class="container">
      <header class="w-100">
        <p class="header-text">Виставки нашого музею!</p>
        <div class="search-bar">
          <input type="text" placeholder="Пошук..." class="search-input form-control-md" v-model="" />
          <b-button variant="dark" class="search-button btn-lg" @click="FindElement()">
            <span class="button-content">
              <img src="../../img/art_exhibition_museum_69903.jpg" alt="Search Icon" class="search-icon" />
              Знайти
            </span>
          </b-button>
          <b-button variant="outline-light" @click="" class="search-button btn-lg">
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
            v-for="item in "
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

      <b-modal v-model="modalVisible" title="Інформація про виставку" hide-footer no-close-on-backdrop>
      <div class="mb-12" v-if="">
        <b-card no-body class="overflow-hidden w-100 m-0 mb-3">
          <b-row no-gutters>
            <b-col md="6">
              <b-card-img :src="'data:;base64,' + " alt="Image" class="rounded-0"></b-card-img>
            </b-col>
            <b-col md="6">
              <b-card-body :title=" + ' ' + ">
                  <b-card-text>{{  }}</b-card-text>
                  <b-card-text>Всього на виставці такі номери експонатів: <br> {{  }}</b-card-text>                  
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

  </script>