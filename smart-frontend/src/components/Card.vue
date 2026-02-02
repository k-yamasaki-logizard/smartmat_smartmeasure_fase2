<script setup lang="ts">

import { computed } from 'vue'

const props = withDefaults(defineProps<{
    backgroundColor?: "white" | "gray"
}>(), {
    backgroundColor: 'white'
})

const emit = defineEmits<{
    (e: 'click'): void
}>()

const backgroundColorClass = computed(() => {
    if(props.backgroundColor === "white") {
        return "bg-white"
    }
    return "bg-gray-300"
})

</script>

<template>
    <button class="text-left flex flex-col gap-y-2 w-full border border-gray-300 rounded-lg p-4" :class="[backgroundColorClass]" @click="emit('click')">
        <slot name="content" />
    </button>
</template>