const measureWeight = async (itemId: string): Promise<void> => {
    await new Promise(resolve => setTimeout(resolve, 1000));
    return;
}

export default measureWeight;