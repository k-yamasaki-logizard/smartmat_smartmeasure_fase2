<script setup lang="ts">
const props = withDefaults(
  defineProps<{
    text?: string
    class?: string
    disabled?: boolean
  }>(),
  {
    text: '重量測定開始',
    class: '',
  }
)

const emit = defineEmits<{
  (e: 'click'): void
}>()

function handleClick() {
  emit('click')
}
</script>

<template>
  <button
    type="button"
    :disabled="disabled"
    class="flex items-center justify-center text-base font-[inherit] font-medium text-neutral-900 rounded-lg border border-[#e8d48b] bg-[#faf0c8] shadow-[0_2px_4px_rgba(0,0,0,0.08)] cursor-pointer transition-colors transition-shadow duration-200 hover:bg-[#f5e6b3] hover:shadow-[0_2px_6px_rgba(0,0,0,0.12)]"
    @click="handleClick"
    :class="class"
  >
    <slot>{{ text }}</slot>
  </button>
</template>
