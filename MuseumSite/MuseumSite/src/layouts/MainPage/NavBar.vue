<script lang="ts">
import { defineComponent, inject } from 'vue';

export default {
    data() {
      return {
        globalState: inject('globalState')
      }
    }
}
</script>

<template>
<nav class="navbar navbar-expand-lg fixed-top bg-light navbar-light">
  <div class="container">
    <router-link class="navbar-brand gap-3 d-flex flex-row p-2" to="/"
      ><img
        id="logo"
        src="../../img/publicmuseumsign_89226.png"
        alt="Logo"
        draggable="false"
        height="40"
    /><p class="m-1">Гусятинський краєзнавчий музей</p></router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-mdb-toggle="collapse"
      data-mdb-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <i class="fas fa-bars"></i>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav ms-auto align-items-center">
        <li class="nav-item">
          <router-link class="nav-link mx-2 fw-bold d-flex align-items-center" to="/"><b-icon-menu-app class="me-2"/>Головна</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link mx-2 fw-bold d-flex align-items-center" to="/allmuseumnews"><b-icon-newspaper class="me-2"/>Новини</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link mx-2 fw-bold d-flex align-items-center" to="/allexhibit"><b-icon-lightbulb class="me-2"/> Експонати</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link mx-2 fw-bold d-flex align-items-center" to="/allexhibition"><b-icon-view-stacked/> Виставки</router-link>
        </li>
        <li class="nav-item ms-3">
          <router-link class="btn btn-outline-dark" v-if="globalState.userRoles.length >= 1" to="/login">Вихід</router-link>
          <router-link class="btn btn-outline-dark" v-else to="/login">Авторизація</router-link>
        </li>
        <li class="nav-item ms-3">
          <router-link class="btn btn-outline-dark" v-if="globalState.userRoles.includes('Admin' || 'Worker')  " to="/controlpanel">Панель керування</router-link>
        </li>
      </ul>
    </div>
  </div>
</nav>
</template>