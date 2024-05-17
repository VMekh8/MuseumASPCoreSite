<template>
    <div class="container contact-form card">
        <h2 class="add-news-text">Додати новини музею</h2>
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="newsid" class="input-label">Ідентифікатор новини</label>
                        <input type="number" id="newsid" v-model="newsid" class="form-control" placeholder="Id" />
                    </div>
                    <div class="form-group">
                        <label for="newsid" class="input-label">Заголовок новини</label>
                        <input type="text" v-model="newsTitle" class="form-control" placeholder="Title" />
                    </div>
                    <div class="form-group">
                        <label for="newsid" class="input-label">Опис новини</label>
                        <b-form-textarea type="text" v-model="newsDescription" class="form-control"  rows="3" max-rows="8" placeholder="Description"></b-form-textarea>
                    </div>
                    <div class="form-group">
                        <label for="newsid" class="input-label">Зображення новини</label>
                        <input type="file" @change="ChangeFileUpload" class="form-control" placeholder="Choose the image" />
                    </div>
                    <div class="form-group">
                        <button type="submit" @click="CreateMuseumNews" class="btn btn-primary">Додати</button>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="text-center">
                        <img src="../../img/newspaper.png" alt="newsImage" class="img-thumbnail" style="width: 65%;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <span class="errorMess" v-if="ErrorMessage">{{ ErrorMessage }}</span>
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
            newsid: 0,
            newsTitle: '',
            newsDescription: '',
            newsImage: null as File | null,
            ErrorMessage: '',
            successMessage: ''
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
                    this.successMessage = "Новину успішно додано!";
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
<style>

.add-news-text {
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

.contact-form {
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

.errorMess,
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