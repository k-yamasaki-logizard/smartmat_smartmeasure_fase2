export type GetSkuResponse = {
    ERROR_CODE: string;
    DATA: {
        SKU: [{
            ITEM_ID: string;
            ITEM_NAME: string;
            STOCK_KEY1: string;
            STOCK_KEY2: string;
            STOCK_KEY3: string;
            STOCK_KEY4: string;
            LOT_USE_FLG: 0 | 1;
            EFFECTIVE_TERM_TYP: string;
            ARV_LIMIT_DAYS: number;
            ARV_DATE_USE_FLG: 0 | 1;
            UNIT_QTY: number;
            IMG_URL: string
        }];
    }
}

export type ImportResponse = {
    ERROR_CODE: string;
    DATA: {
        RESULT: {
            UNIQUE_CODE: string;
            CREATE_DTTM: string;
            TOTAL_REC: string;
            CAPTYRE_REC: string;
            IGNORE_REC: string;
            ERR_REC: string;
            ERROR_DETAIL: {
                LINE: string;
                ERROR_MESSAGE: string;
                COLUMN: string;
                KEY_INFO: string;
            }[];
        };
    };
}

export type ItemPackageWeightAndSizeRequest = {
    itemId: string;
    caseBarcode: string;
    caseWeight: string;
    caseLength: string;
    caseWidth: string;
    caseHeight: string;
}

export type ItemPackageWeightRequest = {
    itemId: string;
    caseBarcode: string;
    caseWeight: string;
}

export type ItemPackageSizeRequest = {
    itemId: string;
    caseBarcode: string;
    caseLength: string;
    caseWidth: string;
    caseHeight: string;
}

export type ItemWeightRequest = {
    itemId: string;
    weight: string;
}

export type ItemSizeRequest = {
    itemId: string;
    length: string;
    width: string;
    height: string;
}

export type ItemWeightAndSizeRequest = {
    itemId: string;
    weight: string;
    length: string;
    width: string;
    height: string;
}

export const useZeroApi = () => {
    const getSku = async (pack_id: string, barcode: string): Promise<GetSkuResponse | null> => {
        try {
            const response = await fetch(`/api/zero-api/sku?pack_id=${pack_id}&barcode=${barcode}`);
            if (!response.ok) {
                return null;
            }
            return response.json() as Promise<GetSkuResponse>;
        }
        catch (error) {
            console.error(error);
            return null;
        }
    }

    const updateItemPackageWeightAndSize = async (request: ItemPackageWeightAndSizeRequest[]): Promise<ImportResponse> => {
        const response = await fetch(`/api/zero-api/item-package-weight-and-size`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error('Failed to update item package weight and size');
        }
        return response.json() as Promise<ImportResponse>;
    }

    const updateItemPackageWeight = async (request: ItemPackageWeightRequest[]): Promise<ImportResponse> => {
        const response = await fetch(`/api/zero-api/item-package-weight`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error('Failed to update item package weight');
        }
        return response.json() as Promise<ImportResponse>;
    }

    const updateItemPackageSize = async (request: ItemPackageSizeRequest[]): Promise<ImportResponse> => {
        const response = await fetch(`/api/zero-api/item-package-size`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error('Failed to update item package size');
        }
        return response.json() as Promise<ImportResponse>;
    }

    const updateItemWeight = async (request: ItemWeightRequest[]): Promise<ImportResponse> => {
        const response = await fetch(`/api/zero-api/item-weight`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error('Failed to update item weight');
        }
        return response.json() as Promise<ImportResponse>;
    }

    const updateItemSize = async (request: ItemSizeRequest[]): Promise<ImportResponse> => {
        const response = await fetch(`/api/zero-api/item-size`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error('Failed to update item size');
        }
        return response.json() as Promise<ImportResponse>;
    }

    const updateItemWeightAndSize = async (request: ItemWeightAndSizeRequest[]): Promise<ImportResponse> => {
        const response = await fetch(`/api/zero-api/item-weight-and-size`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error('Failed to update item weight and size');
        }
        return response.json() as Promise<ImportResponse>;
    }

    return {
        getSku,
        updateItemPackageWeight,
        updateItemPackageSize,
        updateItemPackageWeightAndSize,
        updateItemWeight,
        updateItemSize,
        updateItemWeightAndSize,
    }
}