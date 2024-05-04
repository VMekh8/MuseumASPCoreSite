<template>
<div class="container">
    <h2>Додати новину музею</h2>
    <hr>
    <div class="fields">
        <input type="number" v-model="newsid" placeholder="Id">
        <input type="text" v-model="newsTitle" placeholder="Title">
        <input type="text" v-model="newsDescription" placeholder="Description">
        <input type="file" @change="ChangeFileUpload" placeholder="Choose the image">
        <button type="submit" @click="CreateMuseumNews">Додати</button>
    </div>
    <span class="errorMess" v-if="ErrorMessage">{{ ErrorMessage }}</span>
</div>
</template>

<script lang="ts">
import { apiClient } from '../../apiClient';
export default {
    data() {
        return {
            newsid: 0,
            newsTitle: '',
            newsDescription: '',
            newsImage: null as File | null,
            ErrorMessage: '',
        }
    },
    methods: {
        ChangeFileUpload(event: Event) {
            const file = (event.target as HTMLInputElement).files?.[0];
            if (file) {
                this.newsImage = file;
            }
        },

        async CreateMuseumNews() {
           let formData = new FormData();
           formData.append('Id', this.newsid.toString());
           formData.append('Title', this.newsTitle);
           formData.append('Description', this.newsDescription);
           if (this.newsImage) {
            formData.append('Image', this.newsImage);
           }


            if (this.newsid === 0 || this.newsid < 0) {
                this.ErrorMessage = "Неприпустиме значенння ідентифікатора новини";
                return;
            }
            try {
                const response = await apiClient.post('/Create/CreateNews', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });  

                if (response.status === 200) {
                    console.log(123)
                }
            
            }
            catch (error) {
                this.ErrorMessage = 'Виникла помилка при додаванні елементу'
                console.error(error);
            }
        }
    }
}
</script>
