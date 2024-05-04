<template>
    <div class="container">
        <h2>Додати експонат</h2>
        <hr>
        <div class="input-fields">
            <input type="number" placeholder="Id" v-model="ExhibitId">
            <input type="text" placeholder="Title" v-model="ExhibitTitle">
            <input type="text" placeholder="Description" v-model="ExhibitDesc">
            <input type="File" placeholder="Image" @change="FileUpload" >
            
            <button @click="AddExhibit">Додати</button>
        </div>
        <span class="error" v-if="errorMessage"> {{ errorMessage }}</span>
        <span class="error" v-if="successMessage"> {{ successMessage }}</span>
    </div>    
</template>

<script lang="ts">
import { apiClient } from '../../apiClient';

export default {
    data() {
       return {
        ExhibitId: 0,
        ExhibitTitle: '',
        ExhibitDesc: '',
        ExhibitImage: null as File | null,
        errorMessage: '',
        successMessage: ''
       }
    }, 
    methods: {
        FileUpload(event: Event) {
            const file = (event.target as HTMLInputElement).files?.[0];
            if (file) {
                this.ExhibitImage = file;
            }
        },

        async AddExhibit() {

            let formData = new FormData();

            formData.append('Id', this.ExhibitId.toString());
            formData.append('Title', this.ExhibitTitle);
            formData.append('Description', this.ExhibitDesc);
            if (this.ExhibitImage) {
                formData.append('Image', this.ExhibitImage);
            }

            if (this.ExhibitId === 0 || this.ExhibitId < 0) {
                this.errorMessage = 'Неправильний формат ідентифікатора';
                return;
            }

            try {   
            const response = await apiClient.post('/Create/CreateExhibit', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            if (response.status === 200) {
                this.successMessage = "Експонат успішно додано";
            }
        }
        catch (error) {
            console.error(error);
            this.errorMessage = 'Виникла помилка при додаванні експонату';
        }
        }
    }

}
</script>