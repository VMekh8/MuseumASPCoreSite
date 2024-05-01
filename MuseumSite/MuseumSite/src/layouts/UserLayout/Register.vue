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
                <input type="tel" id="phonenumber" placeholder="Phone Number" v-model="phoneNumber">
                <input type="text" id="firsname" placeholder="*Firstname" v-model="firstName">
                <input type="text" id="lastname" placeholder="*Lastname" v-model="lastName">
            </div>
            

            <p v-if="error" class="error">{{ error }}</p>
            <p v-if="success" class="success">Реєстрація пройшла успішно</p>

            <div class="reg-offer">
                <p>Маєте акаунт? </p>
                <RouterLink to="/login">Авторизація</RouterLink>
            </div>
            <button type="submit" v-on:click="Register">Зареєструватися</button>
        </div>  
    </div>
</template>

<script>
import { apiClient } from '../../apiClient';
export default {
    data() {
        return {
            id: 'id',
            email: '',
            password: '',
            confirmPassword: '',
            phoneNumber: '',
            firstName: '',
            lastName: '',
            error: '',
            success: ''
        }
    },
    methods: {
        async Register() {

            if (!this.email || !this.password || !this.confirmPassword
                || !this.lastName || !this.firstName) {
                    this.error = "Обов'язкові поля мають бути заповнені";
                    return;
                }

            if (this.password !== this.confirmPassword) {
                this.error = "Паролі не співпадають";
                return;
            }
            
            try {

            
            let formData = new FormData();
            
            formData.append('id', this.id);
            formData.append('email', this.email);
            formData.append('password', this.password);
            formData.append('phoneNumber', this.phoneNumber);
            formData.append('firstname', this.firstName);
            formData.append('lastname', this.lastName);
            
            const response = await apiClient.post('/Account/Register', formData, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.status === 200) {
                this.success = "Реєстрація пройшла успішно"
            }
        }
        catch (error) {
            this.error = "Помилка реєстрації";
            console.log(error);
        }
        }
    }
}
</script>