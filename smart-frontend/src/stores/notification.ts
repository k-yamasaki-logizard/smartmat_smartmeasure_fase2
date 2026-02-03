import { defineStore } from 'pinia'

/**
 * お知らせ通知ストア
 * show(message) でメッセージ表示、dismiss() で非表示。
 * 他 store やコンポーネントから useNotificationStore().show('お知らせ') で利用する。
 */
export const useNotificationStore = defineStore('notification', {
  state: () => ({
    /** 表示するメッセージ（null のときは非表示） */
    message: null as string | null,
  }),

  actions: {
    /**
     * お知らせを表示する
     */
    show(message: string): void {
      this.message = message
    },

    /**
     * お知らせを閉じる（タッチで消すときに呼ぶ）
     */
    dismiss(): void {
      this.message = null
    },
  },
})
