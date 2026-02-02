import type { RouteRecordRaw } from 'vue-router'
import BarcodeScanPage from '@/views/03_03_measure-weight/BarcodeScanPage.vue'
import MeasureWeightPage from '@/views/03_03_measure-weight/MeasureWeightPage.vue'
import ListPage from '@/views/03_03_measure-weight/ListPage.vue'
import ConfirmPage from '@/views/03_03_measure-weight/ConfirmPage.vue'
import FinishPage from '@/views/03_03_measure-weight/FinishPage.vue'

/**
 * measure-weight（重量のみ）のルート定義
 */
const routes: RouteRecordRaw[] = [
  {
    path: '/update/weight',
    children: [
      { path: 'barcode-scan', name: 'WeightBarcodeScan', component: BarcodeScanPage },
      { path: 'measure-weight', name: 'WeightMeasureWeight', component: MeasureWeightPage },
      { path: 'list', name: 'WeightList', component: ListPage },
      { path: 'confirm', name: 'WeightConfirm', component: ConfirmPage },
      { path: 'finish', name: 'WeightFinish', component: FinishPage },
    ],
  },
]

export default routes
