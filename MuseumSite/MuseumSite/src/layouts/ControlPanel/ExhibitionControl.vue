<template>
  <div class="container">
    <div class="head">
      <h2>Виставки</h2>
      <router-link to="/controlpanel/addexhibition" class="btn btn-success">Додати виставку</router-link>
    </div>
    <div class="table-responsive mt-4">
      <table class="table table-bordered table-hover">
        <thead class="thead-dark">
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
            <td @dblclick="startEditing(index, 'name')" class="edit-cell">
              <span v-if="!isEditName[index]">{{ exhibition.name }}</span>
              <input v-else type="text" v-model="exhibition.name" @blur="stopEditing(index, 'name')" class="form-control">
            </td>
            <td @dblclick="startEditing(index, 'description')" class="edit-cell">
              <span v-if="!isEditDesc[index]">{{ exhibition.description }}</span>
              <input v-else type="text" v-model="exhibition.description" @blur="stopEditing(index, 'description')" class="form-control">
            </td>
            <td @dblclick="startEditing(index, 'eventDate')" class="edit-cell">
              <span v-if="!isEditDate[index] && exhibition.EventDate">{{ FormatDate(exhibition.EventDate?.toString()) }}</span>
              <input v-else type="datetime-local" v-model="exhibition.EventDate" @blur="stopEditing(index, 'eventDate')" class="form-control">
            </td>
            <td @dblclick="startEditing(index, 'image')" class="edit-cell">
              <img v-if="!isEditImage[index] " :src="'data:;base64,' + exhibition.image" class="img-thumbnail" />
              <input v-else type="file" @change="updateImage(index, $event)" class="form-control-file" />
            </td>
            <td class="actions-cell">
              <button @click="updateExhibition(exhibition.id)" class="btn btn-warning m-1">Редагувати</button>
              <button @click="deleteExhibition(exhibition.id)" class="btn btn-danger m-1">Видалити</button>
            </td>
            <td class="actions-cell">
              <b-button @click="showModal2 = true" class="btn btn-info m-1">Ексопонати на виставці</b-button>
              <ExhibitInteractionModal v-model="showModal2" @ok="handleCancel"/>
              <b-button variant="success" @click="showModal = true" class="btn btn-primary m-1">Додати експонати</b-button>
              <ExhibitModal v-model="showModal" @ok="handleOkToAdd" @cancel="handleCancel"/>
              <b-button variant="danger" @click="showModal1 = true" class="btn btn-danger m-1">Видалити експонати</b-button>
              <ExhibitModal v-model="showModal1" @ok="handleOkToDelete" @cancel="handleCancel"/>
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
import ExhibitInteractionModal from '../../modals/ExhibitOnExhibitions.vue';

export default {
  components: {
    ExhibitModal,
    ExhibitInteractionModal
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
        if (exhibition.EventDate) {
          formData.append('EventDate', exhibition.EventDate.toString());
        }
        
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