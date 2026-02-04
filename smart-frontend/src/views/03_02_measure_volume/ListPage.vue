<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Card from '@/components/Card.vue'
import MeasureButton from '@/components/MeasureButton.vue'
import Footer from '@/components/Footer.vue'
import FooterButton from '@/components/FooterButton.vue'
import FixedScrollLayout from '@/layouts/FixedScrollLayout.vue'
import { useMeasureStore } from '@/stores/measure'
import type { StoredDataItem } from '@/stores/types'

/**
 * 画面ID:03_02_03
 * 容積測定一覧
 */
const router = useRouter()
const measureStore = useMeasureStore()
const items = computed(() => measureStore.storedList)

function handleRemeasure(item: StoredDataItem): void {
  measureStore.setEditingItemById(item.tempItemId)
  router.push('/update/volume/measure-volume')
}
</script>

<template>
  <FixedScrollLayout>
    <template #fixed>
      <Title text="容積測定一覧" class="mb-4">
        <BackButton to="/update/volume/measure-volume" />
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
                    <span class="text-[#4caf50] text-right w-[70px] shrink-0">商品名:</span>
                    <span>{{ item.itemName }}</span>
                  </div>
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px] shrink-0">縦:</span>
                    <span>{{ item.length }}</span>
                  </div>
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px] shrink-0">横:</span>
                    <span>{{ item.width }}</span>
                  </div>
                  <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px] shrink-0">高さ:</span>
                    <span>{{ item.height }}</span>
                  </div>
                  <div class="flex gap-1 whitespace-nowrap">
                    <span class="text-neutral-500 text-sm">(計測時間 {{ item.measuredAt }})</span>
                    <div @click.stop>
                      <MeasureButton text="再測定" class="whitespace-nowrap px-3 text-sm" @click="handleRemeasure(item)" />
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
    <FooterButton position="1" variant="secondary" @click="measureStore.storeEditingItem(); router.push('/update/volume/barcode-scan')">
      次商品へ
    </FooterButton>
    <FooterButton position="3" variant="primary" @click="router.push('/update/volume/confirm')">
      確定
    </FooterButton>
  </Footer>
</template>
