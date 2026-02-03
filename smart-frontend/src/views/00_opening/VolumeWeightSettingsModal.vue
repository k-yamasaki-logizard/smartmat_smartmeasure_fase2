<script setup lang="ts">
/**
 * 容積・重量 登録先マスタ/項目 設定モーダル
 * 登録先マスタと縦/横/高さ/重量の項目マッピングを設定する。
 * Modal.vue で Popup + タイトル + 閉じるボタンをラップし、中身のみを担当。
 */
import Modal from '@/components/Modal.vue'
import Select from '@/components/Select.vue'
import { useSettingsStore } from '@/stores/settings'
import type { RegisterTargetMaster } from '@/stores/types'

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

const registerTargetOptions: { value: RegisterTargetMaster; label: string }[] = [
  { value: 'm_item', label: '商品マスタ' },
  { value: 'm_item_pack', label: '商品梱包形態マスタ' },
]

const volumeLengthOptions = [{ value: 'length_sku', label: '縦 (SKU単位)' }]
const volumeWidthOptions = [{ value: 'width_sku', label: '横 (SKU単位)' }]
const volumeHeightOptions = [{ value: 'height_sku', label: '高さ (SKU単位)' }]
const weightOptions = [{ value: 'weight_sku', label: '重量 (SKU単位)' }]

function onRegisterTargetMaster(value: string): void {
  settingsStore.setRegisterTargetMaster(value as RegisterTargetMaster)
  console.log(settingsStore.registerTargetMaster)
}

function onFieldMapping(payload: {
  length?: string
  width?: string
  height?: string
  weight?: string
}): void {
  settingsStore.setFieldMapping(payload)
}
</script>

<template>
  <Modal
    :show="show"
    title="容積・重量 登録先マスタ/項目"
    @update:show="(v: boolean) => emit('update:show', v)"
  >
    <div class="space-y-4">
      <Select
        label="登録先マスタ"
        :model-value="settingsStore.registerTargetMaster"
        :options="registerTargetOptions"
        @update:model-value="onRegisterTargetMaster"
      />
      <Select
        label="縦 (容積)"
        :model-value="settingsStore.volumeLengthField"
        :options="volumeLengthOptions"
        @update:model-value="(v: string) => onFieldMapping({ length: v })"
      />
      <Select
        label="横 (容積)"
        :model-value="settingsStore.volumeWidthField"
        :options="volumeWidthOptions"
        @update:model-value="(v: string) => onFieldMapping({ width: v })"
      />
      <Select
        label="高さ (容積)"
        :model-value="settingsStore.volumeHeightField"
        :options="volumeHeightOptions"
        @update:model-value="(v: string) => onFieldMapping({ height: v })"
      />
      <Select
        label="重量"
        :model-value="settingsStore.weightField"
        :options="weightOptions"
        @update:model-value="(v: string) => onFieldMapping({ weight: v })"
      />
    </div>
  </Modal>
</template>
