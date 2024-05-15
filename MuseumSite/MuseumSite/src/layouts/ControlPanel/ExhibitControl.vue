<template>
  <div class="container">
    <div class="head">
      <h2>Експонати</h2>
      <router-link to="/controlpanel/addexhibit" class="btn btn-success mx-2">Додати експонат</router-link>
    </div>
    <div class="table-responsive mt-4">
      <table class="table table-bordered table-hover w-100">
        <thead class="thead-dark">
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
            <td @dblclick="startEditing(index, 'title')" class="edit-cell">
              <span v-if="!isEditTitle[index]">{{ exhibit.title }}</span>
              <input v-else v-model="exhibit.title" @blur="stopEditing(index, 'title')" class="form-control" />
            </td>
            <td @dblclick="startEditing(index, 'description')" class="edit-cell">
              <span v-if="!isEditDescription[index]">{{ exhibit.description }}</span>
              <input v-else v-model="exhibit.description" @blur="stopEditing(index, 'description')" class="form-control" />
            </td> 
            <td @dblclick="startEditing(index, 'image')" class="edit-cell">
              <img v-if="!isEditImage[index]" :src="'data:;base64,' + exhibit.image" class="img-thumbnail" />
              <input v-else type="file" @change="updateImage(index, $event)" class="form-control-file" />
            </td>
            <td class="actions-cell ">
              <button @click="updateExhibit(exhibit.id)" class="btn btn-warning btn-md m-1">Редагувати</button>
              <button @click="deleteExhibit(exhibit.id)" class="btn btn-danger btn-md m-1">Видалити</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { ExhibitResponse } from '../../Models/Exhibit';
import { apiClient } from '../../apiClient';

export default {
  setup() {
    const exhibits = ref<ExhibitResponse[]>([]);
    const isEditTitle = ref<boolean[]>([]);
    const isEditDescription = ref<boolean[]>([]);
    const isEditImage = ref<boolean[]>([]);

    const exhibitsFetch = async () => {
      const response = await apiClient.get('/Client/GetAllExhibit');
      exhibits.value = response.data.map((exhibit: any) => new ExhibitResponse(
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

    const stopEditing = async (index: number, field: 'title' | 'description' | 'image')  => {
      switch (field) {
            case 'title': {isEditTitle.value[index] = false};
            case 'description': {isEditDescription.value[index] = false};
            case 'image': {isEditImage.value[index] = false};
          }
          await updateExhibit(exhibits.value[index].id);
    };

    const updateExhibit = async (id: number) => {
      const exhibit = exhibits.value.find(ex => ex.id === id);

      if (exhibit) {
        const formData = new FormData();
        formData.append('Id', exhibit.id.toString());
        formData.append('Title', exhibit.title);
        formData.append('Description', exhibit.description);

        let base64String = exhibit.image;
        if (base64String && base64String.startsWith('data:')) {
          const mimeType = base64String.split(':')[1].split(';')[0];

          if (!base64String.startsWith(`data:${mimeType};base64,`)) {
            base64String = `data:${mimeType};base64,${base64String.split(',')[1]}`;
          }

          const binaryString = window.atob(base64String.split(',')[1]);
          const bytes = new Uint8Array(binaryString.length);
          for (let i = 0; i < binaryString.length; i++) {
            bytes[i] = binaryString.charCodeAt(i);
          }
          const file = new File([bytes], 'image', { type: mimeType });
          formData.append('Image', file);
        }

        try {
          const response = await apiClient.put(`/Edit/ExhibitEdit/${id}`, formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          });

          if (response.status === 200) {
            console.log(200);
            await exhibitsFetch();
          }
        } catch (error) {
          console.log(error);
        }
      }
    };

    onMounted(exhibitsFetch);

    return { exhibits, deleteExhibit, updateExhibit,  
      isEditTitle, isEditDescription, isEditImage, 
      updateImage, startEditing, stopEditing
    };
  },
}
</script>