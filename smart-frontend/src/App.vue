<script setup lang="ts">
import ContentWrapper from '@/layouts/ContentWrapper.vue'
import NotificationBar from '@/components/NotificationBar.vue'
import LoadingOverlay from '@/components/LoadingOverlay.vue'
</script>

<template>
  <ContentWrapper>
    <router-view />
  </ContentWrapper>
  <NotificationBar />
  <LoadingOverlay />
</template>
