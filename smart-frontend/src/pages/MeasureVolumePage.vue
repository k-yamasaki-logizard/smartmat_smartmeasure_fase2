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

const length = ref('')
const width = ref('')
const height = ref('')

const handleConfirm = (): void => {
  const { length: assignedLength, width: assignedWidth, changed } =
    settingsStore.getAssignedLengthWidth(length.value, width.value)
  if (changed) {
    notification.show('縦/横を入れ替えました')
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

  const inputList = ref([
    {
      label: '縦:',
      value: length.value,
    },
    {
      label: '横:',
      value: width.value,
    },
    {
      label: '高さ:',
      value: height.value,
    },
  ])
}

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
  <Input v-model="length" label="縦:" class="mb-2"/>
  <Input v-model="width" label="横:" class="mb-2"/>
  <Input v-model="height" label="高さ:" class="mb-2"/>
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      確定
    </FooterButton>
  </Footer>
</template>
