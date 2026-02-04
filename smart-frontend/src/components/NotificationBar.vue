<script setup lang="ts">
/**
 * お知らせバー
 * notification ストアの message を表示。フッタの直上に固定し、タップで閉じる。画面はブロックしない。
 */
import { storeToRefs } from 'pinia'
import { useNotificationStore } from '@/stores/notification'

const notificationStore = useNotificationStore()
const { message } = storeToRefs(notificationStore)

function dismiss(): void {
  notificationStore.dismiss()
}
</script>

<template>
  <Transition name="notification-fade">
    <button
      v-if="message"
      type="button"
      class="fixed left-4 right-4 z-[1000] flex w-[calc(100%-2rem)] items-center justify-between rounded-lg border border-orange/50 bg-[#fefaf7] px-4 py-3 text-left text-sm text-neutral-900 shadow-[0_2px_8px_rgba(230,126,34,0.15)]"
      style="bottom: 5rem;"
      @click="dismiss"
    >
      <span class="min-w-0 flex-1 whitespace-pre-line">{{ message }}</span>
      <span class="ml-2 shrink-0 text-neutral-500">×</span>
    </button>
  </Transition>
</template>

<style scoped>
.notification-fade-enter-active,
.notification-fade-leave-active {
  transition: opacity 0.2s ease, transform 0.2s ease;
}
.notification-fade-enter-from,
.notification-fade-leave-to {
  opacity: 0;
  transform: translateY(0.5rem);
}
</style>
