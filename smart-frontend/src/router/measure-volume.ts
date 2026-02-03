import type { RouteRecordRaw } from 'vue-router'
import BarcodeScanPage from '@/views/03_02_measure_volume/BarcodeScanPage.vue'
import MeasureVolumePage from '@/views/03_02_measure_volume/MeasureVolumePage.vue'
import ListPage from '@/views/03_02_measure_volume/ListPage.vue'
import ConfirmPage from '@/views/03_02_measure_volume/ConfirmPage.vue'
import FinishPage from '@/views/03_02_measure_volume/FinishPage.vue'

/**
 * measure-volume（容積のみ）のルート定義
 */
const routes: RouteRecordRaw[] = [
  {
    path: '/update/volume',
    children: [
      { path: 'barcode-scan', name: 'VolumeBarcodeScan', component: BarcodeScanPage },
      { path: 'measure-volume', name: 'VolumeMeasureVolume', component: MeasureVolumePage },
      { path: 'list', name: 'VolumeList', component: ListPage },
      { path: 'confirm', name: 'VolumeConfirm', component: ConfirmPage },
      { path: 'finish', name: 'VolumeFinish', component: FinishPage },
    ],
  },
]

export default routes
