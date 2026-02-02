import { defineStore } from 'pinia'

/**
 * サンプル counter ストア。
 * 使用例: コンポーネントで
 *   import { useCounterStore } from '@/stores/counter'
 *   const store = useCounterStore()
 * テンプレートで {{ store.count }} や @click="store.increment()"
 */
export const useCounterStore = defineStore('counter', {
  state: () => ({ count: 0 }),
  actions: {
    increment() {
      this.count++
    },
  },
})
