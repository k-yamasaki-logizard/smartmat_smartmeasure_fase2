<script setup lang="ts">
    import { ref } from 'vue';

    type WeatherForecast = {
        date: string;
        temperatureC: number;
        summary: string;
        temperatureF: number;
    }

    const apiTest = ref<WeatherForecast[]>([]);

    const getHelloApi = async () => {
        const response = await fetch('/api/weatherforecast');
        apiTest.value = await response.json();
    };


</script>

<template>
    <div>
        <h1>API TEST!</h1>
        <button @click="getHelloApi">テストする</button>
        
        <table v-if="apiTest.length > 0">
            <thead>
                <tr>
                    <th>日付</th>
                    <th>気温</th>
                    <th>天気</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in apiTest as WeatherForecast[]" :key="item.date">
                    <td>{{ item.date }}</td>
                    <td>{{ item.temperatureC }}</td>
                    <td>{{ item.summary }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>