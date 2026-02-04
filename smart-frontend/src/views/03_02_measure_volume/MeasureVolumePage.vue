<script setup lang="ts">
import MeasureVolumePage from '@/pages/MeasureVolumePage.vue'

/**
 * 画面ID:03_02_02
 * 容積(3M)測定（容積のみ）
 */
</script>

<template>
  <MeasureVolumePage
    backTo="/update/volume/barcode-scan"
    nextTo="/update/volume/list"
    :updateMeasuredAt="true"
  />
</template>
