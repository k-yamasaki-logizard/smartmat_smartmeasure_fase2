export type LatestMeasureHistoryResponse = {
    measuredAt: string
    current: number,
    prev: number,
    battery: number,
    rssi: number,
    pushedMEasureButtn: boolean,
    measurementModeText: string,
    frequency: number,
    timing: number,
    standardHour: number
}

export const useSmartMat = () => {
    const getLatestMeasureHistory = async (): Promise<LatestMeasureHistoryResponse | null> => {
        const response = await fetch('/api/smartmat-api/latest-measure-history')
        const data = await response.json()
        return data
    }
    return {
        getLatestMeasureHistory
    }
}