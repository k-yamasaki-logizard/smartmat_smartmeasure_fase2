<script setup lang="ts">
import Header from '@/components/Header.vue';
import { useRouter } from 'vue-router';

/**
 * ContentWrapper レイアウト
 * 固定ヘッダー + スクロール可能コンテンツ
 * @param ownerName 荷主名
 * @param areaName 倉庫名
 */
const props = withDefaults(defineProps<{
  ownerName?: string
  areaName?: string
  class?: string
}>(), {
  ownerName: '',
  areaName: '',
  class: '',
})
</script>

<template>
  <div class="flex flex-col flex-1 min-h-0 w-full h-screen">
    <!-- ヘッダ部 -->
    <Header :ownerName="props.ownerName" :areaName="props.areaName" class="mb-4" />
    <!-- コンテンツ部 -->
    <div class="flex flex-1 min-h-0 flex-col my-0 mx-4">
      <slot />
    </div>
  </div>
</template>

<style>
.scrollbar-hide {
  -webkit-appearance: none;
  scrollbar-width: none;  /* Firefox */
}
.scrollbar-hide::-webkit-scrollbar {
  display: none;
}
</style>