<template>
    <div class="container exhibit-form card">
        <h2 class="create-exhibition-text">Створити виставку</h2>
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exhibitionId" class="input-label">Ідентифікатор виставки</label>
                        <input type="number" id="exhibitionId" v-model="exhibitionId" class="form-control" placeholder="Id" />
                    </div>
                    <div class="form-group">
                        <label for="exhibitionName" class="input-label">Назва виставки</label>
                        <input type="text" v-model="exhibitionName" class="form-control" placeholder="Name" />
                    </div>
                    <div class="form-group">
                        <label for="eventDate" class="input-label">Дата та час події</label>
                        <input type="datetime-local" v-model="eventDate" class="form-control" />
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <label for="exhibitionDesc" class="input-label">Опис виставки</label>
                        <input type="text" v-model="exhibitionDesc" class="form-control" placeholder="Description" />
                    </div>
                    <div class="form-group">
                        <label for="exhibitionImage" class="input-label">Зображення виставки</label>
                        <input type="file" @change="FileUpload" class="form-control" placeholder="Choose the image" />
                    </div>
                    <div class="form-group">
                        <button type="submit" @click="AddExhibition" class="btn btn-primary">Створити виставку</button>
                    </div>
                </div>
                <div class="col-md-3">
                    <img src="../../img/art-show.png" alt=""  class="img-thumbnail">
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <span class="error" v-if="errorMessage">{{ errorMessage }}</span>
                    <span class="error" v-if="successMessage">{{ successMessage }}</span>
                </div>
            </div>
        </div>
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
                    formData.append('EventDate', formatedDate.toString());
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

<style>
.create-exhibition-text {
    font-size: 28px;
    font-weight: bold;
    color: #3f3f3f;
    margin: 5% 0% 0% 5%;
}

.input-label {
    font-size: 18px;
    font-weight: 500;
    color: #3f3f3f;
}

.exhibit-form {
    margin-top: 5%;
    margin-bottom: 5%;
    width: 70%;
}

.card-body {
    padding: 3rem;
}

.form-group {
    margin-bottom: 1.5rem;
}

.btn-primary {
    width: 100%;
}

.error {
    display: block;
    margin-top: 0.5rem;
    color: red;
}

.card {
    border: 1px solid rgba(0,0,0,.125);
    border-radius: .25rem;
    box-shadow: 0 0.5rem 1rem rgba(0,0,0,.15);
}

.img-thumbnail {
    border: none;
}

.text-center {
    margin-bottom: 1rem;
}
</style>