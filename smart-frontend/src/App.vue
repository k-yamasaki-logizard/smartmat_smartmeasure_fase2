<script setup lang="ts">
import ContentWrapper from '@/layouts/ContentWrapper.vue'
import NotificationBar from '@/components/NotificationBar.vue'
import LoadingOverlay from '@/components/LoadingOverlay.vue'
</script>

<template>
  <ContentWrapper ownerName="スマートマットクラウド検証" areaName="OCEテスト東京">
    <router-view />
  </ContentWrapper>
  <NotificationBar />
  <LoadingOverlay />
</template>
