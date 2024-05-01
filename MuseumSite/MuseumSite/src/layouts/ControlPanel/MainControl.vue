<template>
    <div class="container">
        <h1>Панель керування</h1>

        <nav>
            <router-link to="/controlpanel/exhibitcontrol">Експонати</router-link>
            <router-link to="/controlpanel/exhibitioncontrol">Виставки</router-link>
            <router-link to="/controlpanel/museumnewscontrol">Новини</router-link>
            <router-link to="/controlpanel/usercontrol">Користувачі</router-link>
            <router-link to="">Ролі Користувачів</router-link>
        </nav>

        <main>
            <router-view/>
        </main>
    </div>
</template>