<script setup lang="ts">
/**
 * 重量測定ページ（共通）
 * スマートマットで重量を計測し、次商品へ/測定一覧/登録確認へ遷移する
 */
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import MeasureButton from '@/components/MeasureButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
import { useMeasureStore } from '@/stores/measure'
import { useNotificationStore } from '@/stores/notification'

const props = defineProps<{
  /** 戻る先パス */
  backTo: string
  /** 「次商品へ」の遷移先（バーコードスキャン） */
  nextTo: string
  /** 「測定一覧」の遷移先 */
  listTo: string
  /** 「登録確認」の遷移先 */
  confirmTo: string
}>()

const router = useRouter()
const measureStore = useMeasureStore()

const handleMeasureWeight = async (): Promise<void> => {
  const result = await measureStore.updateEditingItemWeight()
  if (result.success) {
    useNotificationStore().show(`【商品名：${result.itemName}】\n重量測定が完了しました`)
  } else {
    useNotificationStore().show(`【商品名：${result.itemName}】\n重量測定に失敗しました(${result.error})`)
  }
}

const nextItem = (): void => {
  measureStore.updateEditingItemMeasuredAt()
  measureStore.storeEditingItem()
  router.push(props.nextTo)
}

const handleConfirm = (): void => {
  measureStore.updateEditingItemMeasuredAt()
  if (measureStore.hasMeasuringWeightItem) {
    useNotificationStore().show('測定中の商品があります。測定が完了したら確認ボタンを押してください。')
    return
  }
  router.push(props.confirmTo)
}

</script>

<template>
  <Title text="重量測定" class="mb-4">
    <BackButton :to="backTo" />
  </Title>
  <div class="w-full flex flex-col text-left mb-4">
    <span>商品の重量を計測します。</span>
    <span>スマートマットに商品を設置し、</span>
    <span>本体上面端の計測ボタンと、下記の</span>
    <span>「重量測定開始」ボタンを押下してください。</span>
  </div>
  <p class="font-bold w-full text-left mb-4">商品名: {{ measureStore.editingItem?.itemName ?? '' }}</p>
  <div class="w-full flex justify-center mb-4">
    <MeasureButton
      :disabled="measureStore.hasMeasuringWeightItem"
      class="py-3 px-6"
      @click="handleMeasureWeight"
    >
      {{ measureStore.hasMeasuringWeightItem ? '測定中...' : '重量測定開始' }}
    </MeasureButton>
  </div>
  <Footer>
    <FooterButton position="1" variant="secondary" @click="nextItem">
      次商品へ
    </FooterButton>
    <FooterButton position="2" variant="secondary" @click="router.push(listTo)">
      測定一覧
    </FooterButton>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      登録確認
    </FooterButton>
  </Footer>
</template>
