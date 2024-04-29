<template>
    <div>
      <button @click="submitForm">Submit</button>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    data() {
      return {
        email: 'string@www.www',
        password: 'stringA1_'
      }
    },  
    methods: {
      async submitForm() {
        let formData = new FormData();
  
        formData.append('email', this.email);
        formData.append('password', this.password);
  
        try {
          const response = await axios.post('https://localhost:7105/Account/Login', formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          });
          console.log(response.data);
        } catch (error) {
          console.error(error);
        }
      }
    }
  }
  </script>
  