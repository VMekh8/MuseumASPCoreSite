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
            <th>Взаємодія</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="exhibit in exhibits" :key="exhibit.id">
            <td>{{ exhibit.id }}</td>
            <td>{{ exhibit.title }}</td>
            <td>{{ exhibit.description }}</td>
            <td><img :src="'data:;base64,' + exhibit.image" /></td>
            <td>
              <button>Редагувати</button>
              <button @click="deleteExhibit(exhibit.id)">Видалити</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { ExhibitResponce } from '../../Models/Exhibit';
import { apiClient } from '../../apiClient';

export default {
  setup() {
    const exhibits = ref<ExhibitResponce[]>([]);

    const exhibitsFetch = async () => {
      const response = await apiClient.get('/Client/GetAllExhibit');
      exhibits.value = response.data.map((exhibit: any) => new ExhibitResponce(
        exhibit.id,
        exhibit.title,
        exhibit.description,
        exhibit.image,
      ));
    };

    const deleteExhibit = async (id: number) => {
      const response = await apiClient.delete('/Delete/DeleteExhibit' + id);
      exhibits.value = exhibits.value.filter(exhibit => exhibit.id !== id);
    }

    onMounted(exhibitsFetch);

    return { exhibits, deleteExhibit };
  },
}
</script>