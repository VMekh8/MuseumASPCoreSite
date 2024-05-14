<template>
    <div class="container">
        <div class="head">
            <h2>Новини музею</h2>
            <router-link to="/controlpanel/addmuseumnews">Додати новину</router-link>

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
                  <tr v-for="(news, index) in museumnews" :key="news.id">
                    <td>{{ news.id }}</td>
                    <td @dblclick="startEditing(index, 'title')">
                      <span v-if="!isEditTitle[index]">{{ news.title }}</span>
                      <input v-else v-model="news.title" @blur="stopEditing(index, 'title')" />
                    </td>
                    <td @dblclick="startEditing(index, 'description')">
                      <span v-if="!isEditDescription[index]">{{ news.description }}</span>
                      <input v-else v-model="news.description" @blur="stopEditing(index, 'description')" />
                    </td>
                    <td @dblclick="startEditing(index, 'image')">
                      <img v-if="!isEditImage[index]" :src="'data:;base64,' + news.image" />
                      <input v-else type="file" @change="updateImage(index, $event)" />
                    </td>
                    <td>
                      <button @click="newsUpdate(news.id)">Редагувати</button>
                      <button @click="newsDelete(news.id)">Видалити</button>
                    </td>
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
        const isEditTitle = ref<boolean[]>([]);
        const isEditDescription = ref<boolean[]>([]);
        const isEditImage = ref<boolean[]>([]);

        const NewsFetch = async () => {

            const response = await apiClient.get('/Client/GetAllNews');

            museumnews.value = response.data.map((news: any) => new MuseumNewsResponce(
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
