<template>
    <div class="container">
        <div class="head">
            <h2>Користувачі</h2>
            <router-link to="#">Додати користувача</router-link>

        </div>
        
        <div class="table">
            <table>
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Email</th>
                  <th>Номер телефону</th>
                  <th>Ім'я</th>
                  <th>Прізвище</th>
                  <th>Ролі корисувача</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(user, index) in users" :key="user.id">
                    <td>{{ user.id }}</td>
                    <td @dblclick="startEditing(index, 'email')">
                      <span v-if="!isEditEmail[index]">{{ user.email }}</span>
                      <input v-else v-model="user.email" @blur="stopEditing(index, 'email')" />
                    </td>
                    <td @dblclick="startEditing(index, 'phonenumber')">
                      <span v-if="!isEditPhone[index]">{{ user.phonenumber }}</span>
                      <input v-else v-model="user.phonenumber" @blur="stopEditing(index, 'phonenumber')" />
                    </td>
                    <td @dblclick="startEditing(index, 'firstname')">
                      <span v-if="!isEditFirstName[index]">{{ user.firstname }}</span>
                      <input v-else v-model="user.firstname" @blur="stopEditing(index, 'firstname')" />
                    </td>
                    <td @dblclick="startEditing(index, 'lastname')">
                      <span v-if="!isEditLastName[index]">{{ user.lastname }}</span>
                      <input v-else v-model="user.lastname" @blur="stopEditing(index, 'lastname')" />
                    </td>
                  <td v-for="role in roles[user.email]" :key="role">{{role}}</td>
                  <td>
                    <button>Редагувати</button>
                    <button @click="userDelete(user.id)">Видалити</button>
                    </td>
                </tr>
              </tbody>
            </table>

        </div>

    </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { UserResponse } from '../../Models/User'
import { apiClient } from '../../apiClient';

export default {
    setup() {

        const users = ref<UserResponse[]>([]);
        const roles = ref([]);

        const isEditEmail = ref<boolean[]>([]);
        const isEditPhone = ref<boolean[]>([]);
        const isEditFirstName = ref<boolean[]>([]);
        const isEditLastName = ref<boolean[]>([]);
        
        const usersFetch = async () => {

            const response = await apiClient.get('/Admin/GetUsers')

            users.value = response.data.map((user: any) => new UserResponse(
                user.id,
                user.email,
                user.password,
                user.phoneNumber,
                user.firstname,
                user.lastname
            ));
            
        };

        const startEditing = (index: number, field: string) => {
            switch (field) {
                case 'email': {isEditEmail.value[index] = true};
                case 'phonenumber': {isEditPhone.value[index] = true};
                case 'firstname': {isEditFirstName.value[index] = true};
                case 'lastname': {isEditLastName.value[index] = true};
            }
        }

        const stopEditing = async (index: number, field: string) => {
            switch (field) {
                case 'email': {isEditEmail.value[index] = false};
                case 'phonenumber': {isEditPhone.value[index] = false};
                case 'firstname': {isEditFirstName.value[index] = false};
                case 'lastname': {isEditLastName.value[index] = false};
            }
            await updateUserData(users.value[index].id);
        }

        const updateUserData = async (id:string) => {
            
            const user = users.value.find(u => u.id === id);
            
            const formData = new FormData();
            if (user) {

                formData.append('Id', user.id);
                formData.append('Email', user.email);
                formData.append('Password', user.password);
                if (user.phonenumber) {
                    formData.append('PhoneNumber', user.phonenumber);
                }
                formData.append('Firstname', user.firstname);
                formData.append('Lastname', user.lastname);
            }

            try {

                const response = await apiClient.put('/Admin/EditUser', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });

                if (response.status === 200) {
                    console.log(response.status);
                    await usersFetch();
                }
            }
            catch (error) {
                console.log(error);
            }
        }

        const rolesFetch = async () => {

            for (const user of users.value) {
                if (user.email) {
                    const response = await apiClient.get('/UserRoles/GetUserRole?email=' + user.email);
                    roles.value[user.email] = response.data;
                }
            }
            
        }

        const userDelete = async (userid: string) => {
            await apiClient.delete('/Admin/DeleteUser?userId=' + userid);
            users.value = users.value.filter(u => u.id !== userid);
        }

        onMounted(async () => {
            await usersFetch();
            await rolesFetch();
        });

        return {users, roles, userDelete,
            isEditEmail, isEditPhone, isEditFirstName, isEditLastName,
            startEditing, stopEditing, updateUserData
        };
    }
}
</script>
