import type { RouteRecordRaw } from 'vue-router'
import BarcodeScanPage from '@/views/03_01_measure-volume-and-weight/BarcodeScanPage.vue'
import MeasureVolumePage from '@/views/03_01_measure-volume-and-weight/MeasureVolumePage.vue'
import MeasureWeightPage from '@/views/03_01_measure-volume-and-weight/MeasureWeightPage.vue'
import ListPage from '@/views/03_01_measure-volume-and-weight/ListPage.vue'

/**
 * measure-volume-and-weightのルート定義
 */
const routes: RouteRecordRaw[] = [
    {
        path: '/update/volume-and-weight',
        children: [
            {
                path: 'barcode-scan',
                name: 'BarcodeScan',
                component: BarcodeScanPage
            },
            {
                path: "measure-volume",
                name: 'MeasureVolume',
                component: MeasureVolumePage
            },
            {
                path: "measure-weight",
                name: 'MeasureWeight',
                component: MeasureWeightPage
            },
            {
                path: "list",
                name: 'ListVolumeAndWeight',
                component: ListPage
            },
        ]
    }
]

export default routes;