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
  const settingValue = volumeLongestSideOptions.find(option => option.value === value)
  settingsStore.setVolumeLongestSide(settingValue)
}
</script>

<template>
  <Modal
    :show="show"
    title="容積測定 縦/横 自動変更"
    @update:show="(v: boolean) => emit('update:show', v)"
  >
    <Select
      :model-value="settingsStore.volumeLongestSide.value"
      :options="volumeLongestSideOptions"
      @update:model-value="onVolumeLongestSide"
    />
  </Modal>
</template>
