import type { RouteRecordRaw } from 'vue-router'
import HomePage from '@/views/00_opening/HomePage.vue'
import SettingsPage from '@/views/00_opening/SettingsPage.vue'
import SelectUpdateTypesPage from '@/views/00_opening/SelectUpdateTypePage.vue'

/**
 * openingのルート定義
 */
const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name: 'Home',
        component: HomePage
    },
    {
        path: '/settings',
        name: 'Settings',
        component: SettingsPage
    },
    {
        path: '/update',
        name: 'SelectUpdateTypes',
        component: SelectUpdateTypesPage
    }
]

export default routes;