<script setup lang="ts">
/**
 * 再測定選択モーダル
 * 容積・重量測定一覧の「再測定」押下時に表示し、容積 or 重量の測定画面へ遷移する。
 */
import { useRouter } from 'vue-router'
import Modal from '@/components/Modal.vue'
import { useMeasureStore } from '@/stores/measure'
import type { StoredDataItem } from '@/stores/types'

const props = withDefaults(
  defineProps<{
    show: boolean
    /** 再測定対象の商品（未選択時は null） */
    item: StoredDataItem | null
  }>(),
  { show: false, item: null }
)

const emit = defineEmits<{
  (e: 'update:show', value: boolean): void
}>()

const router = useRouter()
const measureStore = useMeasureStore()

function chooseVolume(): void {
  if (!props.item) return
  measureStore.setEditingItemById(props.item.tempItemId)
  emit('update:show', false)
  router.push('/update/volume-and-weight/measure-volume')
}

function chooseWeight(): void {
  if (!props.item) return
  measureStore.setEditingItemById(props.item.tempItemId)
  emit('update:show', false)
  router.push('/update/volume-and-weight/measure-weight')
}
</script>

<template>
  <Modal
    :show="show"
    title="再測定"
    @update:show="(v: boolean) => emit('update:show', v)"
  >
    <p class="mb-4 text-sm text-neutral-600">
      再測定する項目を選択してください
    </p>
    <div class="flex flex-col gap-3">
      <button
        type="button"
        class="flex items-center justify-center w-full py-3 px-4 text-base font-medium text-neutral-900 rounded-lg border border-orange/50 bg-white shadow-[0_1px_3px_rgba(230,126,34,0.15)] hover:bg-[#fefaf7] transition-colors"
        :disabled="!item"
        @click="chooseVolume"
      >
        容積
      </button>
      <button
        type="button"
        class="flex items-center justify-center w-full py-3 px-4 text-base font-medium text-neutral-900 rounded-lg border border-orange/50 bg-white shadow-[0_1px_3px_rgba(230,126,34,0.15)] hover:bg-[#fefaf7] transition-colors"
        :disabled="!item"
        @click="chooseWeight"
      >
        重量
      </button>
    </div>
  </Modal>
</template>
