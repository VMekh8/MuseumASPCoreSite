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
                <tr v-for="user in users" :key="user.id">
                  <td>{{ user.id }}</td>
                  <td>{{ user.email }}</td>
                  <td>{{ user.phonenumber }}</td>
                  <td>{{ user.firstname }}</td>
                  <td>{{ user.lastname }}</td>
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
import { UserResponce } from '../../Models/User'
import { apiClient } from '../../apiClient';

export default {
    setup() {

        const users = ref<UserResponce[]>([]);
        const roles = ref([]);

        const usersFetch = async () => {

            const response = await apiClient.get('/Admin/GetUsers')

            users.value = response.data.map((user: any) => new UserResponce(
                user.id,
                user.email,
                user.password,
                user.phoneNumber,
                user.firstname,
                user.lastname
            ));
            
        };

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

        return {users, roles, userDelete};
    }
}
</script>
