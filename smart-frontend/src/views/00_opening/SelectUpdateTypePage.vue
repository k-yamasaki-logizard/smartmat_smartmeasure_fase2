<script setup lang="ts">
    import Title from '@/components/Title.vue';
    import LinkButton from '@/components/LinkButton.vue';
    import BackButton from '@/components/BackButton.vue';
    import { useMeasureStore } from '@/stores/measure';

    /**
     * 画面ID:03_00_01
     * 容積・重量 商品マスタ 登録/更新
     */
    const measureStore = useMeasureStore()
</script>

<template>
    <Title text="容積・重量 商品マスタ 登録/更新" class="mb-4">
        <BackButton to="/" />
    </Title>
    <LinkButton to="/update/volume-and-weight/barcode-scan" class="mb-4" @click="measureStore.initialize('volume-and-weight')">
        ①容積+重量測定
    </LinkButton>
    <LinkButton to="/update/volume/barcode-scan" class="mb-4" @click="measureStore.initialize('volume')">
        ②容積測定
    </LinkButton>
    <LinkButton to="/update/weight/barcode-scan" class="mb-4" @click="measureStore.initialize('weight')">
        ③重量測定
    </LinkButton>
</template>