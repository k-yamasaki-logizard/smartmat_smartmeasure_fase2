<script setup lang="ts">
withDefaults(
  defineProps<{
    to?: string
  }>(),
  { to: '/' }
)
</script>

<template>
  <router-link
    :to="to"
    class="inline-flex items-center justify-center w-8 h-8 shrink-0 bg-white border border-gray-200 rounded-lg shadow-[0_1px_3px_rgba(0,0,0,0.08)] no-underline text-neutral-900 transition-colors transition-shadow duration-200 hover:bg-gray-100 hover:shadow-[0_2px_6px_rgba(0,0,0,0.1)]"
    aria-label="戻る"
  >
    <svg class="w-5 h-5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
      <path d="M19 12H5M12 19l-7-7 7-7" />
    </svg>
  </router-link>
</template>
