<script setup lang="ts">
withDefaults(
  defineProps<{
    consignor?: string
    warehouse?: string
    badgeLabel?: string
    badgeSublabel?: string
  }>(),
  {
    consignor: '',
    warehouse: '',
    badgeLabel: 'LZ',
    badgeSublabel: '容積/重量',
  }
)

const emit = defineEmits<{
  (e: 'click:badge'): void
}>()
</script>

<template>
  <div class="sticky top-0 flex items-center gap-4 w-full bg-white p-0 m-0 border-b border-black">
    <div @click="emit('click:badge')" class="bg-orange rounded-lg py-2 px-3 text-white text-center shrink-0">
      <span class="block text-xl font-semibold leading-tight">{{ badgeLabel }}</span>
      <span class="block text-sm leading-tight">{{ badgeSublabel }}</span>
    </div>
    <div class="text-neutral-900">
      <div class="text-base leading-normal">荷主: {{ consignor || 'XXXXXXXXXXXXX' }}</div>
      <div class="text-base leading-normal">倉庫: {{ warehouse || 'XXXXXXXXXXXXX' }}</div>
    </div>
  </div>
</template>
