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
              <span v-if="!isEditDate[index]">{{ FormatDate(exhibition.EventDate?.toString()) }}</span>
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
              <b-button @click="showModal2 = true">Ексопонати на виставці</b-button>
              <ExhibitOnExhibitions v-model="showModal2"
              @ok="handleCancel"/>
              <b-button variant="success" @click="showModal = true">Додати експонати</b-button>
              <ExhibitModal v-model="showModal"
              @ok="handleOkToAdd"
              @cancel="handleCancel"/>
              <b-button variant="danger" @click="showModal1 = true">Видалити експонати</b-button>
              <ExhibitModal v-model="showModal1"
              @ok="handleOkToDelete"
              @cancel="handleCancel"/>
            </td> 
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { ExhibitionResponse } from '../../Models/Exhibition';
import { apiClient } from '../../apiClient';
import moment from 'moment';
import ExhibitModal from '../../modals/ExhibitToExhibition.vue'
import ExhibitOnExhibitions from '../../modals/ExhibitOnExhibitions.vue';

export default {
  components: {
    ExhibitModal,
    ExhibitOnExhibitions
  },
  setup() {
    const exhibitions = ref<ExhibitionResponse[]>([]);

    const isEditName = ref<boolean[]>([]);
    const isEditDesc = ref<boolean[]>([]);
    const isEditDate = ref<boolean[]>([]);
    const isEditImage = ref<boolean[]>([]);

    const showModal = ref(false);
    const showModal1 = ref(false);
    const showModal2 = ref(false);

    const handleOkToAdd = async (payment: { number1: number, number2: number}) => {
      console.log(payment);

      const formData = new FormData();

      formData.append('exhibitionId', payment.number1.toString());
      formData.append('exhibitId', payment.number2.toString());
      try {
       const response = await apiClient.post('/Exhibition/AddExhibitToExhibition', formData, {
         headers: {
           'Content-Type': 'multipart/form-data'
         }
       });

       if (response.status === 200) {
         console.log(response.data);
       }
      }
      catch (error) {
        console.log(error);
      }
    }

    const handleOkToDelete = async (payment: {number1: number, number2: number}) => {

      console.log(payment);

      const formData = new FormData();

      formData.append('exhibitionId', payment.number1.toString());
      formData.append('exhibitId', payment.number2.toString());

      try {
        const response = await apiClient.delete('Exhibition/DeleteExhibitToExhibition', {
          data: formData,
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });

        if (response.status === 200) {
          console.log(response.data);
        }
      }
      catch (error) {
        console.log (error);
      }
    }

    const handleCancel = () => {
      console.log('Cancel operation');
      showModal.value = false;
    }

    const FormatDate = (value: string) => {
      if (value) {
                return moment(value).format('DD-MMM-YYYY HH:mm:ss')
            }
    } 

    const updateExhibition = async (id:number) => {
      
      const exhibition = exhibitions.value.find(ex => ex.id === id);

      if (exhibition) {
        let formData = new FormData();

        formData.append('Id', exhibition.id.toString());
        formData.append('Name', exhibition.name);
        formData.append('Description', exhibition.description);
        formData.append('EventDate', exhibition.EventDate.toString());
        
        let base64String = exhibition.image;
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
          
          const response = await apiClient.put(`/Edit/ExhibitionEdit/${id}`, formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          });

          if (response.status === 200) {
            console.log(response.status);
            await ExhibitionFetch();
          }
        }
        catch (error) {
          console.log(error);
        }

      }
    }

    const stopEditing = async (index: number, field: 'name' | 'description' | 'eventDate' | 'image') => {
      switch (field) {
            case 'name': {isEditName.value[index] = false};
            case 'description': {isEditDesc.value[index] = false};
            case 'eventDate' : {isEditDate.value[index] = false};
            case 'image': {isEditImage.value[index] = false};
          }
          await updateExhibition(exhibitions.value[index].id);
    }

    const startEditing = (index: number, field: 'name' | 'description' | 'eventDate' | 'image') => {
      switch (field) {
            case 'name': {isEditName.value[index] = true};
            case 'description': {isEditDesc.value[index] = true};
            case 'eventDate' : {isEditDate.value[index] = true};
            case 'image': {isEditImage.value[index] = true};
          }
    }

    const updateImage = (index: number, event: Event) => {
      const file = (event.target as HTMLInputElement).files?.[0];

      if (file) {
        const reader = new FileReader();

        reader.onload = () => {
          exhibitions.value[index].image = reader.result as string;
        };
        reader.readAsDataURL(file);
      }
    }

    const ExhibitionFetch = async () => {
      const response = await apiClient.get('/Client/GetAllExhibitions');
      console.log(response.data);
      exhibitions.value = response.data.map((exhibition: any) => new ExhibitionResponse(
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

    return { exhibitions, deleteExhibition, FormatDate,
      isEditName, isEditDesc, isEditDate, isEditImage,
      startEditing, stopEditing, updateExhibition, updateImage,
      showModal, showModal1, showModal2, handleOkToAdd, handleCancel, handleOkToDelete
     };
  }
}
</script>

<style>
.modal-backdrop {
  display: none !important;
}

.modal {
  z-index: 1050 !important;
}
</style>