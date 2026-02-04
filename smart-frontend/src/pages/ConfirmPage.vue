<script setup lang="ts">
/**
 * 商品マスタ登録/更新 確認ページ（共通）
 * 測定済み一覧を表示し、確定でAPI送信・完了画面へ遷移する
 */
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Card from '@/components/Card.vue'
import FooterButton from '@/components/FooterButton.vue'
import Footer from '@/components/Footer.vue'
import FixedScrollLayout from '@/layouts/FixedScrollLayout.vue'
import { useMeasureStore } from '@/stores/measure'
import type { StoredDataItem } from '@/stores/types'

/** 表示モード: 容積+重量 | 容積のみ | 重量のみ（カードに表示する項目を切り替え） */
export type ConfirmDisplayMode = 'volume-and-weight' | 'volume' | 'weight'

const props = defineProps<{
  /** 戻る先パス */
  backTo: string
  /** 確定後の遷移先パス */
  finishTo: string
  /** カードに表示する項目 */
  displayMode: ConfirmDisplayMode
}>()

const router = useRouter()
const measureStore = useMeasureStore()
const items: StoredDataItem[] = measureStore.storedList

/** 表示用の行番号（item.number があれば使用、なければ index+1） */
function displayNumber(item: StoredDataItem & { number?: number }, index: number): number {
  const n = 'number' in item ? item.number : undefined
  return n != null ? Number(n) : index + 1
}

const handleConfirm = (): void => {
  measureStore.submitItems()
  router.push(props.finishTo)
}
</script>

<template>
  <FixedScrollLayout>
    <template #fixed>
      <Title text="商品マスタ登録/更新 確認" class="mb-4">
        <BackButton :to="backTo" />
      </Title>
      <div class="w-full flex flex-col text-center mb-4">
        <span>下記の内容で商品マスタを</span>
        <span>登録/更新します</span>
      </div>
    </template>
    <template #scrollable>
      <div class="w-full" v-for="(item, index) in items" :key="item.tempId">
        <Card :backgroundColor="(Number(index) + 1) % 2 === 0 ? 'gray' : 'white'">
          <template #content>
            <div class="flex flex-row items-center">
              <span>{{ displayNumber(item, index) }}:</span>
              <div>
                <div class="flex gap-1 items-center">
                  <span class="text-[#4caf50] text-right w-[70px]">商品名:</span>
                  <span>{{ item.itemName }}</span>
                </div>
                <template v-if="displayMode === 'volume-and-weight' || displayMode === 'volume'">
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px]">縦:</span>
                    <span>{{ item.length }}</span>
                  </div>
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px]">横:</span>
                    <span>{{ item.width }}</span>
                  </div>
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px]">高さ:</span>
                    <span>{{ item.height }}</span>
                  </div>
                </template>
                <template v-if="displayMode === 'volume-and-weight' || displayMode === 'weight'">
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px]">重量:</span>
                    <span>{{ item.weight }}</span>
                  </div>
                </template>
              </div>
            </div>
          </template>
        </Card>
      </div>
    </template>
  </FixedScrollLayout>
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      確定
    </FooterButton>
  </Footer>
</template>
