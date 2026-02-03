<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import MeasureButton from '@/components/MeasureButton.vue'
import { useMeasureStore } from '@/stores/measure'

/**
 * 画面ID:03_01_02
 * 重量測定画面
 */
const router = useRouter()
const measureStore = useMeasureStore()
const itemLabel = computed(() => measureStore.editingItem?.barcode ?? '')

const handleMeasureWeight = () => {
  measureStore.updateEditingItemWeight()
}

const nextItem = () => {
  measureStore.updateEditingItemMeasuredAt()
  measureStore.storeEditingItem()
  router.push('/update/volume-and-weight/barcode-scan')
}

const handleConfirm = () => {
  measureStore.updateEditingItemMeasuredAt()
  router.push('/update/volume-and-weight/confirm')
}

</script>

<template>
    <Title text="重量測定" class="mb-4">
        <BackButton to="/update/volume-and-weight/measure-volume" />
    </Title>
    <div class="w-full flex flex-col text-left mb-4">
        <span>商品の重量を計測します。</span>
        <span>スマートマットに商品を設置し、</span>
        <span>本体上面端の計測ボタンと、下記の</span>
        <span>「重量測定開始」ボタンを押下してください。</span>
    </div>
    <p class="font-bold w-full text-left mb-4">商品名: {{ itemLabel }}</p>
    <div class="w-full flex justify-center">
        <MeasureButton :disabled="measureStore.isMeasuringWeight" class="py-3 px-6 " @click="handleMeasureWeight">
            {{ measureStore.isMeasuringWeight ? '測定中...' : '重量測定開始' }}
        </MeasureButton>
    </div>
    <Footer>
        <FooterButton position="1" variant="secondary" @click="nextItem">
            次商品へ
        </FooterButton>
        <FooterButton position="2" variant="secondary" @click="router.push('/update/volume-and-weight/list')">
            測定一覧
        </FooterButton>
        <FooterButton position="3" variant="primary" @click="handleConfirm">
            登録確認
        </FooterButton>
    </Footer>
</template>