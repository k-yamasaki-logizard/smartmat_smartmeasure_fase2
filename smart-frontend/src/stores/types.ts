/**
 * 測定モード: 容積+重量 / 容積のみ / 重量のみ
 */
export type MeasureMode = 'volume-and-weight' | 'volume' | 'weight'

/**
 * 登録先マスタ: 商品マスタ | 商品梱包形態マスタ
 */
export type RegisterTargetMaster = 'm_item' | 'm_item_pack'

/**
 * 容積測定 縦/横 自動変更: 最長辺 = 縦 | 最長辺 = 横
 */
export type VolumeLongestSide = 'length' | 'width'

/**
 * 測定済み商品 1件（storeId で一意、作業中〜確定済みを storedData で管理）
 */
export interface StoredDataItem {
  tempItemId: string
  barcode: string
  length: string
  width: string
  height: string
  weight: string
  isMeasuringWeight: boolean
  measuredAt?: string
}

/**
 * 測定済み商品の配列
 */
export type StoredData = StoredDataItem[]

/**
 * 測定済み商品を storeId でキーにしたオブジェクト（store の state 用）
 */
export type StoredDataRecord = Record<string, StoredDataItem>
