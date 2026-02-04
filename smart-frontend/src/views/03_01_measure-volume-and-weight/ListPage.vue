<script setup lang="ts">
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'
import Title from '@/components/Title.vue'
import BackButton from '@/components/BackButton.vue'
import Card from '@/components/Card.vue'
import MeasureButton from '@/components/MeasureButton.vue'
import FooterButton from '@/components/FooterButton.vue'
import Footer from '@/components/Footer.vue'
import FixedScrollLayout from '@/layouts/FixedScrollLayout.vue'
import RemeasureChoiceModal from '@/views/03_01_measure-volume-and-weight/RemeasureChoiceModal.vue'
import { useMeasureStore } from '@/stores/measure'
import type { StoredDataItem } from '@/stores/types'

/**
 * 画面ID:03_01_04
 * 容積・重量測定一覧
 */
const router = useRouter()
const measureStore = useMeasureStore()
const items = computed(() => measureStore.storedList)

const showRemeasureModal = ref(false)
const remeasureTargetItem = ref<StoredDataItem | null>(null)

function handleRemeasure(item: StoredDataItem): void {
  remeasureTargetItem.value = item
  showRemeasureModal.value = true
}

function onRemeasureModalUpdateShow(value: boolean): void {
  showRemeasureModal.value = value
  if (!value) remeasureTargetItem.value = null
}
</script>

<template>
  <FixedScrollLayout>
    <template #fixed>
      <Title text="容積・重量測定一覧" class="mb-4">
        <BackButton to="/update/volume-and-weight/measure-weight" />
      </Title>
    </template>
    <template #scrollable>
      <div v-for="(item, index) in items" :key="item.tempItemId" class="w-full">
        <Card :backgroundColor="(index + 1) % 2 === 0 ? 'gray' : 'white'">
          <template #content>
            <div class="flex flex-row items-center gap-2 w-full">
              <span class="font-medium shrink-0">{{ index + 1 }}:</span>
              <div>
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
                <div class="flex gap-1 items-center">
                    <span class="text-[#4caf50] text-right w-[70px] shrink-0">重量:</span>
                    <span v-if="item.isMeasuringWeight">(測定中...)</span>
                    <span v-else>{{ item.weight }}</span>
                </div>
                <div class="flex gap-1">
                    <span class="text-neutral-500 text-sm">(計測時間 {{ item.measuredAt }})</span>
                    <div @click.stop>
                        <MeasureButton class="whitespace-nowrap px-3 text-sm" text="再測定" @click="handleRemeasure(item)" />
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
    <FooterButton position="3" variant="primary" @click="router.push('/update/volume-and-weight/measure-weight')">
      確定
    </FooterButton>
  </Footer>
  <RemeasureChoiceModal
    :show="showRemeasureModal"
    :item="remeasureTargetItem"
    @update:show="onRemeasureModalUpdateShow"
  />
</template>
