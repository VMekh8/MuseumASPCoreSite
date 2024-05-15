<template>
    <b-modal v-model="visible" title="Відображення експонатів на виставці" hide-footer no-close-on-backdrop>
      <div class="mb-3">
        <b-input-group size="sm" class="mb-3" prepend="Назва виставки:">
          <b-form-input v-model="ExhibitionName"></b-form-input>
          <b-input-group-append>
            <b-button size="sm" text="Button" variant="primary" @click="ExhibitsFetch">Знайти</b-button>
          </b-input-group-append>
        </b-input-group>
      </div>
      <div class="mb-3" v-for="item in Exhibits" :key="item.id">
        <h3>{{ item.title }}</h3>
        <p>{{ item.description }}</p>
      </div>
      <div class="d-flex justify-content-end">
        <b-button variant="primary" @click="onOk">OK</b-button>
      </div>
    </b-modal>
  </template>
  
  <script lang="ts">
  import { apiClient } from '../apiClient';
  import { ExhibitResponce } from '../Models/Exhibit';
  
  export default {
    data() {
      return {
        ExhibitionName: '',
        Exhibits: null,
        visible: false,
      }
    },
    methods: {
      async ExhibitsFetch() {
        if (!this.ExhibitionName) {
          this.ExhibitionName = 'Це поле не може бути порожнім';
          return;
        }
  
        try {
          const formData = new FormData();
          formData.append('name', this.ExhibitionName);
          const response = await apiClient.get('/Exhibition/GetExhibitsOnExhibitions', {
            data: formData,
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          });
  
          if (response.status === 200) {
            this.Exhibits = response.data.map((exhibit: any) => new ExhibitResponce(
              exhibit.id,
              exhibit.title,
              exhibit.description,
              exhibit.image,
            ));
            console.log(this.Exhibits);
          }
        } catch (error) {
          console.log(error)
        }
      },
      onOk() {
        this.visible = false;
        this.$emit('close');
      }
    }
  }
  </script>