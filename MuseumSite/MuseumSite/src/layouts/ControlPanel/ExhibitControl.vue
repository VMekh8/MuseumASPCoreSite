<template>
  <div class="container">
    <div class="head">
      <h2>Експонати</h2>
      <router-link to="/controlpanel/addexhibit">Додати експонат</router-link>
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
      <tr v-for="(exhibit, index) in exhibits" :key="exhibit.id">
        <td>{{ exhibit.id }}</td>
        <td @dblclick="startEditing(index, 'title')">
          <span v-if="!isEditTitle[index]">{{ exhibit.title }}</span>
          <input v-else v-model="exhibit.title" @blur="stopEditing(index, 'title')" />
        </td>
        <td @dblclick="startEditing(index, 'description')">
          <span v-if="!isEditDescription[index]">{{ exhibit.description }}</span>
          <input v-else v-model="exhibit.description" @blur="stopEditing(index, 'description')" />
        </td>
        <td @dblclick="startEditing(index, 'image')">
          <img v-if="!isEditImage[index] " :src="'data:;base64,' + exhibit.image" />
          <input v-else type="file" @change="updateImage(index, $event)" />
        </td>
        <td>
          <button @click="updateExhibit(exhibit.id)">Редагувати</button>
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
import { ExhibitRequest, ExhibitResponce } from '../../Models/Exhibit';
import { apiClient } from '../../apiClient';

export default {
  setup() {
    const exhibits = ref<ExhibitResponce[]>([]);
    const isEditTitle = ref<boolean[]>([]);
    const isEditDescription = ref<boolean[]>([]);
    const isEditImage = ref<boolean[]>([]);

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
      await apiClient.delete('/Delete/DeleteExhibit' + id);
      exhibits.value = exhibits.value.filter(exhibit => exhibit.id !== id);
    }

    const updateImage = (index: number, event: Event) => {
      const file = (event.target as HTMLInputElement).files?.[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = () => {
          exhibits.value[index].image = reader.result as string;
        };
        reader.readAsDataURL(file)
      }
    };

    const startEditing = (index: number, field: 'title' | 'description' | 'image')  => {
      switch (field) {
            case 'title': {isEditTitle.value[index] = true};
            case 'description': {isEditDescription.value[index] = true};
            case 'image': {isEditImage.value[index] = true};
          }
    };

    const stopEditing = (index: number, field: 'title' | 'description' | 'image')  => {
      switch (field) {
            case 'title': {isEditTitle.value[index] = false};
            case 'description': {isEditDescription.value[index] = false};
            case 'image': {isEditImage.value[index] = false};
          }
    };

    const updateExhibit = async (id: number) => {
      const exhibit = exhibits.value[id];

      let exhibitRequest: ExhibitRequest | null = null;
      let imageFile: File | null = null;

      if (exhibit.image) {
        const base64data = exhibit.image;
        const filetypedata = exhibit.image.split(';')[0].split('/')[1];
        const binarydata = window.atob(base64data);

        const bytes = new Uint8Array(base64data.length);

        for (let i = 0; i < binarydata.length; i++) {
          bytes[i] = binarydata.charCodeAt(i);
        }

        const mimeType = `image/${filetypedata}`;

        const blob = new Blob([bytes], {type: mimeType});

        imageFile = new File([blob], mimeType, {type: mimeType});

        console.log(imageFile);
        exhibitRequest = new ExhibitRequest(
          exhibit.id,
          exhibit.title,
          exhibit.description,
          imageFile
        );

        try {
          const response = await apiClient.put(`/Edit/ExhibitEdit${id}`, exhibitRequest, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          });

          if (response.status === 200) {
            console.log(200);
          }
        }
        catch (error) {
          console.log(error);
        }
      }
    }

    onMounted(exhibitsFetch);

    return { exhibits, deleteExhibit, updateExhibit,  
      isEditTitle, isEditDescription, isEditImage, 
      updateImage, startEditing, stopEditing
    };
  },
}
</script>