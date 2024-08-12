interface Callback {
    (): void | boolean;
}

export interface CallbackType {
    callback: Callback;
    id: string;
}

export const CallbackUtil = {
    subscribe: (storeCallbacks: CallbackType[], id: string, callback: () => void) => {
        const check = storeCallbacks.filter(f => f.id.toLowerCase() == id.toLowerCase())[0];
        if (check) {
            check.callback = callback;
        }
        else {
            storeCallbacks.push({
                id: id,
                callback: callback
            });
        }
        return storeCallbacks;
    },
    unsubscribe: (storeCallbacks: CallbackType[], id: string) => {
        storeCallbacks = storeCallbacks.filter(f => f.id != id);
        return storeCallbacks;

    },
    notify: (storeCallbacks: CallbackType[], id?: string) => {
        if (id) {
            const rlt = storeCallbacks.filter(f => f.id == id)[0];
            if (rlt) {
                const check: boolean = rlt.callback() as boolean;
                if (!check) {
                    storeCallbacks = CallbackUtil.unsubscribe(storeCallbacks, id);
                }
            }
        }
        else {
            storeCallbacks.forEach((item) => {
                const check: boolean = item.callback() as boolean;
                if (!check) {
                    storeCallbacks = CallbackUtil.unsubscribe(storeCallbacks, item.id);
                }
            })
        }
        return storeCallbacks;
    },
}