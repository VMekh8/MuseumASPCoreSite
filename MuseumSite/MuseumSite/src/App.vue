<script setup>
import Footer from './layouts/MainPage/Footer.vue';
import NavBar from './layouts/MainPage/NavBar.vue';
import HeroSection from './layouts/MainPage/HeroSection.vue';
import MuseumNewsSection from './layouts/MainPage/MuseumNewsSection.vue';
import ExhibitSection from './layouts/MainPage/ExhibitSection.vue';
import ExhibitionSection from './layouts/MainPage/ExhibitionSection.vue';
</script>


<template>
    <NavBar class="sticky-top"/>
    <HeroSection/>
    <MuseumNewsSection/>
    <ExhibitSection/>
    <ExhibitionSection/>
    <router-view class="v-100 mb-5"/>
    <Footer/>
</template>

