<script setup lang="ts">
import Title from '@/components/Title.vue';
import LinkButton from '@/components/LinkButton.vue';
import BackButton from '@/components/BackButton.vue';

/**
 * 画面ID：04_01_01
 */
</script>

<template>
    <Title text="設定" class="mb-4">
        <BackButton to="/" />
    </Title>
    <LinkButton to="" class="mb-4">
        容積・重量 マスタ登録/更新
        <template #subtext>
            どのマスタ/項目に登録するか指定
        </template>
    </LinkButton>
    <LinkButton to="" class="mb-4">
        容積測定 縦/横 自動変更
        <template #subtext>
            縦/横の最長辺を、どちらかの方向に自動割当する
        </template>
    </LinkButton>
</template>