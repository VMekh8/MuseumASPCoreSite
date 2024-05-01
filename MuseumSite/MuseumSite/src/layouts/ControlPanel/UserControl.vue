<template>
    <div class="container">
        <div class="head">
            <h2>Експонати</h2>
            <router-link to="#">Додати експонат</router-link>

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
                </tr>
              </thead>
              <tbody>
                <tr v-for="exhibit in exhibits" :key="exhibit.id">
                  <td>{{ exhibit.id }}</td>
                  <td>{{ exhibit.title }}</td>
                  <td>{{ exhibit.description }}</td>
                  <td><img :src="'data:;base64,' + exhibit.image" /></td>
                  <td><button>Редагувати</button>
                  <button>Видалити</button></td>
                </tr>
              </tbody>
            </table>

        </div>

    </div>
</template>