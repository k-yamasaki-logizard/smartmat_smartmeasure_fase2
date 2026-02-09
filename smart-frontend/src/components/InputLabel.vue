<script setup lang="ts">
const props = withDefaults(defineProps<{
    label: string
}>(), {
    label: ''
})
</script>

<template>
    <div class="flex items-center">
        <label class="text-[#4caf50] whitespace-nowrap">{{ props.label }}</label>
    </div>
</template>