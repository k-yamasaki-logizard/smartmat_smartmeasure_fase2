import { defineStore } from 'pinia'

/**
 * 画面全体ローディング用ストア
 * show() で表示、hide() で非表示。API 呼び出し前後に useLoadingStore().show() / hide() で利用する。
 */
export const useLoadingStore = defineStore('loading', {
  state: () => ({
    /** ローディング表示中か */
    isLoading: false,
  }),

  actions: {
    /** ローディングを表示する */
    show(): void {
      this.isLoading = true
    },

    /** ローディングを非表示にする */
    hide(): void {
      this.isLoading = false
    },
  },
})
