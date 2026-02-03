<script setup lang="ts">
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Card from '@/components/Card.vue'
import FooterButton from '@/components/FooterButton.vue'
import Footer from '@/components/Footer.vue'
import FixedScrollLayout from '@/layouts/FixedScrollLayout.vue'
import { useMeasureStore } from '@/stores/measure'
import type { StoredDataItem } from '@/stores/types'

/**
 * 画面ID:03_03_04
 * 商品マスタ登録/更新 確認（重量）
 */
const router = useRouter()
const measureStore = useMeasureStore()
const items = measureStore.storedList

const handleConfirm = () => {
  measureStore.submitItems()
  router.push('/update/weight/finish')
}
</script>

<template>
  <FixedScrollLayout>
    <template #fixed>
      <Title text="商品マスタ登録/更新 確認" class="mb-4">
        <BackButton to="/update/weight/measure-weight" />
      </Title>
      <div class="w-full flex flex-col text-center mb-4">
        <span>下記の内容で商品マスタを</span>
        <span>登録/更新します</span>
      </div>
    </template>
    <template #scrollable>
      <div class="w-full" v-for="(item, index) in items" :key="item.tempItemId">
        <Card :backgroundColor="(index + 1) % 2 === 0 ? 'gray' : 'white'">
          <template #content>
            <div class="flex flex-row items-center">
              <span>{{ 'number' in item ? item.number : index + 1 }}:</span>
              <div>
                <div class="flex gap-1 items-center">
                  <span class="text-[#4caf50] text-right w-[70px]">商品名:</span>
                  <span>{{ item.itemName }}</span>
                </div>
                <div class="flex gap-1 items-center">
                  <span class="text-[#4caf50] text-right w-[70px]">重量:</span>
                  <span>{{ item.weight }}</span>
                </div>
              </div>
            </div>
          </template>
        </Card>
      </div>
    </template>
  </FixedScrollLayout>
  <Footer>
    <FooterButton position="3" variant="primary" @click="handleConfirm">
      確定
    </FooterButton>
  </Footer>
</template>
