<template>
    <div class="container">
        <input type="email" id="email" v-model="email">
        <input type="password" id="password" v-model="password">
        <button type="submit" v-on:click="Login">Login</button>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            email: '',
            password: ''
        }
    },
    methods: {
        async Login() {
            try {
                const responce = await axios.post('https://localhost:7105/Account/Login', {
                    email: this.email,
                    password: this.password
                });

                if (responce.status === 200) {
                    console.log(responce.data.token);
                }
            }
            catch(error) {
                console.error(error);
            }
        }
    }
}
</script>