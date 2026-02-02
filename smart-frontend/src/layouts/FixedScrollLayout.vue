<script setup lang="ts">
const props = withDefaults(defineProps<{
    class?: string
}>(), {
    class: '',
});
</script>

<template>
    <div class="flex flex-col flex-1 min-h-0 w-full" :class="class">
        <div class="flex-shrink-0">
            <slot name="fixed" />
        </div>
        <div class="flex-1 min-h-0 overflow-y-auto overflow-x-hidden pb-20 scrollbar-hide">
            <slot name="scrollable" />
        </div>
    </div>
</template>