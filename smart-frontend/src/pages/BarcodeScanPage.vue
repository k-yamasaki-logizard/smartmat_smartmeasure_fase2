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
import { useLoadingStore } from '@/stores/loading'
import InputLabel from '@/components/InputLabel.vue'
import type { FormInstance } from 'vant'

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
const zeroApi = useZeroApi()
const notification = useNotificationStore()

const measureStore = useMeasureStore();

const formRef = ref<FormInstance | null>(null)
const barcode = ref('')

const handleConfirm = async (): Promise<void> => {
  try {
    const valid = await formRef.value?.validate().then(() => true).catch(() => false)
    if (!valid) {
      return
    }
    useLoadingStore().show()
    const sku = await zeroApi.getSku(props.packId, barcode.value)
    if(sku?.ERROR_CODE === "4") {
        throw new Error('バーコード未登録の商品です。\nロジザードZEROで商品/バーコードを登録後、再度読み取ってください。')
    }
    if(sku?.ERROR_CODE !== "0") {
        throw new Error(`バーコードの読み取りでエラーが発生しました(${sku?.ERROR_MESSAGE})`)
    }
    measureStore.addEditingItem(barcode.value, sku.DATA.SKU[0].ITEM_ID, sku.DATA.SKU[0].ITEM_NAME)
    router.push(props.nextTo)
  } catch (error) {
    notification.show((error as Error).message ?? 'エラーが発生しました')
  } finally {
    useLoadingStore().hide()
  }
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
  <van-form ref="formRef" class="w-full">
    <van-field v-model="barcode" name="barcode" :rules="[{ required: true, message: 'バーコードを入力してください' }]">
      <template #label>
        <InputLabel :label="'BC:'" />
      </template>
      <template #input>
        <Input :autofocus="true" v-model="barcode" />
      </template>
    </van-field>
  </van-form>
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
        確定
    </FooterButton>
  </Footer>
</template>

<style scoped>
:deep(.van-field__label) {
  width: 8%;
}
</style>