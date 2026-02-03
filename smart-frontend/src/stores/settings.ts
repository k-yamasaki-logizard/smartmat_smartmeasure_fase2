import { defineStore } from 'pinia'
import type { RegisterTargetMaster, VolumeLongestSide } from './types'

/**
 * 設定ストア
 * 容積・重量 登録先マスタ/項目、容積測定 縦/横 自動変更
 */
export const useSettingsStore = defineStore('settings', {
  state: () => ({
    /** 登録先マスタ: 商品マスタ | 商品梱包形態マスタ */
    registerTargetMaster: 'm_item' as RegisterTargetMaster,
    /** 容積測定 縦/横 自動変更: 最長辺 = 縦 | 最長辺 = 横 */
    volumeLongestSide: 'length' as VolumeLongestSide,
    /** 縦 (容積) の登録先項目 */
    volumeLengthField: 'length_sku',
    /** 横 (容積) の登録先項目 */
    volumeWidthField: 'width_sku',
    /** 高さ (容積) の登録先項目 */
    volumeHeightField: 'height_sku',
    /** 重量の登録先項目 */
    weightField: 'weight_sku',
  }),

  actions: {
    setRegisterTargetMaster(value: RegisterTargetMaster) {
      this.registerTargetMaster = value
    },
    setVolumeLongestSide(value: VolumeLongestSide) {
      this.volumeLongestSide = value
    },
    setFieldMapping(payload: {
      length?: string
      width?: string
      height?: string
      weight?: string
    }) {
      if (payload.length !== undefined) this.volumeLengthField = payload.length
      if (payload.width !== undefined) this.volumeWidthField = payload.width
      if (payload.height !== undefined) this.volumeHeightField = payload.height
      if (payload.weight !== undefined) this.weightField = payload.weight
    },
  },
})
