<script setup lang="ts">
/**
 * 容積(3M)測定ページ（共通）
 * 縦・横・高さを入力し、確定で編集アイテムを更新して次画面へ遷移する
 */
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
import Input from '@/components/Input.vue'
import { useMeasureStore } from '@/stores/measure'
import { useSettingsStore } from '@/stores/settings'
import { useNotificationStore } from '@/stores/notification'
import { VueDraggable } from 'vue-draggable-plus'
import type { FormInstance } from 'vant'

const props = defineProps<{
  /** 戻る先パス */
  backTo: string
  /** 確定後の遷移先パス */
  nextTo: string
  /** 確定時に測定日時を更新するか（容積のみフローでは true） */
  updateMeasuredAt?: boolean
}>()

const router = useRouter()
const measureStore = useMeasureStore()
const settingsStore = useSettingsStore()
const notification = useNotificationStore()

const formRef = ref<FormInstance | null>(null)

const length = ref(measureStore.editingItem?.length ?? '')
const width = ref(measureStore.editingItem?.width ?? '')
const height = ref(measureStore.editingItem?.height ?? '')

const handleConfirm = async (): Promise<void> => {
  const valid = await formRef.value?.validate().then(() => true).catch(() => false)
  if (!valid) {
    return
  }
  const { length: assignedLength, width: assignedWidth, changed } =
    settingsStore.getAssignedLengthWidth(length.value, width.value)
  if (changed) {
    notification.show(`【${settingsStore.volumeLongestSide.label}】\n縦/横を入れ替えました`)
  }
  measureStore.updateEditingItemVolume({
    length: assignedLength,
    width: assignedWidth,
    height: height.value,
  })
  if (props.updateMeasuredAt) {
    measureStore.updateEditingItemMeasuredAt()
  }
  router.push(props.nextTo)
}

const inputList = ref([
    {
      label: '縦:',
      value: length,
    },
    {
      label: '横:',
      value: width,
    },
    {
      label: '高さ:',
      value: height,
    },
])

</script>

<template>
  <Title text="容積(3M)測定" class="mb-4">
    <BackButton :to="backTo" />
  </Title>
  <div class="w-full flex flex-col text-left mb-4">
    <span>商品の3M(縦/横/高さ)を</span>
    <span>計測してください</span>
  </div>
  <p class="font-bold w-full text-left mb-4">商品名: {{ measureStore.editingItem?.itemName ?? '' }}</p>
  <van-form ref="formRef" class="w-full">
    <VueDraggable v-model="inputList">
      <van-field v-for="(input, index) in inputList" :key="input.label" v-model="input.value" :name="input.value" :rules="[{ required: true, message: `${input.label}を入力してください` }]">
        <template #label>
          <InputLabel :label="input.label" />
        </template>
        <template #input>
          <Input :autofocus="index === 0" v-model="input.value" />
        </template>
      </van-field>
    </VueDraggable>
  </van-form>  
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      確定
    </FooterButton>
  </Footer>
</template>

<style scoped>
:deep(.van-field__label) {
  width: 10%;
}
</style>