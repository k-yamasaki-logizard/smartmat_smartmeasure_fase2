import { defineStore } from 'pinia'
import type { MeasureMode, StoredDataItem, StoredDataRecord } from './types'

function pad2(n: number): string {
  return ('0' + n).slice(-2)
}

function formatMeasuredAt(): string {
  const d = new Date()
  return `${d.getFullYear()}/${pad2(d.getMonth() + 1)}/${pad2(d.getDate())} ${pad2(d.getHours())}:${pad2(d.getMinutes())}`
}

/**
 * 測定ストア
 * editingItemId で「編集中」の StoredDataItem を指し、全件は storedData で管理
 * 
 * ※
 * 重量測定など、非同期で「編集中」か「測定済」の商品に更新を行う箇所があるので
 * 簡単のために編集中と測定済の商品を同じ領域に置いておく
 */
export const useMeasureStore = defineStore('measure', {
  state: () => ({
    /** 測定モード */
    mode: 'volume-and-weight' as MeasureMode,
    /** 編集中の StoredDataItem の tempItemId（空のときはなし） */
    editingItemId: '',
    /** 測定済み商品（storeId をキーにしたオブジェクト、作業中〜確定済みを含む） */
    storedData: {} as StoredDataRecord,
    /** 重量測定中(Smart Mat使用中フラグ) */
    isMeasuringWeight: false,
  }),

  getters: {
    /** 測定済みリスト（表示用。連番付き） */
    storedList(): StoredDataItem[] {
      return Object.values(this.storedData).map((item, i) => ({
        ...item,
        number: i + 1,
      }))
    },
    /** 編集中の 1 件（editingItemId に一致する StoredDataItem） */
    editingItem(): StoredDataItem | null {
      if (!this.editingItemId) return null
      return this.storedData[this.editingItemId] ?? null
    },
  },

  actions: {

    initialize(mode: MeasureMode) {
      this.mode = mode
      this.editingItemId = ''
      this.storedData = {}
      this.isMeasuringWeight = false
    },

    /**
     * 新規 1 件を開始（バーコードスキャン確定時）。storedData に追加し editingItemId をセット
     */
    addEditingItem(barcode: string) {
      const item: StoredDataItem = {
        tempItemId: crypto.randomUUID(),
        barcode: barcode,
        length: '',
        width: '',
        height: '',
        weight: '',
        isMeasuringWeight: false,
      }
      this.storedData[item.tempItemId] = item
      this.editingItemId = item.tempItemId
    },

    /**
     * 編集中商品の保存
     * 商品は残しつつ、編集中の商品IDをクリアする
     */
    storeEditingItem() {
      this.editingItemId = ''
    },

    /**
     * 指定した商品を編集中にセット（再測定時など）
     * storedData に存在する tempItemId の場合のみ editingItemId を更新する
     */
    setEditingItemById(tempItemId: string) {
      if (this.storedData[tempItemId]) {
        this.editingItemId = tempItemId
      }
    },

    clearEditingItem() {
      delete this.storedData[this.editingItemId]
      this.editingItemId = ''
    },

    /**
     * 編集中の StoredDataItem の容積（length/width/height）を更新
     */
    updateEditingItemVolume(
      payload: { length?: string; width?: string; height?: string }
    ) {
      const item = this.storedData[this.editingItemId]
      if (!item) return
      if (payload.length !== undefined) item.length = payload.length
      if (payload.width !== undefined) item.width = payload.width
      if (payload.height !== undefined) item.height = payload.height
    },

    /**
     * 編集中の item の重量測定を行う（weight / isMeasuringWeight を更新）
     */
    async updateEditingItemWeight() {
      const item = this.storedData[this.editingItemId]
      if (!item) return
      this.isMeasuringWeight = true
      item.isMeasuringWeight = true
      await new Promise((resolve) => setTimeout(resolve, 30 * 1000))
      console.log('updateWeight', this.editingItemId)
      item.weight = Math.floor(Math.random() * 1000000).toString()
      item.isMeasuringWeight = false
      this.isMeasuringWeight = false
    },

    /**
     * 編集中の 1 件に measuredAt をセット（登録確認・次商品へ時）
     */
    updateEditingItemMeasuredAt() {
      const item = this.storedData[this.editingItemId]
      if (item) item.measuredAt = formatMeasuredAt()
    },

    /**
     * 測定済みデータをAPIに送信する（確認画面の確定時）
     * 成功時に storedData と editingItemId をクリアする
     */
    async submitItems(): Promise<void> {
      // TODO: 実際のAPI呼び出しに差し替える
      console.log('submitItems', this.storedData)
      await Promise.resolve()
      this.storedData = {}
      this.editingItemId = ''
    },
  },
})
