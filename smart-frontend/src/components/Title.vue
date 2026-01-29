<script setup lang="ts">
withDefaults(
  defineProps<{
    text?: string
  }>(),
  {
    text: '',
  }
)
</script>

<template>
  <div class="w-full text-left flex">
    <slot />
    <h1 class="m-0 text-base font-bold text-neutral-900 tracking-wide leading-8">{{ text }}</h1>
  </div>
</template>
