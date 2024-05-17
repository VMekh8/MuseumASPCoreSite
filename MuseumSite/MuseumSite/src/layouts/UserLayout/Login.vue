<template>
    <section class="login-section">
        <main>
            <div class="left">
            <img src="../../img/husyatyn038.jpg" alt="">
        </div>
        <div class="right">
            <h1>Авторизація</h1>
            <h2>Вітаємо з поверненням!
                Авторизуйтесь, будь ласка, 
                для продовження роботи</h2>

                
            <div class="fields">
                <div class="log">
                    <input type="email" id="emailfield" placeholder="Email" v-model="emailfield">
                    <label for="emailfield">Логін:</label>
                </div>
                <div class="pass">
                    <input type="password" id="passwordfield" placeholder="Password" v-model="passwordfield">
                    <label for="passwordfield">Пароль:</label>
                </div>
            </div>
            

            <p v-if="error" class="error">{{ error }}</p>

            <div class="reg-offer">
                <p>Немає акаунту? </p>
                <router-link to="/register">Реєстрація</router-link>
            </div>
            <b-button variant="outline-secondary w-50 m-5 mx-auto" type="submit" v-on:click="Login">Увійти</b-button>
        </div> 
        </main> 
    </section>
</template>

<script lang="ts">
import router from '../../router';
import { apiClient } from '../../apiClient';
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

                const response = await apiClient.post('/Account/Login', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });

                if (response.status === 200)
                {
                    console.log("Success");
                    localStorage.setItem('authToken', response.data.token);
                    this.globalState.userRoles = response.data.roles;
                    console.log(this.globalState.userRoles);

                    await router.push('/').then(async () => await router.go(0));
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

<style>
    @import url('../../assets/account.css');
</style>