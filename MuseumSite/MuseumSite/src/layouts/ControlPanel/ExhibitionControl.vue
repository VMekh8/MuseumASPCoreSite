<template>
  <div class="container">
    <div class="head">
      <h2>Виставки</h2>
      <router-link to="/controlpanel/addexhibition">Додати виставку</router-link>
    </div>
    <div class="table">
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Назва</th>
            <th>Опис</th>
            <th>Дата</th>
            <th>Зображення</th>
            <th>Взаємодія</th>
            <th>Взаємодія з експонатами</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="exhibition in exhibitions" :key="exhibition.id">
            <td>{{ exhibition.id }}</td>
            <td>{{ exhibition.name }}</td>
            <td>{{ exhibition.description }}</td>
            <td>{{ exhibition.EventDate}}</td>
            <td><img :src="'data:;base64,' + exhibition.image" /></td>
            <td>
              <button>Редагувати</button>
              <button @click="deleteExhibition(exhibition.id)">Видалити</button>
            </td>
            <td>
              <button>Ексопонати на виставці</button>
              <button>Додати експонати</button>
              <button>Видалити експонати</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { ExhibitionResponce } from '../../Models/Exhibition';
import { apiClient } from '../../apiClient';
import moment from 'moment';

export default {
  setup() {
    const exhibitions = ref<ExhibitionResponce[]>([]);

    const FormatDate = (value: string) => {
      return moment(value).format('DD-MMM-YYYY HH:mm:ss')
    }

    const ExhibitionFetch = async () => {
      const response = await apiClient.get('/Client/GetAllExhibitions');
      exhibitions.value = response.data.map((exhibition: any) => new ExhibitionResponce(
        exhibition.id,
        exhibition.name,
        exhibition.description,
        exhibition.EventDate,
        exhibition.image,
        exhibition.exhibits
      ));
    }

    const deleteExhibition = async (id: number) => {
      await apiClient.delete('/Delete/DeleteExhibition' + id);
      exhibitions.value = exhibitions.value.filter(exhibition => exhibition.id !== id);
    }

    onMounted(ExhibitionFetch);

    return { exhibitions, deleteExhibition };
  }
}
</script>