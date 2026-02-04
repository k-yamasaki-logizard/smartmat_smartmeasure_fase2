<script setup lang="ts">
/**
 * 商品マスタ登録/更新 完了ページ（共通）
 * 完了メッセージを表示し、HOME または次商品へ遷移する
 */
import { useRouter } from 'vue-router'
import InfoIcon from '@/components/InfoIcon.vue'
import Title from '@/components/Title.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'

const props = defineProps<{
  /** 「次へ」ボタンの遷移先（次商品のバーコードスキャン等） */
  nextTo: string
}>()

const router = useRouter()

const handleNext = (): void => {
  router.push(props.nextTo)
}
</script>

<template>
  <Title text="商品マスタ登録/更新 完了" class="w-full justify-center mb-4" />
  <InfoIcon class="mb-4 w-full" />
  <div class="w-full flex flex-col text-center mb-4">
    <span>商品マスタの登録/更新が</span>
    <span>完了しました。</span>
  </div>
  <Footer>
    <FooterButton position="1" variant="secondary" @click="router.push('/')">HOME</FooterButton>
    <FooterButton position="3" variant="primary" @click="handleNext">次へ</FooterButton>
  </Footer>
</template>
