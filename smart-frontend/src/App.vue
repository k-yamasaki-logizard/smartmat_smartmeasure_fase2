<script setup lang="ts">
import Header from '@/components/Header.vue';
import ContentWrapper from '@/components/ContentWrapper.vue';
</script>

<template>
  <Header />
  <ContentWrapper>
    <router-view />
  </ContentWrapper>
</template>
