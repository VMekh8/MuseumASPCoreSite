<template>
    <div class="container">
      <header class="w-100">
        <p class="header-text">Експонати нашого музею!</p>
        <div class="search-bar">
          <input type="text" placeholder="Пошук..." class="search-input form-control-md" />
          <b-button variant="dark" class="search-button btn-lg">
            <span class="button-content">
              <img src="../../img/magnifying-glass.png" alt="Search Icon" class="search-icon" />
              Знайти
            </span>
          </b-button>
          <b-button variant="outline-light" @click="refreshItems" class="search-button btn-lg">
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
            v-for="item in exhibitItems"
            :key="item.id"
            :title="item.title"
            :img-src="item.image"
            img-alt="Image"
            img-top
            class="card"
          >
            <b-card-text>{{ item.description }}</b-card-text>
            <b-button variant="outline-dark w-100 m-1">Детальніше</b-button>
          </b-card>
        </b-card-group>
      </main>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        exhibitItems: []
      };
    },
    methods: {
      refreshItems() {
        // Логіка оновлення елементів з сервера
      }
    }
  };
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
  </style>