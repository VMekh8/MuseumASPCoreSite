<template>
    <div class="container">
        <div class="left">
            <img src="../../img/images.jpg" alt="">
        </div>
        <div class="right">
            <h1>Авторизація</h1>
            <h2>Вітаємо з поверненням!
                Авторизуйтесь, будь ласка, 
                для продовження роботи</h2>

                
            <div class="fields">
                <input type="email" id="emailfield" placeholder="Email" v-model="emailfield">
                <input type="password" id="passwordfield" placeholder="Password" v-model="passwordfield">
            </div>
            

            <p v-if="error" class="error">{{ error }}</p>

            <div class="reg-offer">
                <p>Немає акаунту? </p>
                <router-link to="/register">Реєстрація</router-link>
            </div>

            <button type="submit" v-on:click="Login">Увійти</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import { inject } from 'vue';

export default {
    data() {
        return {
            emailfield: '',
            passwordfield: '',
            error: '',
            globalState: inject('globalState')
        }
    },
    methods: {
        async Login() {

            if (!this.emailfield || !this.passwordfield) {
                this.error = "Всі поля мають бути заповнені";
                return;
            }

            let formData = new FormData();
            formData.append('email', this.emailfield);
            formData.append('password', this.passwordfield);

            try {

                const responce = await axios.post('https://localhost:7105/Account/Login', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });

                if (responce.status === 200)
                {
                    console.log("Success");
                    axios.defaults.headers.common['Authorization'] = 'Bearer ' + responce.data.token;
                    this.globalState.userRoles = responce.data.roles;
                    console.log(this.globalState.userRoles);
                }
            }
            catch (error) {
                console.error(error);
                this.error = 'Помилка авторизації';
            }
        }
    }
}
</script>