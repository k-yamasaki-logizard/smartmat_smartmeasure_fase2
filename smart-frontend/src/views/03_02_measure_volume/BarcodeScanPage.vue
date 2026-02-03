<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
import Input from '@/components/Input.vue'
import { useMeasureStore } from '@/stores/measure'

/**
 * 画面ID:03_02_01
 * バーコードスキャン（容積のみ）
 */
const router = useRouter()
const barcode = ref('')
const measureStore = useMeasureStore()

const handleConfirm = () => {
  measureStore.addEditingItem(barcode.value)
  router.push('/update/volume/measure-volume')
}

onMounted(() => {
  measureStore.clearEditingItem()
})
</script>

<template>
  <Title text="バーコードスキャン" class="mb-4">
    <BackButton to="/update" />
  </Title>
  <div class="w-full flex flex-col text-left mb-4">
    <span>容積を測定する商品の</span>
    <span>バーコードをスキャンしてください</span>
  </div>
  <Input v-model="barcode" label="商品名:" class="mb-4" />
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      確定
    </FooterButton>
  </Footer>
</template>