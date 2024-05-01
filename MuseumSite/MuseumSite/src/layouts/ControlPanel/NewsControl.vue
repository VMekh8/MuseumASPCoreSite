<template>
    <div class="container">
        <div class="head">
            <h2>Новини музею</h2>
            <router-link to="#">Додати новину</router-link>

        </div>
        
        <div class="table">
            <table>
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Назва</th>
                  <th>Опис</th>
                  <th>Зображення</th>
                  <th>Взаємодія</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="news in museumnews" :key="news.id">
                  <td>{{ news.id }}</td>
                  <td>{{ news.title }}</td>
                  <td>{{ news.description }}</td>
                  <td><img :src="'data:;base64,' + news.image" /></td>
                  <td><button>Редагувати</button>
                  <button @click="newsDelete(news.id)">Видалити</button></td>
                </tr>
              </tbody>
            </table>

        </div>

    </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { MuseumNewsResponce } from '../../Models/MuseumNews';
import { apiClient } from '../../apiClient';

export default {
    setup() {

        const museumnews = ref<MuseumNewsResponce[]>([]);

        const NewsFetch = async () => {

            const response = await apiClient.get('/Client/GetAllNews');

            museumnews.value = response.data.map((news: any) => new MuseumNewsResponce(
                news.id,
                news.title,
                news.description,
                news.image
            ));
        };
        
        const newsDelete = async (id: number) => {
          await apiClient.delete('/Delete/DeleteNews' + id);
          museumnews.value = museumnews.value.filter(news=> news.id !== id);
        }


        onMounted(NewsFetch);

        return {museumnews, newsDelete};
    }
}

</script>
