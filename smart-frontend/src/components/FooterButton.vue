<script setup lang="ts">
import { computed } from 'vue'

const props = withDefaults(
  defineProps<{
    position?: "1" | "2" | "3"
    variant?: 'primary' | 'secondary'
    text?: string
  }>(),
  {
    position: "1",
    variant: 'secondary',
    text: '',
  }
)

const emit = defineEmits<{
  (e: 'click'): void
}>();

const colStartClass = computed(() => {
  if(props.position === "1") {
    return 'col-start-1'
  }
  if(props.position === "2") {
    return 'col-start-2'
  }
  return 'col-start-3';
});

const variantClass = computed(() => {
    if(props.variant === 'primary') {
        return 'bg-blue-600 text-white border-none hover:bg-blue-700'
    }
    return 'bg-white text-neutral-900 border border-neutral-700 hover:bg-gray-100'
});

</script>

<template>
  <button
    type="button"
    class="inline-flex p-2 items-center justify-center text-base font-[inherit] cursor-pointer rounded-lg transition-colors transition-shadow duration-200 whitespace-nowrap"
    :class="[variantClass, colStartClass]"
    @click="emit('click')"
  >
    <span class="block">
      <slot>{{ text }}</slot>
    </span>
  </button>
</template>
