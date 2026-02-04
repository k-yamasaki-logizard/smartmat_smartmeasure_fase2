<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
import Input from '@/components/Input.vue'
import { useMeasureStore } from '@/stores/measure'
import { useZeroApi } from '@/composables/zero-api'
import { useNotificationStore } from '@/stores/notification'

const props = defineProps<{
    backTo: string,
    nextTo: string,
    packId: string,
}>()

/**
 * 画面ID:03_01_01
 * バーコードスキャン
 */
const router = useRouter()
const barcode = ref('')
const zeroApi = useZeroApi()
const notification = useNotificationStore()

const measureStore = useMeasureStore();

const handleConfirm = async () => {
  // 暫定的に、pack_barcode=3で固定
  const sku = await zeroApi.getSku(props.packId, barcode.value)
  if(sku?.ERROR_CODE === "6") {
    notification.show('バーコード未登録の商品です。\nロジザードZEROで商品/バーコードを登録後、再度読み取ってください。')
    return
  }
  if(sku?.ERROR_CODE !== "0") {
    notification.show('バーコードの読み取りでエラーが発生しました')
    return
  }
  measureStore.addEditingItem(barcode.value, sku.DATA.SKU[0].ITEM_ID, sku.DATA.SKU[0].ITEM_NAME)
  router.push(props.nextTo)
}

onMounted(() => {
  measureStore.clearEditingItem()
})

</script>

<template>
  <Title text="バーコードスキャン" class="mb-4">
    <BackButton :to="backTo" />
  </Title>
  <div class="w-full flex flex-col text-left mb-4">
    <span>容積・重量を測定する商品の</span>
    <span>バーコードをスキャンしてください</span>
  </div>
  <Input v-model="barcode" label="BC:" class="mb-4" />
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
        確定
    </FooterButton>
  </Footer>
</template>