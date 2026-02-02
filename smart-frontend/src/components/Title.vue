<script setup lang="ts">
const props = withDefaults(
  defineProps<{
    text?: string
    class?: string
  }>(),
  {
    text: '',
    class: '',
  }
)
</script>

<template>
  <div class="w-full text-left flex" :class="class">
    <slot />
    <h1 class="m-0 text-base font-bold text-neutral-900 tracking-wide leading-8">{{ text }}</h1>
  </div>
</template>
