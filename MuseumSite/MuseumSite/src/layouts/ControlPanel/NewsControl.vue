<template>
  <div class="container">
      <div class="head">
          <h2>Новини музею</h2>
          <router-link to="/controlpanel/addmuseumnews" class="btn btn-success">Додати новину</router-link>
      </div>
      
      <div class="table-responsive mt-4">
          <table class="table table-bordered table-hover">
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
                  <tr v-for="(news, index) in museumnews" :key="news.id">
                      <td>{{ news.id }}</td>
                      <td @dblclick="startEditing(index, 'title')" class="edit-cell">
                          <span v-if="!isEditTitle[index]">{{ news.title }}</span>
                          <input v-else v-model="news.title" @blur="stopEditing(index, 'title')" class="form-control">
                      </td>
                      <td @dblclick="startEditing(index, 'description')" class="edit-cell">
                          <span v-if="!isEditDescription[index]">{{ news.description }}</span>
                          <input v-else v-model="news.description" @blur="stopEditing(index, 'description')" class="form-control">
                      </td>
                      <td @dblclick="startEditing(index, 'image')" class="edit-cell w-75 h-50">
                          <img v-if="!isEditImage[index]" :src="'data:;base64,' + news.image" class="img-thumbnail" />
                          <input v-else type="file" @change="updateImage(index, $event)" class="form-control-file" />
                      </td>
                      <td class="actions-cell">
                          <button @click="newsUpdate(news.id)" class="btn btn-warning m-1">Редагувати</button>
                          <button @click="newsDelete(news.id)" class="btn btn-danger m-1">Видалити</button>
                      </td>
                  </tr>
              </tbody>
          </table>
      </div>
  </div>
</template>
<script lang="ts">
import { ref, onMounted } from 'vue';
import { MuseumNewsResponse } from '../../Models/MuseumNews';
import { apiClient } from '../../apiClient';

export default {
    setup() {

        const museumnews = ref<MuseumNewsResponse[]>([]);
        const isEditTitle = ref<boolean[]>([]);
        const isEditDescription = ref<boolean[]>([]);
        const isEditImage = ref<boolean[]>([]);

        const NewsFetch = async () => {

            const response = await apiClient.get('/Client/GetAllNews');

            museumnews.value = response.data.map((news: any) => new MuseumNewsResponse(
                news.id,
                news.title,
                news.description,
                news.image
            ));
            isEditTitle.value = museumnews.value.map(() => false);
            isEditDescription.value =  museumnews.value.map(() => false);
            isEditImage.value = museumnews.value.map(() => false);
        };
        
        const newsDelete = async (id: number) => {
          await apiClient.delete('/Delete/DeleteNews' + id);
          museumnews.value = museumnews.value.filter(news=> news.id !== id);
        }

        const newsUpdate = async (id: number) => {
          const news = museumnews.value.find(nw => nw.id === id);

          if (news) {

            const formData = new FormData();

            formData.append('Id', news.id.toString());
            formData.append('Title', news.title);
            formData.append('Description', news.description);

            let base64String = news.image;

            if (base64String && base64String.startsWith('data:')) {
              const mimeType = base64String.split(':')[1].split(';')[0];

              if (!base64String.startsWith(`data:${mimeType};base64,`)) {
                base64String = `data:${mimeType};base64,${base64String.split(',')[1]}`;
              }

              const binaryString = window.atob(base64String.split(',')[1]);
              const bytes = new Uint8Array(binaryString.length);

              for (let i =0; i<binaryString.length; i++) {
                bytes[i] = binaryString.charCodeAt(i);
              }

              const file = new File([bytes], 'image', {type: mimeType});

              formData.append('Image', file);

            }

            try {
              const response = await apiClient.put(`/Edit/NewsEdit/${id}`, formData, {
                headers: {
                  'Content-Type': 'multipart/form-data'
                }
              });

              if (response.status === 200) {
                console.log(response.status);

                await NewsFetch();
              }

            }
            catch (error) {
              console.log(error);
            }
          }
        }

        const updateImage = (index: number, event: Event) => {
          const file = (event.target as HTMLInputElement).files?.[0];
          if (file) {
            const reader = new FileReader();
            reader.onload = () => {
              museumnews.value[index].image = reader.result as string;
            };
            reader.readAsDataURL(file);
          }
        };

        const startEditing = (index: number, field: 'title' | 'description' | 'image') => {
          switch (field) {
            case 'title': {isEditTitle.value[index] = true};
            case 'description': {isEditDescription.value[index] = true};
            case 'image': {isEditImage.value[index] = true};
          }
        }

        const stopEditing = (index: number, field: 'title' | 'description' | 'image') => {
          switch (field) {
            case 'title': {isEditTitle.value[index] = false};
            case 'description': {isEditDescription.value[index] = false};
            case 'image': {isEditImage.value[index] = false};
          }
        }

        onMounted(NewsFetch);

        return {museumnews, newsDelete, newsUpdate,
          isEditTitle, isEditDescription, isEditImage, 
          updateImage, startEditing, stopEditing
        };
    }
}

</script>