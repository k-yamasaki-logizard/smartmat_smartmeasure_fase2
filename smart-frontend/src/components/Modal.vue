<script setup lang="ts">
/**
 * 共通モーダル
 * Vant Popup + タイトル + 閉じるボタンをまとめたラッパー。中身はスロットで指定。
 */
import { Popup } from 'vant'

const props = withDefaults(
  defineProps<{
    show: boolean
    title: string
  }>(),
  { show: false, title: '' }
)

const emit = defineEmits<{
  (e: 'update:show', value: boolean): void
}>()

function close(): void {
  emit('update:show', false)
}

function onUpdateShow(value: boolean): void {
  emit('update:show', value)
}
</script>

<template>
  <Popup
    :show="show"
    position="center"
    round
    teleport="#app"
    :style="{ width: '90%', maxWidth: '400px', backgroundColor: 'white', position: 'fixed', top: '50%', left: '50%', transform: 'translate(-50%, -50%)', borderRadius: '16px', boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.1)' }"
    @update:show="onUpdateShow"
  >
    <div class="p-6">
      <div class="relative mb-4">
        <h2 class="pr-8 text-lg font-bold text-neutral-900">
          {{ title }}
        </h2>
        <button
          type="button"
          class="absolute right-0 top-0 p-1 text-neutral-500 hover:text-neutral-700"
          aria-label="閉じる"
          @click="close"
        >
          <span class="text-xl leading-none">×</span>
        </button>
      </div>
      <slot />
    </div>
  </Popup>
</template>
