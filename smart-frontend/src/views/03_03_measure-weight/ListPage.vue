<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Card from '@/components/Card.vue'
import MeasureButton from '@/components/MeasureButton.vue'
import FooterButton from '@/components/FooterButton.vue'
import Footer from '@/components/Footer.vue'
import FixedScrollLayout from '@/layouts/FixedScrollLayout.vue'
import { useMeasureStore } from '@/stores/measure'
import type { StoredDataItem } from '@/stores/types'

/**
 * 画面ID:03_03_03
 * 重量測定一覧
 */
const router = useRouter()
const measureStore = useMeasureStore()
const items = computed(() => measureStore.storedList)

function handleRemeasure(item: StoredDataItem) {
  console.log('再測定', item.tempItemId)
}
</script>

<template>
  <FixedScrollLayout>
    <template #fixed>
      <Title text="重量測定一覧" class="mb-4">
        <BackButton to="/update/weight/measure-weight" />
      </Title>
    </template>
    <template #scrollable>
      <div v-for="(item, index) in items" :key="item.tempItemId" class="w-full">
        <Card :backgroundColor="(index + 1) % 2 === 0 ? 'gray' : 'white'">
          <template #content>
            <div class="flex flex-row items-center gap-2 w-full">
              <span class="font-medium shrink-0">{{ 'number' in item ? item.number : index + 1 }}:</span>
              <div>
                <div class="flex-1 min-w-0 flex flex-col gap-1 text-left">
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px] shrink-0">BC:</span>
                    <span>{{ item.barcode }}</span>
                  </div>
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px] shrink-0">重量:</span>
                    <span v-if="item.isMeasuringWeight">(測定中...)</span>
                    <span v-else>{{ item.weight }}</span>
                  </div>
                  <div class="flex gap-1 whitespace-nowrap">
                    <span class="text-neutral-500 text-sm">(計測時間 {{ item.measuredAt }})</span>
                    <div @click.stop>
                      <MeasureButton class="whitespace-nowrap px-3 text-sm" text="再測定" @click="handleRemeasure(item)" />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </template>
        </Card>
      </div>
    </template>
  </FixedScrollLayout>
  <Footer>
    <FooterButton position="3" variant="primary" @click="router.push('/update/weight/measure-weight')">
      確定
    </FooterButton>
  </Footer>
</template>
