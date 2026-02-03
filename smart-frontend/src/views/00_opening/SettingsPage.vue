<script setup lang="ts">
import { ref } from 'vue'
import Title from '@/components/Title.vue'
import LinkButton from '@/components/LinkButton.vue'
import BackButton from '@/components/BackButton.vue'
import VolumeWeightSettingsModal from '@/views/00_opening/VolumeWeightSettingsModal.vue'
import VolumeLongestSideSettingsModal from '@/views/00_opening/VolumeLongestSideSettingsModal.vue'

/**
 * 画面ID：04_01_01
 * 設定画面。容積・重量 登録先マスタ/項目、容積測定 縦/横 自動変更はモーダルで設定。
 */
const showVolumeWeightModal = ref(false)
const showVolumeLongestSideModal = ref(false)

function openVolumeWeightModal(): void {
  showVolumeWeightModal.value = true
}

function openVolumeLongestSideModal(): void {
  showVolumeLongestSideModal.value = true
}
</script>

<template>
    <Title text="設定" class="mb-4">
        <BackButton to="/" />
    </Title>
    <LinkButton to="" class="mb-4" @click="openVolumeWeightModal">
        容積・重量 登録先マスタ/項目
        <template #subtext>
            どのマスタ/項目に登録するか指定
        </template>
    </LinkButton>
    <VolumeWeightSettingsModal v-model:show="showVolumeWeightModal" />
    <LinkButton to="" class="mb-4" @click="openVolumeLongestSideModal">
        容積測定 縦/横 自動変更
        <template #subtext>
            縦/横の最長辺を、どちらかの方向に自動割当する
        </template>
    </LinkButton>
    <VolumeLongestSideSettingsModal v-model:show="showVolumeLongestSideModal" />
</template>