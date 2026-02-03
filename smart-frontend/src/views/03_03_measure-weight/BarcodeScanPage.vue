<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
import Input from '@/components/Input.vue'
import { useMeasureStore } from '@/stores/measure'
import useZeroApi from '@/composables/zero-api'
import { useNotificationStore } from '@/stores/notification'

/**
 * 画面ID:03_03_01
 * バーコードスキャン（重量）
 */
const router = useRouter()
const barcode = ref('')
const measureStore = useMeasureStore()
const zeroApi = useZeroApi()
const notification = useNotificationStore()

const handleConfirm = async () => {
  const sku = await zeroApi.getSku('3', barcode.value)
  if(sku?.ERROR_CODE !== "0") {
    notification.show('バーコードの読み取りでエラーが発生しました')
    return
  }
  measureStore.addEditingItem(barcode.value, sku.DATA.SKU.ITEM_NAME)
  router.push('/update/weight/measure-weight')
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
    <span>重量を測定する商品の</span>
    <span>バーコードをスキャンしてください</span>
  </div>
  <Input v-model="barcode" label="BC:" class="mb-4" />
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      確定
    </FooterButton>
  </Footer>
</template>
