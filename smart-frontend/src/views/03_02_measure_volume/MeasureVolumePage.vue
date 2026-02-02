<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
import Input from '@/components/Input.vue'
import { useMeasureStore } from '@/stores/measure'

/**
 * 画面ID:03_02_02
 * 容積(3M)測定
 */
const router = useRouter()
const measureStore = useMeasureStore()
const itemLabel = computed(() => measureStore.editingItem?.barcode ?? '')

const length = ref('')
const width = ref('')
const height = ref('')

const handleConfirm = () => {
  measureStore.updateEditingItemVolume({
    length: length.value,
    width: width.value,
    height: height.value,
  })
  measureStore.updateEditingItemMeasuredAt()
  router.push('/update/volume/list')
}
</script>

<template>
  <Title text="容積(3M)測定" class="mb-4">
    <BackButton to="/update/volume/barcode-scan" />
  </Title>
  <div class="w-full flex flex-col text-left mb-4">
    <span>商品の3M(縦/横/高さ)を</span>
    <span>計測してください</span>
  </div>
  <p class="font-bold w-full text-left mb-4">BC: {{ itemLabel }}</p>
  <Input v-model="length" label="縦:" class="mb-2" />
  <Input v-model="width" label="横:" class="mb-2" />
  <Input v-model="height" label="高さ:" class="mb-2" />
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      確定
    </FooterButton>
  </Footer>
</template>
