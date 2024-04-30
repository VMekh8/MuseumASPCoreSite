<template>
    <div class="container">
        <div class="left">
            <img src="../../img/images.jpg" alt="">
        </div>
        <div class="right">
            <h1>Реєстрація</h1>
            <h2>Вітаємо! Пройдіть реєстрацію для продовження роботи з нами</h2>

                
            <div class="fields">
                <input type="email" id="email" placeholder="*Email" v-model="email">
                <input type="password" id="password" placeholder="*Password" v-model="password">
                <input type="password" id="confirmpassword" placeholder="*Confirm Password" v-model="confirmPassword">
            </div>

            <div class="userdata">
                <input type="email" id="phonenumber" placeholder="Phone Number" v-model="phoneNumber">
                <input type="email" id="email" placeholder="*Firstname" v-model="firstName">
                <input type="email" id="email" placeholder="*Lastname" v-model="lastName">
            </div>
            

            <p v-if="error" class="error">{{ error }}</p>

            <div class="reg-offer">
                <p>Маєте акаунт? </p>
                <RouterLink to="/login">Авторизація</RouterLink>
            </div>
            <button type="submit" v-on:click="Register">Зареєструватися</button>
        </div>  
    </div>
</template>

<script>
import axios from 'axios';
import { error } from 'console';

export default {
    data() {
        return {
            email: '',
            password: '',
            confirmPassword: '',
            phoneNumber: '',
            firstName: '',
            lastName: '',
            error: ''
        }
    },
    methods: {
        async Register() {

            if (!this.email || !this.password || !this.confirmPassword
                || !this.lastName || !this.firstName) {
                    this.error = "Обов'язкові поля мають бути заповнені"
                }
                
        }
    }
}
</script>