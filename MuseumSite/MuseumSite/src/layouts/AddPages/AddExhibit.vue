<template>
    <div class="container exhibit-form card">
        <h2 class="add-exhibit-text">Додати експонат</h2>
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exhibitId" class="input-label">Ідентифікатор експонату</label>
                        <input type="number" id="exhibitId" v-model="ExhibitId" class="form-control" placeholder="Id" />
                    </div>
                    <div class="form-group">
                        <label for="exhibitTitle" class="input-label">Назва експонату</label>
                        <input type="text" v-model="ExhibitTitle" class="form-control" placeholder="Title" />
                    </div>
                    <div class="form-group">
                        <label for="exhibitDesc" class="input-label">Опис експонату</label>
                        <b-form-textarea type="text" v-model="ExhibitDesc" class="form-control" rows="3" max-rows="8" placeholder="Description"></b-form-textarea>
                    </div>
                    <div class="form-group">
                        <label for="exhibitImage" class="input-label">Зображення експонату</label>
                        <input type="file" @change="FileUpload" class="form-control" placeholder="Choose the image" />
                    </div>
                    <div class="form-group">
                        <button type="submit" @click="AddExhibit" class="btn btn-primary">Додати</button>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="text-center">
                        <img src="../../img/art-exhibition.png" alt="exhibitImage" class="img-thumbnail" style="width: 65%;" />
                    </div>
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

<style>
.add-exhibit-text {
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