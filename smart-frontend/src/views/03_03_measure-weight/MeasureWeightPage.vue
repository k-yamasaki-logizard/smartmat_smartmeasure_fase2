<script setup lang="ts">
import MeasureWeightPage from '@/pages/MeasureWeightPage.vue'

/**
 * 画面ID:03_03_02
 * 重量測定（重量のみ）
 */
</script>

<template>
  <MeasureWeightPage
    backTo="/update/weight/barcode-scan"
    nextTo="/update/weight/barcode-scan"
    listTo="/update/weight/list"
    confirmTo="/update/weight/confirm"
  />
</template>
