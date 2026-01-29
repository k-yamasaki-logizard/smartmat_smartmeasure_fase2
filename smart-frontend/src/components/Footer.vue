<script setup lang="ts">

</script>

<template>
  <!-- 
    3列のグリッドレイアウトに変更 
    中央寄せ・固定フッター
  -->
  <div class="fixed bottom-0 w-full mx-auto grid grid-cols-3 items-center justify-center gap-3 p-4">
    <slot />
  </div>
</template>
