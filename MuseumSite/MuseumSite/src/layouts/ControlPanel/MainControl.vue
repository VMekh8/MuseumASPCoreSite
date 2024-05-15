<template>
    <div class="container">
      <h1 class="mt-5">Панель керування</h1>
  
      <div class="d-flex justify-content-between mt-4">
      <div>
        <router-link to="/controlpanel/exhibitcontrol" class="btn btn-primary btn-block btn-md">Експонати</router-link>
      </div>
      <div>
        <router-link to="/controlpanel/exhibitioncontrol" class="btn btn-primary btn-block btn-md">Виставки</router-link>
      </div>
      <div>
        <router-link to="/controlpanel/museumnewscontrol" class="btn btn-primary btn-block btn-md">Новини</router-link>
      </div>
      <div>
        <router-link to="/controlpanel/usercontrol" class="btn btn-primary btn-block btn-md">Користувачі</router-link>
      </div>
      <div>
        <router-link to="" class="btn btn-primary btn-block btn-md">Ролі Користувачів</router-link>
      </div>
    </div>

  
      <hr class="mt-4">
  
      <main>
        <router-view />
      </main>
    </div>
  </template>
  