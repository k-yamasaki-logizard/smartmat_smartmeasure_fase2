<script setup lang="ts">
import InfoIcon from '@/components/InfoIcon.vue'
import Title from '@/components/Title.vue'
import Card from '@/components/Card.vue'
import Footer from '@/components/Footer.vue';
import FooterButton from '@/components/FooterButton.vue';
import { useRouter } from 'vue-router';

const router = useRouter()
/**
 * 画面ID:03_03_05
 * 商品マスタ登録/更新 完了（重量）
 */
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
    <FooterButton position="3" variant="primary" @click="router.push('/update/weight/barcode-scan')">次へ</FooterButton>
  </Footer>
</template>
