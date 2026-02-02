<script setup lang="ts">
import Title from '@/components/Title.vue';
import LinkButton from '@/components/LinkButton.vue';

/**
 * 画面ID:02_01_01
 */
</script>

<template>
    <Title text="HOME" class="mb-4" />
    <LinkButton to="/update" subtext="" class="mb-4">容積・重量 マスタ登録/更新</LinkButton>
    <LinkButton to="/settings" subtext="" class="mb-4">設定</LinkButton>
</template>