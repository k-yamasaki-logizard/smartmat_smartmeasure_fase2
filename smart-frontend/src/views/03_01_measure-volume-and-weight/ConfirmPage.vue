<script setup lang="ts">
import ConfirmPage from '@/pages/ConfirmPage.vue'

/**
 * 画面ID:03_01_05
 * 商品マスタ登録/更新 確認（容積・重量）
 */
</script>

<template>
  <ConfirmPage
    backTo="/update/volume-and-weight/measure-weight"
    finishTo="/update/volume-and-weight/finish"
    displayMode="volume-and-weight"
  />
</template>
