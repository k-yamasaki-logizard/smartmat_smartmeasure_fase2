<script setup lang="ts">
/**
 * 画面全体を覆うローディングオーバーレイ
 * loading ストアの isLoading が true のとき表示。背面の操作はブロックする。
 */
import { storeToRefs } from 'pinia'
import { useLoadingStore } from '@/stores/loading'

const loadingStore = useLoadingStore()
const { isLoading } = storeToRefs(loadingStore)
</script>

<template>
  <Transition name="loading-fade">
    <div
      v-if="isLoading"
      class="fixed inset-0 z-[2000] flex items-center justify-center bg-black/50"
      role="status"
      aria-busy="true"
      aria-label="読み込み中"
    >
      <div class="flex flex-col items-center gap-4">
        <div class="loading-spinner h-12 w-12 rounded-full border-4 border-neutral-200 border-t-neutral-800" />
        <span class="text-sm font-medium text-white">読み込み中...</span>
      </div>
    </div>
  </Transition>
</template>

<style scoped>
.loading-spinner {
  animation: loading-spin 0.8s linear infinite;
}
@keyframes loading-spin {
  to {
    transform: rotate(360deg);
  }
}
.loading-fade-enter-active,
.loading-fade-leave-active {
  transition: opacity 0.2s ease;
}
.loading-fade-enter-from,
.loading-fade-leave-to {
  opacity: 0;
}
</style>
