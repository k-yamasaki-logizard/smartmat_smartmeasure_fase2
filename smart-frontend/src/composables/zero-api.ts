type GetSkuResponse = {
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

const useZeroApi = () => {
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
    return {
        getSku
    }
}

export default useZeroApi;