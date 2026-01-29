<script setup lang="ts">
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
/**
 * 画面ID:03_02_01
 * バーコードスキャン
 */
</script>

<template>
  <div class="flex flex-col items-start justify-start w-full h-full">
    <Title text="バーコードスキャン">
      <BackButton to="/update" />
    </Title>
    <span>容積・重量を測定する商品の</span>
    <span>バーコードをスキャンしてください</span>
  </div>
  <Footer>
    <FooterButton position="1" variant="secondary" to="/update/volume-and-weight/barcode-scan">
        ちんぽ
    </FooterButton>
    <FooterButton position="3" variant="primary" to="/update/volume-and-weight/measure-volume">
        確定
    </FooterButton>
  </Footer>
</template>
