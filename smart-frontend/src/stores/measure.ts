import { defineStore } from 'pinia'
import type { MeasureMode, RegisterTargetMaster, StoredDataItem, StoredDataRecord } from './types'
import { useSmartMat } from '@/composables/smart-mat'
import { useZeroApi } from '@/composables/zero-api'
import type { ImportResponse } from '@/composables/zero-api'


function pad2(n: number): string {
  return ('0' + n).slice(-2)
}

function formatMeasuredAt(): string {
  const d = new Date()
  return `${d.getFullYear()}/${pad2(d.getMonth() + 1)}/${pad2(d.getDate())} ${pad2(d.getHours())}:${pad2(d.getMinutes())}`
}

/**
 * submit時の部分
 * ・settings storeでの登録先マスタ
 * ・measure storeでの測定モード
 * 上記によって、動きが変わる
*/
const submitMap: Record<RegisterTargetMaster, Record<MeasureMode, (storedData: StoredDataItem[]) => Promise<ImportResponse>>> = {
  'm_item': {
    'volume-and-weight': async (storedData: StoredDataItem[]) => {
      const requests = storedData.map((item) => ({
        itemId: item.itemId,
        weight: item.weight,
        length: item.length,
        width: item.width,
        height: item.height,
      }))
      return await useZeroApi().updateItemWeightAndSize(requests)
    },
    'volume': async (storedData: StoredDataItem[]) => {
      const requests = storedData.map((item) => ({
        itemId: item.itemId,
        length: item.length,
        width: item.width,
        height: item.height,
      }))
      return await useZeroApi().updateItemSize(requests)
    },
    'weight': async (storedData: StoredDataItem[]) => {
      const requests = storedData.map((item) => ({
        itemId: item.itemId,
        weight: item.weight,
      }))
      return await useZeroApi().updateItemWeight(requests)
    },
  },
  'm_item_pack': {
    'volume-and-weight': async (storedData: StoredDataItem[]) => {
      const requests = storedData.map((item) => ({
        itemId: item.itemId,
        caseBarcode: item.barcode,
        caseWeight: item.weight,
        caseLength: item.length,
        caseWidth: item.width,
        caseHeight: item.height,
      }))
      return await useZeroApi().updateItemPackageWeightAndSize(requests)
    },
    'volume': async (storedData: StoredDataItem[]) => {
      const requests = storedData.map((item) => ({
        itemId: item.itemId,
        caseBarcode: item.barcode,
        caseLength: item.length,
        caseWidth: item.width,
        caseHeight: item.height,
      }))
      return await useZeroApi().updateItemPackageSize(requests)
    },
    'weight': async (storedData: StoredDataItem[]) => {
      const requests = storedData.map((item) => ({
        itemId: item.itemId,
        caseBarcode: item.barcode,
        caseWeight: item.weight,
      }))
      return await useZeroApi().updateItemPackageWeight(requests)
    },
  },
}

/**
 * 測定ストア
 * editingTempId で「編集中」の StoredDataItem を指し、全件は storedData で管理
 * 
 * ※
 * 重量測定など、非同期で「編集中」か「測定済」の商品に更新を行う箇所があるので
 * 簡単のために編集中と測定済の商品を同じ領域に置いておく
 */
