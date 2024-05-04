<template>
    <div class="container">
        <h2>Створити виставку</h2>
        <hr>
        <div class="input-fields">
            <input type="number" placeholder="Id" v-model="exhibitionId">
            <input type="text" placeholder="Name" v-model="exhibitionName">
            <input type="text" placeholder="Description" v-model="exhibitionDesc">
            <input type="datetime-local" v-model="eventDate">
            <input type="file" @change="FileUpload">

            <button @click="AddExhibition">Створити виставку</button>
        </div>

        <span class="errorMess" v-if="errorMessage">{{ errorMessage }}</span>
        <span class="error" v-if="successMessage"> {{ successMessage }}</span>
    </div>
</template>

<script lang="ts">
import { apiClient } from '../../apiClient';
import moment from 'moment';

export default {
    data() {
        return {
            exhibitionId: 0,
            exhibitionName: '',
            exhibitionDesc: '',
            eventDate: '',
            exhibitionImage: null as File | null,
            errorMessage: '',
            successMessage: ''
        }
    },
    methods: {
        format_date(value: string) {
            if (value) {
                return moment(value).format('DD-MMM-YYYY HH:mm:ss')
            }
        },

        FileUpload(event: Event) {
            const file = (event.target as HTMLInputElement).files?.[0];
            if (file) {
                this.exhibitionImage = file;
            }
        },

        async AddExhibition() {
            if (this.exhibitionId === 0 || this.exhibitionId < 0) {
                this.errorMessage = 'Неправильний формат ідентифікатора';
                return;
            }

            const currentDate = new Date();
            let formData = new FormData();

            if (this.eventDate && new Date(this.eventDate).getTime() >= currentDate.getTime())  {
                const formatedDate = this.format_date(this.eventDate);
                if (formatedDate) {
                    formData.append('eventDate', formatedDate.toString());
                }
            }
            else {
                this.errorMessage = 'Ви не можете створити виставку в минулому';
                return;
            }

            formData.append('Id', this.exhibitionId.toString());
            formData.append('Name', this.exhibitionName);
            formData.append('Description', this.exhibitionDesc);

            if (this.exhibitionImage) {
                formData.append('Image', this.exhibitionImage)
            }

            try {
                const response = await apiClient.post('/Create/CreateExhibition', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

                if (response.status === 200) {
                    this.successMessage = 'Додавання пройшло успішно';
                    return;
                }
            }
            catch (error) {
                console.error(error);
                this.errorMessage = 'Виникла помилка при створенні виставки';
                return;
            }
        }
    }
}
</script>
