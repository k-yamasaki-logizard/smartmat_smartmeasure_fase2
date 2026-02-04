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

export type StockInfoResponse = {
    device: {
        deviceSerialNumber: string,
        version: string
    },
    deviceMeasurement: {
        isConnected: boolean,
        current: number,
        measuredAt: string,
        battery: number,
        rssi: number
    },
    subscription: {
        subscribed: boolean,
        outputTypeText: string,
        quantity: number,
        measurementModeText: string,
        measurementTypeText: string,
        full: number | null,
        weight: number,
        weightUnit: string,
        containerWeight: number,
        triggerRemainingPercent: number | null,
        triggerRemainingNumber: number | null
    },
    measurementControl: {
        v1: null,
        v2: {
            frequency: number,
            timing: string,
            standardHour: number
        },
        frequency: number,
        timing: string,
        standardHour: number
    },
    subscriptionMeasurement: {
        current: number,
        measuredAt: string,
        remainingPercent: number | null,
        remainingNumber: number
    },
    tags: null,
    product: {
        productCode: string,
        title: string,
        imageUrl: string
    }
}

export const useSmartMat = () => {
    const getLatestMeasureHistory = async (): Promise<LatestMeasureHistoryResponse | null> => {
        const response = await fetch('/api/smartmat-api/latest-measure-history')
        const data = await response.json()
        return data
    }

    const getStockInfo = async (): Promise<StockInfoResponse | null> => {
        const response = await fetch('/api/smartmat-api/stock-info')
        const data = await response.json()
        return data
    }
    return {
        getLatestMeasureHistory,
        getStockInfo
    }
}