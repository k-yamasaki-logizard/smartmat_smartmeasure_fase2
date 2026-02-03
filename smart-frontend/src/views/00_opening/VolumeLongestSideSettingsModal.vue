<script setup lang="ts">
/**
 * 容積測定 縦/横 自動変更 設定モーダル
 * 最長辺を縦/横のどちらに自動割当するかを設定する。
 * Modal.vue で Popup + タイトル + 閉じるボタンをラップし、中身のみを担当。
 */
import Modal from '@/components/Modal.vue'
import Select from '@/components/Select.vue'
import { useSettingsStore } from '@/stores/settings'
import type { VolumeLongestSide } from '@/stores/types'

const props = withDefaults(
  defineProps<{
    show: boolean
  }>(),
  { show: false }
)

const emit = defineEmits<{
  (e: 'update:show', value: boolean): void
}>()

const settingsStore = useSettingsStore()

const volumeLongestSideOptions: { value: VolumeLongestSide; label: string }[] = [
  { value: 'length', label: '[最長辺 = 縦]に自動変更' },
  { value: 'width', label: '[最長辺 = 横]に自動変更' },
]

function onVolumeLongestSide(value: string): void {
  settingsStore.setVolumeLongestSide(value as VolumeLongestSide)
}
</script>

<template>
  <Modal
    :show="show"
    title="容積測定 縦/横 自動変更"
    @update:show="(v: boolean) => emit('update:show', v)"
  >
    <p class="mb-4 flex items-center gap-1 text-sm text-neutral-600">
      <span class="inline-block size-0 border-l-[5px] border-r-[5px] border-b-[8px] border-l-transparent border-r-transparent border-b-neutral-400" />
      縦/横の最長辺を、どちらかの方向に自動割当する
    </p>
    <Select
      :model-value="settingsStore.volumeLongestSide"
      :options="volumeLongestSideOptions"
      @update:model-value="onVolumeLongestSide"
    />
  </Modal>
</template>
