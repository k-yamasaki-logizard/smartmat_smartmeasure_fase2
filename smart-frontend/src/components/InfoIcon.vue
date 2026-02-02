<script setup lang="ts">
const props = withDefaults(defineProps<{
    class?: string
}>(), {
    class: '',
});
</script>

<template>
    <button class="i-carbon-information-filled bg-[#ee752f] flex h-[80px] w-[80px] items-center justify-center" :class="props.class" />
</template>