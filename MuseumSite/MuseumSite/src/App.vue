<script setup>
import Footer from './layouts/MainPage/Footer.vue';
</script>


<template>
        <router-link to="/login">Авторизація</router-link>
        <router-link to="/controlpanel">Панель керування</router-link>
        <router-link to="/allexhibit">Всі експонати</router-link>
    <router-view/>
    <Footer/>
</template>

