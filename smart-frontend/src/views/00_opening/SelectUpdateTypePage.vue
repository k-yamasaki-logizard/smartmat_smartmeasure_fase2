<script setup lang="ts">
    import Title from '@/components/Title.vue';
    import LinkButton from '@/components/LinkButton.vue';
    import BackButton from '@/components/BackButton.vue';

    /**
     * 画面ID:03_00_01
     * 容積・重量 商品マスタ 登録/更新
     */
</script>

<template>
    <Title text="容積・重量 商品マスタ 登録/更新">
        <BackButton to="/" />
    </Title>
    <LinkButton to="/update/volume-and-weight/barcode-scan" >
        ①容積+重量測定
    </LinkButton>
    <LinkButton to="" >
        ②容積測定
    </LinkButton>
    <LinkButton to="" >
        ③重量測定
    </LinkButton>
</template>