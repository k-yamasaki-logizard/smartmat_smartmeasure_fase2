import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import openingRoutes from './opening'
import measureVolumeAndWeightRoutes from './measure-volume-and-weight'

// ルート定義
const routes: RouteRecordRaw[] = [
    ...openingRoutes,
    ...measureVolumeAndWeightRoutes
]

// ルーターインスタンスの作成
const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router