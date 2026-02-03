<script setup lang="ts">
import ContentWrapper from '@/layouts/ContentWrapper.vue'
import NotificationBar from '@/components/NotificationBar.vue'
</script>

<template>
  <ContentWrapper>
    <router-view />
  </ContentWrapper>
  <NotificationBar />
</template>
