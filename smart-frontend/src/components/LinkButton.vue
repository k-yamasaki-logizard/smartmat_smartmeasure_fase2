<script setup lang="ts">
withDefaults(
  defineProps<{
    to: string
    text?: string
    subtext?: string
    class?: string
  }>(),
  {
    text: '',
    subtext: '',
    class: '',
  }
)

const emit = defineEmits<{
  (e: 'click'): void
}>()

</script>

<template>
  <router-link
    :to="to"
    class="flex flex-col items-center justify-center w-full max-w-full py-4 px-0 bg-white border border-orange/50 rounded-lg shadow-[0_1px_3px_rgba(230,126,34,0.15)] transition-colors transition-shadow duration-200 no-underline hover:bg-[#fefaf7] hover:shadow-[0_2px_6px_rgba(230,126,34,0.2)]"
    :class="class"
    @click="emit('click')"
  >
    <span class="block text-base font-bold text-neutral-900 text-center no-underline">
      <slot>{{ text }}</slot>
    </span>
    <span class="block text-sm text-gray-300 text-center no-underline">
      <slot name="subtext">{{ subtext }}</slot>
    </span>
  </router-link>
</template>
