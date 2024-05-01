<template>
<div class="container">
    <nav>
        <router-link to="/login">Авторизація</router-link>
        <router-link to="/controlpanel">Панель керування</router-link>
    </nav>

    <main>
        <router-view/>
    </main>
    
</div>
</template>