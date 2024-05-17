<script setup>
import Footer from './layouts/MainPage/Footer.vue';
import NavBar from './layouts/MainPage/NavBar.vue';

</script>


<template>
    <NavBar class="sticky-top"/>
    <router-view class="mb-5"/>
    <Footer/>
</template>