export const useMeasureStore = defineStore('measure', {
  state: () => ({
    /** 測定モード */
    mode: 'volume-and-weight' as MeasureMode,
    /** 編集中の StoredDataItem の tempId（空のときはなし） */
    editingTempId: '',
    /** 測定済み商品（storeId をキーにしたオブジェクト、作業中〜確定済みを含む） */
    storedData: {} as StoredDataRecord,
  }),

  getters: {
    /** 測定済みリスト（表示用。連番付き） */
    storedList(): StoredDataItem[] {
      return Object.values(this.storedData).map((item, i) => ({
        ...item,
        number: i + 1,
      }))
    },
    /** 編集中の 1 件（editingTempId に一致する StoredDataItem） */
    editingItem(): StoredDataItem | null {
      if (!this.editingTempId) return null
      return this.storedData[this.editingTempId] ?? null
    },
    /** 測定中の商品が 1 件でもあるか */
    hasMeasuringWeightItem(): boolean {
      return Object.values(this.storedData).some((item: StoredDataItem) => item.weightMeasuringStatus === 'measuring')
    },
  },

  actions: {

    initialize(mode: MeasureMode) {
      this.mode = mode
      this.editingTempId = ''
      this.storedData = {}
    },

    /**
     * 新規 1 件を開始（バーコードスキャン確定時）。storedData に追加し editingTempId をセット
     */
    addEditingItem(barcode: string, itemId: string, itemName: string) {
      const item: StoredDataItem = {
        tempId: crypto.randomUUID(),
        barcode: barcode,
        itemId: itemId,
        itemName: itemName,
        length: '',
        width: '',
        height: '',
        weight: '',
        weightMeasuringStatus: 'not_measured',
      }
      this.storedData[item.tempId] = item
      this.editingTempId = item.tempId
    },

    /**
     * 編集中商品の保存
     * 商品は残しつつ、編集中の商品IDをクリアする
     */
    storeEditingItem() {
      this.editingTempId = ''
    },

    /**
     * 指定した商品を編集中にセット（再測定時など）
     * storedData に存在する tempId の場合のみ editingTempId を更新する
     */
    setEditingItemById(tempId: string) {
      if (!this.storedData[tempId]) {
        throw new Error(`編集中の商品が存在しません(ID:${tempId})`);
      }
      this.editingTempId = tempId
    },

    clearEditingItem() {
      delete this.storedData[this.editingTempId]
      this.editingTempId = ''
    },

    /**
     * 編集中の StoredDataItem の容積（length/width/height）を更新
     */
    updateEditingItemVolume(
      payload: { length?: string; width?: string; height?: string }
    ) {
      const item = this.storedData[this.editingTempId]
      if (!item) {
        throw new Error(`編集中の商品が存在しません(ID:${this.editingTempId})`);
      }
      if (payload.length !== undefined) item.length = payload.length
      if (payload.width !== undefined) item.width = payload.width
      if (payload.height !== undefined) item.height = payload.height
    },

    /**
     * 編集中の item の重量測定を行う（weight / weightMeasuringStatus を更新）
     */
    async updateEditingItemWeight() {
      const currentTime = new Date().getTime();
      const item = this.storedData[this.editingTempId]
      if (!item) {
        throw new Error(`編集中の商品が存在しません(ID:${this.editingTempId})`);
      }
      item.weightMeasuringStatus = 'measuring'

      try {

        // SmartMatのAPI反映を待つ
        // 1. 10秒待つ
        // 2. /api/smartmat-api/stock-info を呼び出す
        // 3. measuredAtが2026-02-04 00:10:45+09:00の形式で得られる
        // 4. measuredAtが計測開始時刻以後になるか、1分経つまでポーリングする(10秒ごと)
        let stockInfo = null;

        do {
          await new Promise((resolve) => setTimeout(resolve, 10 * 1000))

          stockInfo = await useSmartMat().getStockInfo()
          if (!stockInfo) {
            throw new Error('api')
          }
          if (new Date().getTime() >= currentTime + 120 * 1000) {
            throw new Error('timeout')
          }
        } while (new Date(stockInfo.deviceMeasurement.measuredAt).getTime() <= currentTime)

        item.weight = stockInfo.deviceMeasurement.current.toString()
        item.weightMeasuringStatus = 'measured'
        return {
          success: true,
          itemName: item.itemName,
          weight: item.weight,
        }
      } catch (error) {
        item.weightMeasuringStatus = 'failed'
        return {
          success: false,
          error: error instanceof Error ? (error.message as 'timeout' | 'api') : 'unknown',
          itemName: item.itemName,
        }
      }
    },

    /**
     * 編集中の 1 件に measuredAt をセット（登録確認・次商品へ時）
     */
    updateEditingItemMeasuredAt() {
      const item = this.storedData[this.editingTempId]
      if (item) item.measuredAt = formatMeasuredAt()
    },

    /**
     * 測定済みデータをAPIに送信する（確認画面の確定時）
     * 成功時に storedData と editingTempId をクリアする
     */
    async submitItems(targetMaster: RegisterTargetMaster): Promise<void> {
      const updateMethod = submitMap[targetMaster][this.mode as MeasureMode]
      const storedData: StoredDataItem[] = Object.values(this.storedData);
      const result = await updateMethod(storedData);
      this.storedData = {}
      this.editingTempId = ''
      return result;
    },
  },

  persist: true
})
