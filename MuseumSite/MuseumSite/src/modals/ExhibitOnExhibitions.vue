<template>
    <b-modal v-model="visible" title="Взаємодія експонатів з виставкою" hide-footer no-close-on-backdrop>
      <div class="mb-3">
        <b-input-group size="sm" class="mb-3" prepend="Назва виставки:">
          <b-form-input v-model="exhibitionName"></b-form-input>
          <b-input-group-append>
            <b-button size="sm" text="Button" variant="primary" @click="fetchExhibits">Знайти</b-button>
          </b-input-group-append>
        </b-input-group>
      </div>
      <div class="mb-3" v-for="exhibit in exhibits" :key="exhibit.id">
        <h3>{{ exhibit.title }}</h3>
        <p>{{ exhibit.description }}</p>
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
  
      const exhibitionName = ref('');
      const exhibits = ref<ExhibitResponse[]>([]);
  
      const fetchExhibits = async () => {
        if (!exhibitionName.value) {
          exhibitionName.value = 'Це поле не може бути порожнім';
          return;
        }
  
        try {
          const formData = new FormData();
          formData.append('name', exhibitionName.value);
          const response = await apiClient.get('/Exhibition/GetExhibitsOnExhibitions', {
            data: formData,
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          });
  
          if (response.status === 200) {
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