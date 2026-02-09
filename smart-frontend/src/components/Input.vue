<script setup lang="ts">
    import { ref, onMounted, computed } from 'vue'

    const props = withDefaults(defineProps<{
        autofocus?: boolean,
        modelValue?: string,
    }>(), {
        autofocus: false,
        modelValue: '',
    })

    const emit = defineEmits<{
        (e: 'update:modelValue', value: string): void
    }>()

    const innerValue = computed({
        get: () => props.modelValue ?? '',
        set: (val: string) => emit('update:modelValue', val)
    })

    const inputRef = ref<HTMLInputElement | null>(null)
    
    onMounted(() => {
        if (props.autofocus) {
            inputRef.value?.focus()
        }
    })
</script>

<template>
    <input
        ref="inputRef"
        v-model="innerValue"
        type="text"
        class="border-b-2 border-[#4CAF50] bg-[#FFFF99] flex-1 focus:outline-none focus:border-b-orange-400"
    >
</template>