    <template>
        <div class="container">
            <h1>Панель керування</h1>
            
            <nav>
                <router-link to="/controlpanel/exhibitcontrol">Експонати</router-link>
                <router-link to="">Виставки</router-link>
                <router-link to="">Новини</router-link>
                <router-link to="">Користувачі</router-link>
            </nav>

            <main>
                <router-view/>
            </main>
        </div>
    </template>