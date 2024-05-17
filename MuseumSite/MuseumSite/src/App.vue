<script setup>
import Footer from './layouts/MainPage/Footer.vue';
import NavBar from './layouts/MainPage/NavBar.vue';
</script>


<template>
    <NavBar/>
    
        <router-link to="/login">Авторизація</router-link>
        <router-link to="/controlpanel">Панель керування</router-link>
        <router-link to="/allexhibit">Всі експонати</router-link>
    <router-view/>
    <Footer/>
</template>

