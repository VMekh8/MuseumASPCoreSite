<template>
    <section class="exhibition-section" id="exhibition-section">
        <h2>Виставки музею!</h2>
        <p>Беріть безпосередню участь в житті музею.</p>

    <div class="exhibition-carousel w-100">
        <Carousel>
        <Slide v-for="item in exhibitions" :key="item.id">
            <div class="card">
              <div class="card-image">
                <img :src="'data:;base64, '+ item.image">
              </div>
              <div class="card-prop">
                <p class="card-meal-type" v-if="item.EventDate">{{ FormatDate(item.EventDate?.toString()) }}</p>
                <h2 class="card-title">{{ item.name }}</h2>
                <p class="card-body">{{item.description}}</p>
              </div>
            </div>
        </Slide>

        <template #addons>
          <Navigation />
          <Pagination />
        </template>
    </Carousel>

    </div>
    </section>
</template>
<script lang="ts">
import { ExhibitionResponse } from '../../Models/Exhibition';
import { apiClient } from '../../apiClient';
import { defineComponent, onMounted, ref } from 'vue'
import { Carousel, Navigation, Pagination, Slide } from 'vue3-carousel'
import moment from 'moment';


export default defineComponent({
    components: {
        Carousel,
        Navigation,
        Pagination,
        Slide
    },
    setup() {
        
        const exhibitions = ref<ExhibitionResponse[]>([]);

        const ExhibitionFetch = async () => {
            
            try {

                const response = await apiClient.get('/Client/GetAllExhibitions');

                if (response.status === 200) {
                    console.log(response.status);

                    exhibitions.value = response.data.map((e: any) => new ExhibitionResponse(
                        e.id,
                        e.name,
                        e.description,
                        e.date,
                        e.image,
                        e.exhibits
                    ));

                    console.log(exhibitions.value);
                }
            }
            catch(error) {
                console.log(error);
            }
        }

        const FormatDate = (value: string) => {
            if (value) {
                return moment(value).format('DD-MMM-YYYY HH:mm:ss')
            }
        } 

        onMounted(ExhibitionFetch);

        return {
            exhibitions, ExhibitionFetch, FormatDate
        }
        
    }
})
</script>

<style>

 
section {
    display: flex;
    align-items: center;
    flex-direction: column;
    padding: 65px 0 0;
    background-color: whitesmoke;

  }


section h2 {
  font-size: 5rem;
}

section > p {
  text-align: center;
  font-size: 2rem;
}
    .card{
  display:flex;
  flex-direction:column;
  width:400px;
  margin-bottom:60px;
}
.card>div{
  box-shadow:0 15px 20px 0 rgba(0,0,0,0.5);
}
.card-image{
  width:400px;
  height:250px;
  position: relative;
}
.card-image>img{
  width:100%;
  height:100%;
  object-fit:cover;
  object-position:center;
}
.card-prop{
  position: absolute;
  bottom: 0;
  left: 50%;
  transform: translateX(-50%);
  height:150px; 
  width:300px;
  background-color:#1D1C20;
  color:#fff;
  padding:20px;
  bottom: 20%;
}
.card-meal-type{
  font-style:italic;
}
.card-title{
  font-size:2.2rem;
  margin-bottom:20px;
  margin-top:5px;
}
.card-desc{
  font-size:1.25rem;
}
</style>
