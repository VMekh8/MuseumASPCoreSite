<template>
    <b-modal v-model="visible" title="Взаємодія експонатів з виставкою" hide-footer no-close-on-backdrop>
      <div class="mb-3">
        <b-input-group size="sm" class="mb-3" prepend="Назва виставки:">
          <b-form-input v-model="exhibitionName" required></b-form-input>
          <b-input-group-append>
            <b-button size="sm" text="Button" variant="primary" @click="fetchExhibits">Знайти</b-button>
          </b-input-group-append>
        </b-input-group>
      </div>
      <div class="mb-3" v-for="exhibit in exhibits" :key="exhibit.id">
        <b-card no-body class="overflow-hidden" style="max-width: 540px;">
           <b-row no-gutters>
             <b-col md="6">
                <b-card-img :src="'data:;base64,' + exhibit.image" alt="Image" class="rounded-0"></b-card-img>
             </b-col>
             <b-col md="6">
               <b-card-body :title='exhibit.id + " " +exhibit.title'>
                 <b-card-text>
                   <p>{{ exhibit.description }}</p>
                 </b-card-text>
               </b-card-body>
             </b-col>
           </b-row>
         </b-card>
      </div>
      <div class="d-flex justify-content-end">
        <b-button variant="primary" @click="onOk">OK</b-button>
      </div>
    </b-modal>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, watch } from 'vue';
  import { apiClient } from '../apiClient';
  import { ExhibitResponse } from '../Models/Exhibit';
  
  export default defineComponent({
    name: 'ExhibitInteractionModal',
    props: {
      modelValue: { type: Boolean, required: true }
    },
    emits: ['update:modelValue', 'ok', 'cancel'],
    setup(props, { emit }) {
      const visible = ref(props.modelValue);
      watch(() => props.modelValue, (newVal) => {
        visible.value = newVal;
      });
      watch(visible, (newVal) => {
        emit('update:modelValue', newVal);
      });
  
        const exhibitionName = ref<string>('');
        const exhibits = ref<ExhibitResponse[]>([]);
  
      const fetchExhibits = async () => {
        if (!exhibitionName.value.trim()) {
          exhibitionName.value = 'Це поле не може бути порожнім';
          return;
        }
  
        try {
          const formData = new FormData();
          formData.append('ExhibitionName', exhibitionName.value);

          const response = await apiClient.get(`/Exhibition/GetExhibitsOnExhibitions?ExhibitionName=${exhibitionName.value}`);
  
          if (response.status === 200) {
            console.log(response.status);
            exhibits.value = response.data.map((exhibit: any) => new ExhibitResponse(
              exhibit.id,
              exhibit.title,
              exhibit.description,
              exhibit.image
            ));
          }
        } catch (error) {
          console.log(error);
        }
      };
  
      const onOk = () => {
        visible.value = false;
        emit('ok');
      };
  
      const onCancel = () => {
        emit('cancel');
        visible.value = false;
      };
  
      return { visible, exhibitionName, exhibits, fetchExhibits, onOk, onCancel };
    }
  });
  </script>