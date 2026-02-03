<script setup lang="ts">
import { computed } from 'vue'

/**
 * 共通セレクトボックス
 * ラベル + options でドロップダウンを表示。v-model で双方向バインド。
 */
export interface SelectOption {
  value: string
  label: string
}

const props = withDefaults(
  defineProps<{
    modelValue: string
    options: SelectOption[]
    label?: string
    class?: string
  }>(),
  {
    label: '',
    class: '',
  }
)

const optionList = computed<SelectOption[]>(() => props.options)

const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void
}>()

function onInput(event: Event): void {
  const target = event.target as HTMLSelectElement
  emit('update:modelValue', target.value)
}
</script>

<template>
  <div class="w-full" :class="props.class">
    <label v-if="props.label" class="mb-1 block text-sm font-medium text-neutral-900">
      <span class="text-black">■</span>{{ props.label }}
    </label>
    <select
      :value="props.modelValue"
      class="w-full rounded-lg border border-neutral-300 bg-white py-2 pl-3 pr-8 text-neutral-900 appearance-none focus:outline-none focus:ring-2 focus:ring-orange/30 focus:border-orange/50"
      @input="onInput"
    >
      <option
        v-for="opt in optionList"
        :key="opt.value"
        :value="opt.value"
      >
        {{ opt.label }}
      </option>
    </select>
  </div>
</template>

<style scoped>
select {
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' viewBox='0 0 12 12'%3E%3Cpath fill='%23000' d='M6 8L1 3h10z'/%3E%3C/svg%3E");
  background-repeat: no-repeat;
  background-position: right 0.75rem center;
}
</style>
