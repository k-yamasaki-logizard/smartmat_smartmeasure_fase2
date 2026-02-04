<script setup lang="ts">
import MeasureWeightPage from '@/pages/MeasureWeightPage.vue'

/**
 * 画面ID:03_01_04
 * 重量測定（容積・重量フロー）
 */
</script>

<template>
  <MeasureWeightPage
    backTo="/update/volume-and-weight/measure-volume"
    nextTo="/update/volume-and-weight/barcode-scan"
    listTo="/update/volume-and-weight/list"
    confirmTo="/update/volume-and-weight/confirm"
  />
</template>
