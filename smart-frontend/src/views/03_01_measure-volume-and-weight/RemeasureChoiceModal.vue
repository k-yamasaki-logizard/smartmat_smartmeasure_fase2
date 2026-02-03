<script setup lang="ts">
/**
 * 再測定選択モーダル
 * 容積・重量測定一覧の「再測定」押下時に表示し、容積 or 重量の測定画面へ遷移する。
 * mode に応じて商品情報（BC/縦/横/高さ/重量）の表示を出し分ける。
 */
import { useRouter } from 'vue-router'
import Modal from '@/components/Modal.vue'
import { useMeasureStore } from '@/stores/measure'
import type { StoredDataItem } from '@/stores/types'

export type RemeasureModalMode = 'volume-and-weight' | 'volume' | 'weight'

const props = withDefaults(
  defineProps<{
    show: boolean
    /** 再測定対象の商品（未選択時は null） */
    item: StoredDataItem | null
    /** 表示モード: 容積・重量両方 / 容積のみ / 重量のみ（表示項目の出し分けに使用） */
    mode?: RemeasureModalMode
  }>(),
  { show: false, item: null, mode: 'volume-and-weight' }
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
    <div v-if="item" class="mb-4 rounded-lg border border-neutral-200 bg-neutral-50 p-3 text-sm">
      <div class="flex flex-col gap-1">
        <div class="flex gap-1 items-center">
          <span class="text-[#4caf50] w-[56px] shrink-0">商品名:</span>
          <span>{{ item.barcode }}</span>
        </div>
        <template v-if="mode === 'volume-and-weight' || mode === 'volume'">
          <div class="flex gap-1 items-center">
            <span class="text-[#4caf50] w-[56px] shrink-0">縦:</span>
            <span>{{ item.length }}</span>
          </div>
          <div class="flex gap-1 items-center">
            <span class="text-[#4caf50] w-[56px] shrink-0">横:</span>
            <span>{{ item.width }}</span>
          </div>
          <div class="flex gap-1 items-center">
            <span class="text-[#4caf50] w-[56px] shrink-0">高さ:</span>
            <span>{{ item.height }}</span>
          </div>
        </template>
        <template v-if="mode === 'volume-and-weight' || mode === 'weight'">
          <div class="flex gap-1 items-center">
            <span class="text-[#4caf50] w-[56px] shrink-0">重量:</span>
            <span v-if="item.isMeasuringWeight">(測定中...)</span>
            <span v-else>{{ item.weight }}</span>
          </div>
        </template>
      </div>
    </div>
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
