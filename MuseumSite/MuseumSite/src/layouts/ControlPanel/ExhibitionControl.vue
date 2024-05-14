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
          <tr v-for="(exhibition, index) in exhibitions" :key="exhibition.id">
            <td>{{ exhibition.id }}</td>
            <td @dblclick="startEditing(index, 'name')">
              <span v-if="!isEditName[index]">{{ exhibition.name }}</span>
              <input v-else type="text" v-model="exhibition.name" @blur="stopEditing(index, 'name')">
            </td>
            <td @dblclick="startEditing(index, 'description')">
              <span v-if="!isEditDesc[index]">{{ exhibition.description }}</span>
              <input v-else type="text" v-model="exhibition.description" @blur="stopEditing(index, 'description')">
            </td>
            <td @dblclick="startEditing(index, 'eventDate')">
              <span v-if="!isEditDate[index]">{{ exhibition.EventDate }}</span>
              <input v-else type="datetime-local" v-model="exhibition.EventDate" @blur="stopEditing(index, 'eventDate')">
            </td>
            <td @dblclick="startEditing(index, 'image')">
              <img v-if="!isEditImage[index] " :src="'data:;base64,' + exhibition.image" />
          <input v-else type="file" @change="updateImage(index, $event)" />
            </td>
            
            <td>
              <button @click="updateExhibition(exhibition.id)">Редагувати</button>
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
      if (value) {
                return moment(value).format('DD-MMM-YYYY HH:mm:ss')
            }
    } 

    const ExhibitionFetch = async () => {
      const response = await apiClient.get('/Client/GetAllExhibitions');
      console.log(response.data);
      exhibitions.value = response.data.map((exhibition: any) => new ExhibitionResponce(
        exhibition.id,
        exhibition.name,
        exhibition.description,
        exhibition.date,
        exhibition.image,
        exhibition.exhibits
      ));
      console.log(exhibitions.value);
    }
    const deleteExhibition = async (id: number) => {
      await apiClient.delete('/Delete/DeleteExhibition' + id);
      exhibitions.value = exhibitions.value.filter(exhibition => exhibition.id !== id);
    }

    onMounted(ExhibitionFetch);

    return { exhibitions, deleteExhibition, FormatDate };
  }
}
</script>