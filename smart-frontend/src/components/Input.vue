<script setup lang="ts">
    import { computed } from 'vue'
    import type { FieldRule } from 'vant'

    const props = withDefaults(defineProps<{
        modelValue?: string
        label: string
        labelWidth?: string
        rules?: FieldRule[]
        class?: string
    }>(), {
        modelValue: '',
        labelWidth: "auto",
        rules: [],
        class: ''
    })

    const emit = defineEmits<{
        (e: 'update:modelValue', value: string): void
    }>()

    const innerValue = computed({
        get: () => props.modelValue ?? '',
        set: (val: string) => emit('update:modelValue', val)
    })

</script>

<template>
    <van-form ref="formInstance" class="w-full" :rules="rules" validate-trigger="onSubmit" :class="class">
        <div class="border-1 border-[#CCCCCC] rounded-lg w-full">
            <!-- BC -->
            <van-field v-model="innerValue" :rules="rules">
                <template #label>
                    <div class="flex items-center">
                        <label class="text-[#4caf50] whitespace-nowrap">{{ props.label }}</label>
                    </div>
                </template>
                <template #input>
                    <input
                        v-model="innerValue"
                        type="text"
                        class="border-b-2 border-[#4CAF50] bg-[#FFFF99] flex-1 focus:outline-none focus:border-b-orange-400"
                    >
                </template>
            </van-field>
        </div>
    </van-form>
</template>

<style scoped>
    :deep(.van-field__label) {
        width: v-bind(labelWidth);
    }
</style>