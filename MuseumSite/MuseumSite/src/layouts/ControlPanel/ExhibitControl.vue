<template>
    <div class="container">
        <div class="head">
            <h2>Експонати</h2>
            <router-link to="#">Додати експонат</router-link>

        </div>
        
        <div class="table">
            <table>
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Назва</th>
                  <th>Опис</th>
                  <th>Зображення</th>
                  <th>ID Виставки</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="exhibit in exhibits" :key="exhibit.id">
                  <td>{{ exhibit.id }}</td>
                  <td>{{ exhibit.title }}</td>
                  <td>{{ exhibit.description }}</td>
                  <td><img :src="'data:;base64,' + exhibit.image" /></td>
                </tr>
              </tbody>
            </table>

        </div>

    </div>
</template>

<script lang="ts">
import axios from "axios";
import { ref, onMounted } from 'vue';
import { ExhibitResponce } from '../../Models/Exhibit';

export default {
    setup() {
        const exhibits = ref<ExhibitResponce[]>([]);
        
        const exhibitsFetch = async () => {

            const response = await axios.get(import.meta.env.VITE_API_URL+'/Client/GetAllExhibit');

            exhibits.value = response.data.map((exhibit: any) => new ExhibitResponce(
                exhibit.id,
                exhibit.title,
                exhibit.description,
                exhibit.image,
            ));
        };
        
        onMounted(exhibitsFetch);

        return {exhibits};
    }
}

</script>