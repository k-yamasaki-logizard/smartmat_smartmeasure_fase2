import { defineStore } from 'pinia'
import type { RegisterTargetMaster, VolumeLongestSide } from './types'

/**
 * 設定ストア
 * 容積・重量 登録先マスタ/項目、容積測定 縦/横 自動変更
 */
export const useSettingsStore = defineStore('settings', {
  state: () => ({
    /** 登録先マスタ: 商品梱包形態マスタ | 出荷実績マスタ */
    registerTargetMaster: 'product_packaging' as RegisterTargetMaster,
    /** 容積測定 縦/横 自動変更: 最長辺 = 縦 | 最長辺 = 横 */
    volumeLongestSide: 'length' as VolumeLongestSide,
  }),

  actions: {
    setRegisterTargetMaster(value: RegisterTargetMaster) {
      this.registerTargetMaster = value
    },
    setVolumeLongestSide(value: VolumeLongestSide) {
      this.volumeLongestSide = value
    },
  },
})
